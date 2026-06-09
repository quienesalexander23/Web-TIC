using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Models;
using WebTIC.API.Models.DTOs;

namespace WebTIC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")] // Solo administradores pueden gestionar usuarios
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuariosController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] string? role = null, [FromQuery] bool? isActive = null)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(u => u.Email.ToLower().Contains(search) || 
                                         u.FirstName.ToLower().Contains(search) || 
                                         u.LastName.ToLower().Contains(search));
            }

            if (isActive.HasValue)
            {
                query = query.Where(u => u.IsActive == isActive.Value);
            }

            var totalItems = await query.CountAsync();

            var users = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userDtos = new List<UsuarioDto>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                // Si hay un filtro por rol, excluimos los que no coincidan
                if (!string.IsNullOrEmpty(role) && !userRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
                {
                    totalItems--; // Ajuste simple para paginación
                    continue;
                }

                userDtos.Add(new UsuarioDto
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    Roles = userRoles.ToList()
                });
            }

            var result = new PaginatedResult<UsuarioDto>
            {
                Items = userDtos,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioDto dto)
        {
            if (!dto.Email.EndsWith("@epn.edu.ec"))
                return BadRequest(new { message = "El correo debe ser del dominio @epn.edu.ec" });

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest(new { message = "El correo ya está registrado" });

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsActive = true,
                EmailConfirmed = true
            };

            // Contraseña temporal (en producción se enviaría por correo)
            string temporalPassword = "TemporalPassword2026!!";
            
            var result = await _userManager.CreateAsync(user, temporalPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return Ok(new { message = "Usuario creado exitosamente", temporalPassword });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(string id, [FromBody] UpdateUsuarioDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) return BadRequest(updateResult.Errors);

            // Manejo del Rol
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(dto.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!string.IsNullOrEmpty(dto.Role))
                {
                    await _userManager.AddToRoleAsync(user, dto.Role);
                }
            }

            return Ok(new { message = "Usuario actualizado exitosamente" });
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> ToggleEstadoUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive; // Toggle lógico
            await _userManager.UpdateAsync(user);

            return Ok(new { message = $"Usuario {(user.IsActive ? "activado" : "desactivado")} exitosamente", isActive = user.IsActive });
        }
    }
}
