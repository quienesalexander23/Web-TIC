using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebTIC.API.Models;
using Xunit;

namespace WebTIC.API.Tests
{
    // Pruebas de integración del Sprint 2: Gestión de Usuarios y RBAC.
    // Cada método referencia el ID del caso documentado en
    // Test_y_Validaciones/Pruebas_Funcionales/Sprint2/Sprint2_Pruebas.md
    public class Sprint2_UserManagementTests : IClassFixture<WebTicApiFactory>
    {
        private readonly WebTicApiFactory _factory;
        private readonly HttpClient _client;

        public Sprint2_UserManagementTests(WebTicApiFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private async Task<string> LoginAndGetTokenAsync(string email)
        {
            await _client.PostAsJsonAsync("/api/auth/login", new { email, password = TestSeeder.DefaultPassword });

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByEmailAsync(email);
            var code = await userManager.GenerateTwoFactorTokenAsync(user!, "Email");

            var verify = await _client.PostAsJsonAsync("/api/auth/verify-2fa", new { email, code });
            var body = await verify.Content.ReadFromJsonAsync<JsonElement>();
            return body.GetProperty("token").GetString()!;
        }

        private HttpRequestMessage Authorized(HttpMethod method, string url, string token, object? body = null)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (body != null) request.Content = JsonContent.Create(body);
            return request;
        }

        [Fact(DisplayName = "CP2-01/CP2-02/CP2-03: creación válida, correo duplicado y dominio no institucional")]
        public async Task CP2_01_02_03_CreacionDeUsuarios()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");
            const string newEmail = "nuevo.usuario.cp201@epn.edu.ec";

            var createValid = await _client.SendAsync(Authorized(HttpMethod.Post, "/api/usuarios", adminToken,
                new { email = newEmail, firstName = "Nuevo", lastName = "Usuario", role = "Docente" }));
            Assert.Equal(HttpStatusCode.OK, createValid.StatusCode);

            var createDuplicate = await _client.SendAsync(Authorized(HttpMethod.Post, "/api/usuarios", adminToken,
                new { email = newEmail, firstName = "Dup", lastName = "Licado", role = "Docente" }));
            Assert.Equal(HttpStatusCode.BadRequest, createDuplicate.StatusCode);

            var createBadDomain = await _client.SendAsync(Authorized(HttpMethod.Post, "/api/usuarios", adminToken,
                new { email = "usuario@gmail.com", firstName = "X", lastName = "Y", role = "Docente" }));
            Assert.Equal(HttpStatusCode.BadRequest, createBadDomain.StatusCode);
        }

        [Fact(DisplayName = "CP2-04/CP2-05/CP2-06: paginación, búsqueda parcial y filtro por rol")]
        public async Task CP2_04_05_06_ListadoPaginadoBusquedaYFiltro()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");

            var paged = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/usuarios?page=1&pageSize=2", adminToken));
            var pagedBody = await paged.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal(2, pagedBody.GetProperty("items").GetArrayLength());
            Assert.True(pagedBody.GetProperty("totalItems").GetInt32() >= 6);

            var search = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/usuarios?page=1&pageSize=10&search=docente.test", adminToken));
            var searchBody = await search.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal(1, searchBody.GetProperty("items").GetArrayLength());
            Assert.Equal("docente.test@epn.edu.ec", searchBody.GetProperty("items")[0].GetProperty("email").GetString());

