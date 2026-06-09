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

            // Validar Login (con lockout activado)
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                return StatusCode(423, new { Message = "Cuenta suspendida temporalmente por múltiples intentos fallidos. Intente nuevamente en 15 minutos." });
            }

            if (!result.Succeeded)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas" });
            }

            // Generar JWT
            var token = await GenerateJwtToken(user);
            
            // Auditoría
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            await _auditService.LogEventAsync("LOGIN", user.Id, ipAddress, "Inicio de sesión exitoso");

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

            // Usamos el IEmailService para enviar el correo (localmente se guardará en /LocalEmails)
            var emailBody = $@"
                <h2>Restablecer Contraseña Web-TIC EPN</h2>
                <p>Hola {user.FirstName},</p>
                <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta institucional.</p>
                <p>Por favor, haz clic en el siguiente enlace para crear una nueva contraseña:</p>
                <p><a href='{resetLink}'>{resetLink}</a></p>
                <br>
                <p>Si no realizaste esta solicitud, puedes ignorar este correo de forma segura.</p>
            ";

            await _emailService.SendEmailAsync(user.Email, "Recuperación de Contraseña - WebTIC", emailBody);

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
            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

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
