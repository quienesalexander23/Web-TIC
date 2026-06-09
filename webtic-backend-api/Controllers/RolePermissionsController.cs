using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using WebTIC.API.Models;

namespace WebTIC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class RolePermissionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolePermissionsController(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        [HttpGet("{roleName}")]
        public async Task<IActionResult> GetPermissionsByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return NotFound("Rol no encontrado");

            var permissions = await _context.SystemPermissions
                .Where(p => p.RoleId == role.Id)
                .ToListAsync();

            return Ok(permissions);
        }

        [HttpPost("{roleName}")]
        public async Task<IActionResult> SavePermissions(string roleName, [FromBody] List<SystemPermission> newPermissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return NotFound("Rol no encontrado");

            // Eliminar los permisos actuales del rol para sobreescribir la matriz
            var existingPermissions = await _context.SystemPermissions
                .Where(p => p.RoleId == role.Id)
                .ToListAsync();
            
            _context.SystemPermissions.RemoveRange(existingPermissions);

            // Asignar el nuevo RoleId a todos los permisos que llegan
            foreach (var perm in newPermissions)
            {
                perm.Id = Guid.NewGuid().ToString(); // Regenerar Id para evitar conflictos
                perm.RoleId = role.Id;
            }

            await _context.SystemPermissions.AddRangeAsync(newPermissions);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Permisos actualizados exitosamente." });
        }
    }
}
