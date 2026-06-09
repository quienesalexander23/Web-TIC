using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Data;
using System.Linq;
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

        [HttpGet]
        public async Task<IActionResult> GetAuditLogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.LogAuditoria.OrderByDescending(l => l.Timestamp);
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
    }
}
