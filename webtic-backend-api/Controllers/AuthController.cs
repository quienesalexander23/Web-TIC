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
                return Unauthorized(new { Message = "Cuenta suspendida temporalmente por múltiples intentos fallidos. Intente nuevamente en 15 minutos." });
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
