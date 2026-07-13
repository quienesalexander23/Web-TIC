using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTIC.API.Models;
using WebTIC.API.Models.DTOs;
using System.Security.Claims;

namespace WebTIC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")] // Solo administradores pueden gestionar usuarios
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WebTIC.API.Services.IEmailService _emailService;
        private readonly WebTIC.API.Services.IAuditService _auditService;

        public UsuariosController(UserManager<ApplicationUser> userManager, WebTIC.API.Services.IEmailService emailService, WebTIC.API.Services.IAuditService auditService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] string? role = null, [FromQuery] bool? isActive = null)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(u => u.Email.ToLower().Contains(search) || 
                                         u.FirstName.ToLower().Contains(search) || 
                                         u.LastName.ToLower().Contains(search));
            }

            if (isActive.HasValue)
            {
                query = query.Where(u => u.IsActive == isActive.Value);
            }

            var totalItems = await query.CountAsync();

            var users = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userDtos = new List<UsuarioDto>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                
                // Si hay un filtro por rol, excluimos los que no coincidan
                if (!string.IsNullOrEmpty(role) && !userRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
                {
                    totalItems--; // Ajuste simple para paginación
                    continue;
                }

                userDtos.Add(new UsuarioDto
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = user.IsActive,
                    IsLockedOut = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow,
                    Roles = userRoles.ToList()
                });
            }

            var result = new PaginatedResult<UsuarioDto>
            {
                Items = userDtos,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioDto dto)
        {
            if (!dto.Email.EndsWith("@epn.edu.ec"))
                return BadRequest(new { message = "El correo debe ser del dominio @epn.edu.ec" });

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest(new { message = "El correo ya está registrado" });

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsActive = true,
                EmailConfirmed = true
            };

            // Contraseña temporal segura (mínimo 8, 1 mayúscula, 1 número)
            string temporalPassword = "WebTIC_" + Guid.NewGuid().ToString().Substring(0, 8) + "!";
            
            var result = await _userManager.CreateAsync(user, temporalPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            // Enviar la contraseña por correo electrónico
            var loginLink = "http://localhost:4200/login";
            var emailBody = $@"
                <h2>Bienvenido a Web-TIC EPN</h2>
                <p>Hola {user.FirstName} {user.LastName},</p>
                <p>Se ha creado una cuenta para ti en el sistema Web-TIC. A continuación tus credenciales de acceso:</p>
                <ul>
                    <li><strong>Correo Institucional:</strong> {user.Email}</li>
                    <li><strong>Contraseña Temporal:</strong> {temporalPassword}</li>
                </ul>
                <p>Puedes iniciar sesión en el siguiente enlace:</p>
                <p><a href='{loginLink}'>{loginLink}</a></p>
                <br>
                <p>Por tu seguridad, te recomendamos restablecer esta contraseña desde la pantalla de inicio de sesión utilizando la opción '¿Olvidaste tu contraseña?'.</p>
            ";

            try
            {
                await _emailService.SendEmailAsync(user.Email, "Credenciales de Acceso - WebTIC", emailBody);
            }
            catch (Exception ex)
            {
                // No bloquear la creación del usuario si el proveedor de correo falla temporalmente.
                Console.WriteLine($"[EmailService] Envío de credenciales falló para {user.Email}: {ex.Message}");
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) ?? "System";
            await _auditService.LogEventAsync("CREATE_USER", currentUserId, ipAddress, $"Creó al usuario {user.Email}");

            return Ok(new { message = "Usuario creado exitosamente. Se ha enviado un correo con las credenciales." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(string id, [FromBody] UpdateUsuarioDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) return BadRequest(updateResult.Errors);

            // Manejo del Rol
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(dto.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!string.IsNullOrEmpty(dto.Role))
                {
                    await _userManager.AddToRoleAsync(user, dto.Role);
                }
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) ?? "System";
            await _auditService.LogEventAsync("UPDATE_USER", currentUserId, ipAddress, $"Actualizó al usuario {user.Email}");

            return Ok(new { message = "Usuario actualizado exitosamente" });
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> ToggleEstadoUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive; // Toggle lógico
            await _userManager.UpdateAsync(user);

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) ?? "System";
            var accion = user.IsActive ? "activó" : "desactivó";
            await _auditService.LogEventAsync("TOGGLE_STATUS", currentUserId, ipAddress, $"El administrador {accion} al usuario {user.Email}");

            return Ok(new { message = $"Usuario {(user.IsActive ? "activado" : "desactivado")} exitosamente", isActive = user.IsActive });
        }

        [HttpPost("{id}/unlock")]
        public async Task<IActionResult> UnlockUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound(new { message = "Usuario no encontrado" });

            // 1. Quitar el bloqueo y reiniciar intentos
            await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.ResetAccessFailedCountAsync(user);

            // 2. Generar Token para restablecer contraseña
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var resetLink = $"http://localhost:4200/reset-password?email={user.Email}&token={encodedToken}";

            // 3. Enviar correo de recuperación
            var emailBody = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e5e7eb; border-radius: 8px; overflow: hidden;'>
                    <div style='background-color: #00346F; padding: 20px; text-align: center;'>
                        <h1 style='color: white; margin: 0; font-size: 24px;'>Sistema Web-TIC</h1>
                    </div>
                    <div style='padding: 30px; color: #333;'>
                        <h2 style='color: #00346F; margin-top: 0;'>Cuenta Desbloqueada</h2>
                        <p>Hola {user.FirstName},</p>
                        <p>El administrador del sistema ha desbloqueado tu cuenta institucional.</p>
                        <p>Por motivos de seguridad, es necesario que restablezcas tu contraseña antes de volver a ingresar. Haz clic en el siguiente botón para crear una nueva:</p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{resetLink}' style='background-color: #00346F; color: white; padding: 12px 24px; text-decoration: none; border-radius: 4px; font-weight: bold; display: inline-block;'>Restablecer Contraseña</a>
                        </div>
                        <p style='font-size: 12px; color: #6b7280; margin-top: 30px;'>Si tienes algún inconveniente, contacta al administrador.</p>
                    </div>
                </div>";

            try
            {
                await _emailService.SendEmailAsync(user.Email!, "Acceso Restaurado", emailBody);
            }
            catch (Exception ex)
            {
                // No bloquear el desbloqueo de la cuenta si el proveedor de correo falla temporalmente.
                Console.WriteLine($"[EmailService] Envío de notificación de desbloqueo falló para {user.Email}: {ex.Message}");
            }

            // 4. Auditoría
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) ?? "System";
            await _auditService.LogEventAsync("UNLOCK_USER", currentUserId, ipAddress, $"Desbloqueó al usuario {user.Email} y envió enlace de recuperación");

            return Ok(new { message = "Cuenta desbloqueada. Se ha enviado un correo al usuario para que recupere su contraseña." });
        }
    }
}
