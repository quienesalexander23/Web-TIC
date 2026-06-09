using System;

namespace WebTIC.API.Models
{
    public class LogAuditoria
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ActionType { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string IpAddress { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
