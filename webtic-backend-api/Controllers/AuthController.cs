using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebTIC.API.DTOs;
using WebTIC.API.Models;

namespace WebTIC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly WebTIC.API.Services.IEmailService _emailService;
        private readonly WebTIC.API.Services.IAuditService _auditService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            WebTIC.API.Services.IEmailService emailService,
            WebTIC.API.Services.IAuditService auditService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
            _auditService = auditService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            // 1. Validar dominio institucional (Requerimiento Flujo 1)
            if (!model.Email.EndsWith("@epn.edu.ec"))
            {
                return BadRequest(new { Message = "Debe utilizar un correo institucional (@epn.edu.ec)." });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            // Mitigación de enumeración de usuarios (Mensaje genérico)
            if (user == null)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas" });
            }

            // Validar si el usuario fue dado de baja lógicamente (Flujo 2)
            if (!user.IsActive)
            {
                return Unauthorized(new { Message = "Su cuenta se encuentra inactiva. Contacte al administrador." });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                await _auditService.LogEventAsync("LOCKOUT", user.Id, ip, "Cuenta bloqueada por superar el límite de 5 intentos fallidos.");
                
                return StatusCode(423, new 
                { 
                    ErrorType = "LockedOut", 
                    Message = "Acceso Bloqueado", 
                    Description = "Por motivos de seguridad su cuenta ha sido suspendida. Contacte al administrador." 
                });
            }

            if (!result.Succeeded)
            {
                var count = await _userManager.GetAccessFailedCountAsync(user);
                return Unauthorized(new 
                { 
                    ErrorType = "InvalidCredentials", 
                    Message = "Acceso Denegado", 
                    Description = $"Credenciales incorrectas. Intento {count} de 5." 
                });
            }

            // Generar código 2FA
            var twoFactorCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            // Preparar el cuerpo del correo
            var emailBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e5e7eb; border-radius: 8px; overflow: hidden;'>
                    <div style='background-color: #00346F; padding: 20px; text-align: center;'>
                        <h1 style='color: white; margin: 0; font-size: 24px;'>Sistema Web-TIC</h1>
                    </div>
                    <div style='padding: 30px; color: #333;'>
                        <h2 style='color: #00346F; margin-top: 0;'>Código de Acceso</h2>
                        <p>Hola {user.FirstName},</p>
                        <p>Has intentado iniciar sesión en tu cuenta. Por favor, utiliza el siguiente código numérico para verificar tu identidad:</p>
                        <div style='background-color: #f3f4f6; padding: 15px; text-align: center; font-size: 24px; font-weight: bold; letter-spacing: 5px; color: #00346F; margin: 20px 0; border-radius: 4px;'>
                            {twoFactorCode}
                        </div>
                        <p>Este código caduca en un par de minutos.</p>
                        <p style='font-size: 12px; color: #6b7280; margin-top: 30px;'>Si no solicitaste este código, puedes ignorar este mensaje.</p>
                    </div>
                </div>";

            try
            {
                await _emailService.SendEmailAsync(user.Email!, "Tu código de acceso", emailBody);
            }
            catch (Exception ex)
            {
                // No bloquear el login si el proveedor de correo falla temporalmente.
                Console.WriteLine($"[EmailService] Envío de código 2FA falló para {user.Email}: {ex.Message}");
            }

            return Ok(new
            {
                Requires2FA = true,
                Email = user.Email,
                Message = "Se ha enviado un código de seguridad a su correo."
            });
        }

        [HttpPost("verify-2fa")]
        public async Task<IActionResult> Verify2FA([FromBody] Verify2FADto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !user.IsActive)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas o cuenta inactiva." });
            }

            // Verificar el código 2FA
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", model.Code);
            if (!isValid)
            {
                return BadRequest(new { Message = "El código ingresado es incorrecto o ha expirado." });
            }

            // Código válido, generar JWT
            var token = await GenerateJwtToken(user);
            
            // Auditoría
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            await _auditService.LogEventAsync("LOGIN_2FA", user.Id, ipAddress, "Inicio de sesión 2FA exitoso");

            return Ok(new 
            { 
                Token = token,
                Message = "Autenticación exitosa"
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !user.IsActive)
            {
                // Devolvemos 200 de igual manera para evitar enumeración de correos
                return Ok(new { Message = "Si el correo existe y está activo, se ha enviado un enlace de recuperación." });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Construimos la URL del Frontend para recuperar contraseña
            var encodedToken = Uri.EscapeDataString(token);
            var resetLink = $"http://localhost:4200/reset-password?email={model.Email}&token={encodedToken}";

            // Usamos el IEmailService para enviar el correo con diseño profesional
            var emailBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e5e7eb; border-radius: 8px; overflow: hidden;'>
                    <div style='background-color: #00346F; padding: 20px; text-align: center;'>
                        <h1 style='color: white; margin: 0; font-size: 24px;'>Sistema Web-TIC</h1>
                    </div>
                    <div style='padding: 30px; color: #333;'>
                        <h2 style='color: #00346F; margin-top: 0;'>Recuperación de Contraseña</h2>
                        <p>Hola {user.FirstName},</p>
                        <p>Hemos recibido una solicitud para restablecer tu contraseña.</p>
                        <p>Haz clic en el siguiente botón para crear una nueva:</p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{resetLink}' style='background-color: #00346F; color: white; padding: 12px 24px; text-decoration: none; border-radius: 4px; font-weight: bold;'>Restablecer Contraseña</a>
                        </div>
                        <p style='word-break: break-all; font-size: 13px;'>Si el botón no funciona, copia y pega este enlace:<br><a href='{resetLink}'>{resetLink}</a></p>
                        <p style='font-size: 12px; color: #6b7280; margin-top: 30px;'>Si no realizaste esta solicitud, puedes ignorar este correo de forma segura.</p>
                    </div>
                </div>";

            try
            {
                await _emailService.SendEmailAsync(user.Email, "Recuperación de Contraseña", emailBody);
            }
            catch (Exception ex)
            {
                // No bloquear el flujo de recuperación si el proveedor de correo falla temporalmente.
                Console.WriteLine($"[EmailService] Envío de enlace de recuperación falló para {user.Email}: {ex.Message}");
            }

            return Ok(new { Message = "Si el correo existe y está activo, se ha enviado un enlace de recuperación." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "Error al restablecer la contraseña." });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            
            if (result.Succeeded)
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                await _auditService.LogEventAsync("RESET_PASSWORD", user.Id, ipAddress, "Contraseña restablecida correctamente");
                
                return Ok(new { Message = "Contraseña restablecida correctamente." });
            }

            return BadRequest(new { Message = "Token inválido o expirado, o la contraseña no cumple las políticas.", Errors = result.Errors });
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? jwtSettings["Secret"];
            var key = Encoding.ASCII.GetBytes(secret!);

            // Obtener roles del usuario
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            // Agregar roles a los claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
