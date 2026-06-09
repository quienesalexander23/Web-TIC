using System.ComponentModel.DataAnnotations;

namespace WebTIC.API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El correo institucional es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }
}