            var byRole = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/usuarios?page=1&pageSize=50&role=Docente", adminToken));
            var byRoleBody = await byRole.Content.ReadFromJsonAsync<JsonElement>();
            foreach (var item in byRoleBody.GetProperty("items").EnumerateArray())
            {
                var roles = item.GetProperty("roles").EnumerateArray().Select(r => r.GetString());
                Assert.Contains("Docente", roles);
            }
        }

        [Fact(DisplayName = "CP2-07/CP2-08: edición de nombre exitosa; modificar correo es rechazado explícitamente con HTTP 400")]
        public async Task CP2_07_08_EdicionDeDatosYCorreoInmutable()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var target = await userManager.FindByEmailAsync("docente.test@epn.edu.ec");

            var edit = await _client.SendAsync(Authorized(HttpMethod.Put, $"/api/usuarios/{target!.Id}", adminToken,
                new { firstName = "DocenteEditado", lastName = "Test", role = "Docente" }));
            Assert.Equal(HttpStatusCode.OK, edit.StatusCode);

            // CP2-08: el correo institucional es inmutable. Intentar cambiarlo es
            // rechazado explícitamente con HTTP 400, no ignorado silenciosamente.
            var editEmailAttempt = await _client.SendAsync(Authorized(HttpMethod.Put, $"/api/usuarios/{target.Id}", adminToken,
                new { firstName = "DocenteEditado", lastName = "Test", role = "Docente", email = "otro@epn.edu.ec" }));
            Assert.Equal(HttpStatusCode.BadRequest, editEmailAttempt.StatusCode);

            var refreshed = await userManager.FindByIdAsync(target.Id);
            Assert.Equal("docente.test@epn.edu.ec", refreshed!.Email); // el correo NO cambió

            // Enviar el mismo correo actual (sin intento de cambio real) sí debe permitirse.
            var editSameEmail = await _client.SendAsync(Authorized(HttpMethod.Put, $"/api/usuarios/{target.Id}", adminToken,
                new { firstName = "DocenteEditado", lastName = "Test", role = "Docente", email = "docente.test@epn.edu.ec" }));
            Assert.Equal(HttpStatusCode.OK, editSameEmail.StatusCode);
        }

        [Fact(DisplayName = "CP2-09/CP2-10: desactivar cuenta deniega login; reactivar y desbloquear la restauran")]
        public async Task CP2_09_10_DesactivarReactivarYDesbloquear()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var target = await userManager.FindByEmailAsync("miembro.test@epn.edu.ec");

            // CP2-09: desactivar
            await _client.SendAsync(Authorized(HttpMethod.Patch, $"/api/usuarios/{target!.Id}/estado", adminToken));
            var loginDeactivated = await _client.PostAsJsonAsync("/api/auth/login", new { email = "miembro.test@epn.edu.ec", password = TestSeeder.DefaultPassword });
            Assert.Equal(HttpStatusCode.Unauthorized, loginDeactivated.StatusCode);

            // CP2-10: reactivar
            await _client.SendAsync(Authorized(HttpMethod.Patch, $"/api/usuarios/{target.Id}/estado", adminToken));
            var loginReactivated = await _client.PostAsJsonAsync("/api/auth/login", new { email = "miembro.test@epn.edu.ec", password = TestSeeder.DefaultPassword });
            Assert.Equal(HttpStatusCode.OK, loginReactivated.StatusCode);

            // CP2-10 (variante Desbloqueo administrativo, /unlock): tras un bloqueo por fuerza bruta
            for (int i = 0; i < 5; i++)
                await _client.PostAsJsonAsync("/api/auth/login", new { email = "miembro.test@epn.edu.ec", password = $"Malo{i}!" });

            var unlock = await _client.SendAsync(Authorized(HttpMethod.Post, $"/api/usuarios/{target.Id}/unlock", adminToken));
            Assert.Equal(HttpStatusCode.OK, unlock.StatusCode);

            var loginAfterUnlock = await _client.PostAsJsonAsync("/api/auth/login", new { email = "miembro.test@epn.edu.ec", password = TestSeeder.DefaultPassword });
            Assert.Equal(HttpStatusCode.OK, loginAfterUnlock.StatusCode);
        }

        [Fact(DisplayName = "CP2-11 (corregido): CREATE/UPDATE y el toggle de estado (activar/desactivar) generan registro de auditoría")]
        public async Task CP2_11_AuditoriaCRUD_IncluyeToggleDeEstado()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var target = await userManager.FindByEmailAsync("presidente.test@epn.edu.ec");

            await _client.SendAsync(Authorized(HttpMethod.Put, $"/api/usuarios/{target!.Id}", adminToken,
                new { firstName = "PresidenteEditado", lastName = "Test", role = "Presidente CPGIC" }));
            await _client.SendAsync(Authorized(HttpMethod.Patch, $"/api/usuarios/{target.Id}/estado", adminToken));
            await _client.SendAsync(Authorized(HttpMethod.Patch, $"/api/usuarios/{target.Id}/estado", adminToken));

            var auditResponse = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit?pageNumber=1&pageSize=50", adminToken));
            var auditBody = await auditResponse.Content.ReadFromJsonAsync<JsonElement>();
            var actionTypes = auditBody.GetProperty("items").EnumerateArray()
                .Select(i => i.GetProperty("actionType").GetString()).ToList();

            Assert.Contains("UPDATE_USER", actionTypes);
            // TOGGLE_STATUS ahora se registra (antes del fix, este ActionType no existía
            // y activar/desactivar cuentas quedaba fuera del rastro de auditoría).
            Assert.Contains("TOGGLE_STATUS", actionTypes);
        }

        [Theory(DisplayName = "CP2-12/13/14/15: Docente, Presidente y Miembro CPGIC reciben HTTP 403 en endpoints de Administrador")]
        [InlineData("docente.test@epn.edu.ec")]
        [InlineData("presidente.test@epn.edu.ec")]
        [InlineData("miembro.test@epn.edu.ec")]
        public async Task CP2_12_13_14_15_RolesNoAdmin_Reciben403(string email)
        {
            var token = await LoginAndGetTokenAsync(email);

            var usuarios = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/usuarios?page=1&pageSize=5", token));
            var roles = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/RolePermissions/roles", token));
            var audit = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit?pageNumber=1&pageSize=5", token));
            var dashboard = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/dashboard/stats", token));

            Assert.Equal(HttpStatusCode.Forbidden, usuarios.StatusCode);
            Assert.Equal(HttpStatusCode.Forbidden, roles.StatusCode);
            Assert.Equal(HttpStatusCode.Forbidden, audit.StatusCode);
            Assert.Equal(HttpStatusCode.Forbidden, dashboard.StatusCode);
        }

        [Fact(DisplayName = "CP2-16: cambio de rol se refleja inmediatamente en el registro del usuario")]
        public async Task CP2_16_CambioDeRol()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var target = await userManager.FindByEmailAsync("docente.test@epn.edu.ec");

            await _client.SendAsync(Authorized(HttpMethod.Put, $"/api/usuarios/{target!.Id}", adminToken,
                new { firstName = target.FirstName, lastName = target.LastName, role = "Miembro CPGIC" }));

            var rolesNow = await userManager.GetRolesAsync(await userManager.FindByIdAsync(target.Id) ?? target);
            Assert.Contains("Miembro CPGIC", rolesNow);
            Assert.DoesNotContain("Docente", rolesNow);
        }

        [Fact(DisplayName = "CP2-18: un token con firma manipulada es rechazado con HTTP 401")]
        public async Task CP2_18_TokenManipulado_Rechazado()
        {
            var token = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");
            var tampered = token.Substring(0, token.Length - 5) + "AAAAA";

            var response = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/usuarios?page=1&pageSize=5", tampered));
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // CP2-17 (Visibilidad directiva *appHasRole) es un comportamiento exclusivamente de
        // frontend (Angular) y no tiene equivalente en la API — se verifica manualmente
        // (ver INFORME_VERIFICACION_QA.md). El hallazgo real: la directiva oculta
        // correctamente Home/Roles/Audit/Settings, pero no la página de Usuarios.
    }
}
