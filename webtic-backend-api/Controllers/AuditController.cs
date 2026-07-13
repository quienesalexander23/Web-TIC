using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTIC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")] // Solo Administrador puede ver los logs
    public class AuditController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuditController(AppDbContext context)
        {
            _context = context;
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
            var query = ApplyFilters(actionType, userId, fromDate, toDate);
            var totalRecords = await query.CountAsync();
            var logs = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = logs
            };

            return Ok(result);
        }

        // Devuelve la lista de tipos de acción distintos presentes en el log, para
        // poblar el filtro desplegable del frontend sin hardcodear valores.
        [HttpGet("action-types")]
        public async Task<IActionResult> GetActionTypes()
        {
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
