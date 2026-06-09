using System.Threading.Tasks;
using WebTIC.API.Data;
using WebTIC.API.Models;

namespace WebTIC.API.Services
{
    public class AuditService : IAuditService
    {
        private readonly AppDbContext _context;

        public AuditService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogEventAsync(string actionType, string userId, string ipAddress, string details)
        {
            var log = new LogAuditoria
            {
                ActionType = actionType,
                UserId = userId,
                IpAddress = ipAddress,
                Details = details
            };

            _context.LogAuditoria.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
