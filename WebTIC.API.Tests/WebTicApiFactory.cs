using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WebTIC.API.Data;
using WebTIC.API.Models;
using WebTIC.API.Services;

namespace WebTIC.API.Tests
{
    // Reemplaza la base de datos real (Supabase/PostgreSQL) y el servicio de correo
    // por versiones en memoria/falsas para poder ejecutar los 66 casos de prueba
    // funcional de forma reproducible y sin depender de infraestructura externa.
    public class WebTicApiFactory : WebApplicationFactory<Program>
    {
        public readonly string DbName = Guid.NewGuid().ToString();
        public readonly FakeEmailService FakeEmail = new FakeEmailService();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                // Quitar el registro real de AppDbContext (PostgreSQL) y usar InMemory
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(DbName);
                });

                // Reemplazar el envío real de correo por una versión falsa que
                // guarda los códigos/tokens en memoria (sustituye a Gmail SMTP).
                var emailDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IEmailService));
                if (emailDescriptor != null) services.Remove(emailDescriptor);
                services.AddSingleton<IEmailService>(FakeEmail);

                // Construir el proveedor y sembrar roles + usuarios de prueba
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
                TestSeeder.SeedAsync(scopedServices).GetAwaiter().GetResult();
            });
        }
    }

    // Captura los códigos 2FA y tokens de reseteo en memoria en lugar de enviarlos
    // por correo real, para que las pruebas puedan leerlos igual que el
    // administrador lo haría desde su bandeja de entrada.
    public class FakeEmailService : IEmailService
    {
        public List<(string ToEmail, string Subject, string Body)> SentEmails { get; } = new();

        public Task SendEmailAsync(string toEmail, string subject, string message)
        {
            SentEmails.Add((toEmail, subject, message));
            return Task.CompletedTask;
        }

        public string? GetLastBodyFor(string email) =>
            SentEmails.LastOrDefault(e => e.ToEmail == email).Body;
    }

    public static class TestSeeder
    {
        public const string DefaultPassword = "Santodomingo23!!";

        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Administrador", "Docente", "Presidente CPGIC", "Miembro CPGIC", "Estudiante" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            async Task CreateUser(string email, string first, string last, string role, bool isActive = true)
            {
                var existing = await userManager.FindByEmailAsync(email);
                if (existing != null) return;

                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = first,
                    LastName = last,
                    IsActive = isActive,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, DefaultPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, role);
            }

            await CreateUser("admin.test@epn.edu.ec", "Admin", "Test", "Administrador");
            await CreateUser("docente.test@epn.edu.ec", "Docente", "Test", "Docente");
            await CreateUser("presidente.test@epn.edu.ec", "Presidente", "Test", "Presidente CPGIC");
            await CreateUser("miembro.test@epn.edu.ec", "Miembro", "Test", "Miembro CPGIC");
            await CreateUser("inactivo.test@epn.edu.ec", "Inactivo", "Test", "Docente", isActive: false);
            // Cuentas dedicadas para pruebas que mutan estado (bloqueo, reset), para no
            // interferir con otras pruebas que comparten la misma BD InMemory por clase.
            await CreateUser("lockout.test@epn.edu.ec", "Lockout", "Test", "Docente");
            await CreateUser("reset.test@epn.edu.ec", "Reset", "Test", "Miembro CPGIC");
        }
    }
}
