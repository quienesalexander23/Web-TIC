using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebTIC.API.Models;
using WebTIC.API.Models.DTOs;
using System.Linq;

namespace WebTIC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Cualquier usuario autenticado puede acceder a su perfil
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var profileDto = new UsuarioDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                Roles = roles.ToList()
            };

            return Ok(profileDto);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateUsuarioDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // Solo actualizamos Nombre y Apellido
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "Perfil actualizado exitosamente" });
        }
    }
}
