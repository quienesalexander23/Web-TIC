using System;

namespace WebTIC.API.Models
{
    // Lista negra de tokens JWT revocados (por logout explícito). Se identifica cada
    // token por su claim 'jti' (único por emisión). ExpiresAt permite limpiar filas
    // vencidas sin necesidad de conservarlas para siempre.
    public class RevokedToken
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Jti { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime RevokedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
    }
}
