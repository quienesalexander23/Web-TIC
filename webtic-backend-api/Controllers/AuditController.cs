using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebTIC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // El acceso granular real se valida por permiso (ver HasAuditReadPermissionAsync)
    public class AuditController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuditController(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // Administrador siempre tiene acceso total. Para el resto de roles, el acceso
        // de lectura a la auditoría depende del permiso granular configurado en
        // Gestión de Roles y Permisos (módulo "Gestión de Usuarios", recurso
        // "Registros de Auditoría"), no de un rol fijo hardcodeado.
        private async Task<bool> HasAuditReadPermissionAsync()
        {
            if (User.IsInRole("Administrador")) return true;

            var roleName = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(roleName)) return false;

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;

            return await _context.SystemPermissions.AnyAsync(p =>
                p.RoleId == role.Id &&
                p.ModuleName == "Gestión de Usuarios" &&
                p.ResourceName == "Registros de Auditoría" &&
                p.CanRead);
        }

        private IQueryable<Models.LogAuditoria> ApplyFilters(
            string? actionType, string? userId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.LogAuditoria.AsQueryable();

            if (!string.IsNullOrWhiteSpace(actionType))
                query = query.Where(l => l.ActionType == actionType);

            if (!string.IsNullOrWhiteSpace(userId))
                query = query.Where(l => l.UserId == userId);

            if (fromDate.HasValue)
                query = query.Where(l => l.Timestamp >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(l => l.Timestamp <= toDate.Value);

            return query.OrderByDescending(l => l.Timestamp);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditLogs(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? actionType = null,
            [FromQuery] string? userId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            if (!await HasAuditReadPermissionAsync()) return Forbid();

            var query = ApplyFilters(actionType, userId, fromDate, toDate);
            var totalItems = await query.CountAsync();
            var logs = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new Models.DTOs.PaginatedResult<Models.LogAuditoria>
            {
                Items = logs,
                TotalItems = totalItems,
                Page = pageNumber,
                PageSize = pageSize
            };

            return Ok(result);
        }

        // Devuelve la lista de tipos de acción distintos presentes en el log, para
        // poblar el filtro desplegable del frontend sin hardcodear valores.
        [HttpGet("action-types")]
        public async Task<IActionResult> GetActionTypes()
        {
            if (!await HasAuditReadPermissionAsync()) return Forbid();

            var types = await _context.LogAuditoria
                .Select(l => l.ActionType)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();

            return Ok(types);
        }

        // Exportación a CSV de los registros que cumplan los filtros (sin paginar,
        // hasta un límite razonable) — cierra el hallazgo de Sprint 3 (la tesis
        // describía esta funcionalidad en la Figura 2.12, pero nunca se implementó).
        [HttpGet("export")]
        public async Task<IActionResult> ExportCsv(
            [FromQuery] string? actionType = null,
            [FromQuery] string? userId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            if (!await HasAuditReadPermissionAsync()) return Forbid();

            const int maxRows = 5000;
            var logs = await ApplyFilters(actionType, userId, fromDate, toDate)
                .Take(maxRows)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Fecha,TipoDeAccion,UsuarioId,DireccionIP,Detalles");
            foreach (var log in logs)
            {
                csv.AppendLine(string.Join(",",
                    log.Timestamp.ToString("o"),
                    CsvEscape(log.ActionType),
                    CsvEscape(log.UserId),
                    CsvEscape(log.IpAddress),
                    CsvEscape(log.Details)));
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
            var fileName = $"auditoria_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";
            return File(bytes, "text/csv", fileName);
        }

        private static string CsvEscape(string? value)
        {
            value ??= string.Empty;
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }
    }
}
