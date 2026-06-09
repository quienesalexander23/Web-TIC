using Microsoft.AspNetCore.Identity;

namespace WebTIC.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        
        // Propiedad para el borrado lógico (baja lógica) del Flujo 2
        public bool IsActive { get; set; } = true;
        
        // Para auditoría general, registrar cuándo fue creado el usuario
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
