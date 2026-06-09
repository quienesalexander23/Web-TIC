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

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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
            
            // En un entorno real aquí se enviaría un correo (System.Net.Mail / SendGrid)
            // Para propósitos de la tesis y desarrollo, vamos a imprimir el Token en la consola del backend o devolverlo
            Console.WriteLine($"\n=======================================\n[RESET TOKEN PARA {model.Email}]:\n{token}\n=======================================\n");

            return Ok(new { Message = "Si el correo existe y está activo, se ha enviado un enlace de recuperación.", DebugToken = token });
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
