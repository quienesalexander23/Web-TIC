using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var alexEmail = "alexander.tibanta@epn.edu.ec";
            var alexUser = await userManager.FindByEmailAsync(alexEmail);
            if (alexUser == null)
            {
                await CreateUserIfNotExists(alexEmail, "Alexander", "Tibanta", "Administrador", "Santodomingo23!!");
            }
            else
            {
                var currentRoles = await userManager.GetRolesAsync(alexUser);
                await userManager.RemoveFromRolesAsync(alexUser, currentRoles);
                await userManager.AddToRoleAsync(alexUser, "Administrador");
            }
            await CreateUserIfNotExists("victor.velepucha@epn.edu.ec", "Victor", "Velepucha", "Docente", "Santodomingo23!!");
            
            // Usuarios para otros roles
            await CreateUserIfNotExists("presidente.cpgic@epn.edu.ec", "Presidente", "CPGIC", "Presidente CPGIC", "Santodomingo23!!");
            await CreateUserIfNotExists("miembro.cpgic@epn.edu.ec", "Miembro", "CPGIC", "Miembro CPGIC", "Santodomingo23!!");

            // 3. Sembrar Matriz de Permisos
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            // Forzar limpieza de permisos anteriores para cargar la nueva matriz TIC
            context.SystemPermissions.RemoveRange(context.SystemPermissions);
            await context.SaveChangesAsync();

            // Definir la estructura base de módulos y recursos según el documento del Proyecto TIC
            var modulesTemplate = new List<(string Module, string Resource)>
            {
                // Estudiante A: Módulo Propuestas de TIC
                ("Propuestas de TIC", "Redacción de Propuestas"),
                ("Propuestas de TIC", "Validación por CPGIC"),
                ("Propuestas de TIC", "Asignación de Estudiantes"),

                // Estudiante B: Módulo de Consultas y Reportes
                ("Consultas y Reportes", "Visor de Propuestas Aprobadas"),
                ("Consultas y Reportes", "Generador de Reportes (PDF/Excel)"),

                // Estudiante C: Módulo Gestión de Usuarios y Autenticación
                ("Gestión de Usuarios", "Gestión de Cuentas y Perfiles"),
                ("Gestión de Usuarios", "Asignación de Roles y Permisos"),
                ("Gestión de Usuarios", "Registros de Auditoría")
            };

            foreach (var roleName in roles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null) continue;

                foreach (var template in modulesTemplate)
                {
                        bool isAdmin = roleName == "Administrador";
                        bool isDocente = roleName == "Docente";
                        bool isCpgic = roleName == "Presidente CPGIC" || roleName == "Miembro CPGIC";
                        bool isStudent = roleName == "Estudiante";

                        var perm = new SystemPermission
                        {
                            Id = Guid.NewGuid().ToString(),
                            RoleId = role.Id,
                            ModuleName = template.Module,
                            ResourceName = template.Resource,
                            // Por defecto Administrador tiene todo
                            CanRead = isAdmin,
                            CanWrite = isAdmin,
                            CanApprove = isAdmin,
                            CanDelete = isAdmin
                        };

                        // Lógica de simulación para "Docente"
                        if (isDocente)
                        {
                            if (template.Resource == "Redacción de Propuestas") { perm.CanRead = true; perm.CanWrite = true; perm.CanDelete = true; }
                            if (template.Resource == "Asignación de Estudiantes") { perm.CanRead = true; perm.CanWrite = true; }
                            if (template.Module == "Consultas y Reportes") { perm.CanRead = true; }
                        }

                        // Lógica de simulación para "CPGIC"
                        if (isCpgic)
                        {
                            if (template.Resource == "Redacción de Propuestas") { perm.CanRead = true; }
                            if (template.Resource == "Validación por CPGIC") { perm.CanRead = true; perm.CanApprove = true; }
                            if (template.Module == "Consultas y Reportes") { perm.CanRead = true; perm.CanWrite = true; } // Write para generar reportes
                        }

                        // Lógica de simulación para "Estudiante"
                        if (isStudent)
                        {
                            if (template.Resource == "Visor de Propuestas Aprobadas") { perm.CanRead = true; }
                        }

                        context.SystemPermissions.Add(perm);
                    }
            }
            
            await context.SaveChangesAsync();
        }
    }
}
