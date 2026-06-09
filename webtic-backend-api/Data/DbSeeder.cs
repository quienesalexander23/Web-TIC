using Microsoft.AspNetCore.Identity;
using WebTIC.API.Models;

namespace WebTIC.API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // 1. Crear Roles
            string[] roles = new[] { "Administrador", "Docente", "Presidente CPGIC", "Miembro CPGIC", "Estudiante" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // 2. Crear Usuarios de Prueba

            // Función auxiliar para crear usuarios
            async Task CreateUserIfNotExists(string email, string firstName, string lastName, string role, string password)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }

            // Usuario Administrador Base
            await CreateUserIfNotExists("admin.webtic@epn.edu.ec", "Admin", "Sistema", "Administrador", "Santodomingo23!!");

            // Usuarios solicitados para pruebas
            await CreateUserIfNotExists("alexander.tibanta@epn.edu.ec", "Alexander", "Tibanta", "Estudiante", "Santodomingo23!!");
            await CreateUserIfNotExists("victor.velepucha@epn.edu.ec", "Victor", "Velepucha", "Docente", "Santodomingo23!!");
            
            // Usuarios para otros roles
            await CreateUserIfNotExists("presidente.cpgic@epn.edu.ec", "Presidente", "CPGIC", "Presidente CPGIC", "Santodomingo23!!");
            await CreateUserIfNotExists("miembro.cpgic@epn.edu.ec", "Miembro", "CPGIC", "Miembro CPGIC", "Santodomingo23!!");
        }
    }
}
