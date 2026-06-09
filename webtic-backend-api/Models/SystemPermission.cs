using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebTIC.API.Models
{
    public class SystemPermission
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        [MaxLength(100)]
        public string ModuleName { get; set; } = string.Empty; // Ej: "Academic Planning"

        [Required]
        [MaxLength(100)]
        public string ResourceName { get; set; } = string.Empty; // Ej: "Syllabi Management"

        // Action Flags
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanApprove { get; set; }
        public bool CanDelete { get; set; }

        [Required]
        public string RoleId { get; set; } = string.Empty;

        [ForeignKey("RoleId")]
        public virtual IdentityRole? Role { get; set; }
    }
}
