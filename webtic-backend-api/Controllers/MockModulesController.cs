using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using System.Security.Claims;

namespace WebTIC.API.Controllers
{
    // Endpoints de solo lectura con datos de ejemplo estáticos (no hay tablas reales
    // de propuestas TIC en este backend, ya que esos módulos los desarrollan otras
    // compañeras). Sirven únicamente para demostrar que el permiso granular
    // configurado en Gestión de Roles y Permisos controla acceso real a pantallas
    // de cualquier módulo del proyecto, no solo a las del módulo Gestión de Usuarios.
    [Route("api/mock")]
    [ApiController]
    [Authorize]
    public class MockModulesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MockModulesController(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        private async Task<bool> HasReadPermissionAsync(string moduleName, string resourceName)
        {
            if (User.IsInRole("Administrador")) return true;

            var roleName = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(roleName)) return false;

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;

            return await _context.SystemPermissions.AnyAsync(p =>
                p.RoleId == role.Id &&
                p.ModuleName == moduleName &&
                p.ResourceName == resourceName &&
                p.CanRead);
        }

        // Representa el "Visor de Propuestas Aprobadas" del Módulo de Consultas y Reportes.
        [HttpGet("consultas-reportes")]
        public async Task<IActionResult> GetConsultasReportes()
        {
            if (!await HasReadPermissionAsync("Consultas y Reportes", "Visor de Propuestas Aprobadas"))
                return Forbid();

            var data = new[]
            {
                new { Titulo = "Sistema de recomendación académica con IA", Proponente = "Dr. Carlos Reyes", EstudianteAsignado = (string?)"María Fernanda López", Estado = "Asignado", FechaUltimoCambio = "2026-06-02" },
                new { Titulo = "Plataforma de monitoreo de redes IoT", Proponente = "Dra. Andrea Salazar", EstudianteAsignado = (string?)null, Estado = "Disponible", FechaUltimoCambio = "2026-06-10" },
                new { Titulo = "Automatización de pruebas de regresión visual", Proponente = "Ing. Jorge Paredes", EstudianteAsignado = (string?)"Pedro Andrade", Estado = "Asignado", FechaUltimoCambio = "2026-05-28" },
                new { Titulo = "Análisis de sentimiento en redes sociales universitarias", Proponente = "Dra. Andrea Salazar", EstudianteAsignado = (string?)null, Estado = "Disponible", FechaUltimoCambio = "2026-06-15" }
            };

            return Ok(data);
        }

        // Representa la "Validación por CPGIC" del Módulo Propuestas de TIC.
        [HttpGet("propuestas-tic")]
        public async Task<IActionResult> GetPropuestasTic()
        {
            if (!await HasReadPermissionAsync("Propuestas de TIC", "Validación por CPGIC"))
                return Forbid();

            var data = new[]
            {
                new { Titulo = "Sistema de recomendación académica con IA", DocenteProponente = "Dr. Carlos Reyes", Estado = "Aprobado", FechaEnvio = "2026-05-20" },
                new { Titulo = "Plataforma de monitoreo de redes IoT", DocenteProponente = "Dra. Andrea Salazar", Estado = "Aprobado", FechaEnvio = "2026-05-22" },
                new { Titulo = "Chatbot de soporte estudiantil", DocenteProponente = "Ing. Jorge Paredes", Estado = "Pendiente", FechaEnvio = "2026-06-01" },
                new { Titulo = "Sistema de votación electrónica para consejo estudiantil", DocenteProponente = "Dr. Carlos Reyes", Estado = "Rechazado", FechaEnvio = "2026-05-15" }
            };

            return Ok(data);
        }
    }
}
