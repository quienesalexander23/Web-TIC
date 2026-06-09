using System;
using System.Threading.Tasks;

namespace WebTIC.API.Services
{
    public interface IAuditService
    {
        Task LogEventAsync(string actionType, string userId, string ipAddress, string details);
    }
}
