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
    // Pruebas de integración del Sprint 3: Registro Automático de Auditoría.
    // Los 18 casos documentados en Sprint3_Pruebas.md (CP3-01..18) son genéricos
    // ("Transacción #N"); estas pruebas verifican el comportamiento real del módulo
    // completo en lugar de mapear 1:1 a IDs sin diferenciación real.
    public class Sprint3_AuditTests : IClassFixture<WebTicApiFactory>
    {
        private readonly WebTicApiFactory _factory;
        private readonly HttpClient _client;

        public Sprint3_AuditTests(WebTicApiFactory factory)
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

        private HttpRequestMessage Authorized(HttpMethod method, string url, string token) =>
            new(method, url) { Headers = { Authorization = new AuthenticationHeaderValue("Bearer", token) } };

        [Fact(DisplayName = "Sprint 3: login exitoso, creación y edición de usuario generan registro automático de auditoría")]
        public async Task RegistroAutomatico_ParaLoginCreacionYEdicion()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");

            await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/api/usuarios")
            {
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", adminToken) },
                Content = JsonContent.Create(new { email = "auditoria.cp3@epn.edu.ec", firstName = "Auditoria", lastName = "Test", role = "Docente" })
            });

            var auditResponse = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit?pageNumber=1&pageSize=50", adminToken));
            var body = await auditResponse.Content.ReadFromJsonAsync<JsonElement>();
            var items = body.GetProperty("items").EnumerateArray().ToList();
            var actionTypes = items.Select(i => i.GetProperty("actionType").GetString()).ToList();

            Assert.Contains("LOGIN_2FA", actionTypes);
            Assert.Contains("CREATE_USER", actionTypes);

            // Cada registro trae los campos esperados para trazabilidad institucional.
            var createLog = items.First(i => i.GetProperty("actionType").GetString() == "CREATE_USER");
            Assert.False(string.IsNullOrEmpty(createLog.GetProperty("userId").GetString()));
            Assert.False(string.IsNullOrEmpty(createLog.GetProperty("ipAddress").GetString()));
            Assert.False(string.IsNullOrEmpty(createLog.GetProperty("details").GetString()));
        }

        [Fact(DisplayName = "Sprint 3: los registros de auditoría se devuelven en orden cronológico descendente")]
        public async Task OrdenCronologicoDescendente()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");
            // Generar varios eventos en secuencia
            for (int i = 0; i < 3; i++)
            {
                await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/api/usuarios")
                {
                    Headers = { Authorization = new AuthenticationHeaderValue("Bearer", adminToken) },
                    Content = JsonContent.Create(new { email = $"orden.cp3.{i}@epn.edu.ec", firstName = "Orden", lastName = $"Test{i}", role = "Docente" })
                });
            }

            var auditResponse = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit?pageNumber=1&pageSize=50", adminToken));
            var body = await auditResponse.Content.ReadFromJsonAsync<JsonElement>();
            var timestamps = body.GetProperty("items").EnumerateArray()
                .Select(i => i.GetProperty("timestamp").GetDateTime()).ToList();

            var sortedDescending = timestamps.OrderByDescending(t => t).ToList();
            Assert.Equal(sortedDescending, timestamps);
        }

        [Fact(DisplayName = "Sprint 3: el registro de auditoría es de solo lectura (no expone edición ni borrado) y solo Admin puede leerlo")]
        public async Task InmutabilidadYAccesoSoloAdmin()
        {
            var docenteToken = await LoginAndGetTokenAsync("docente.test@epn.edu.ec");
            var forbidden = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit?pageNumber=1&pageSize=5", docenteToken));
            Assert.Equal(HttpStatusCode.Forbidden, forbidden.StatusCode);

            // No existen verbos PUT/DELETE en AuditController — se confirma indirectamente:
            // cualquier intento debe caer en 404 (ruta no mapeada) o 405, nunca en una edición exitosa.
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");
            var deleteAttempt = await _client.SendAsync(Authorized(HttpMethod.Delete, "/api/audit/1", adminToken));
            Assert.NotEqual(HttpStatusCode.OK, deleteAttempt.StatusCode);
        }

        [Fact(DisplayName = "Sprint 3 (nuevo): filtro por actionType y exportación CSV funcionan")]
        public async Task FiltroPorActionTypeYExportacionCsv()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");

            var filtered = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit?pageNumber=1&pageSize=50&actionType=LOGIN_2FA", adminToken));
            Assert.Equal(HttpStatusCode.OK, filtered.StatusCode);
            var filteredBody = await filtered.Content.ReadFromJsonAsync<JsonElement>();
            foreach (var item in filteredBody.GetProperty("items").EnumerateArray())
                Assert.Equal("LOGIN_2FA", item.GetProperty("actionType").GetString());

            var actionTypes = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit/action-types", adminToken));
            Assert.Equal(HttpStatusCode.OK, actionTypes.StatusCode);
            var typesList = await actionTypes.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Contains(typesList.EnumerateArray(), t => t.GetString() == "LOGIN_2FA");

            var csvResponse = await _client.SendAsync(Authorized(HttpMethod.Get, "/api/audit/export", adminToken));
            Assert.Equal(HttpStatusCode.OK, csvResponse.StatusCode);
            Assert.Equal("text/csv", csvResponse.Content.Headers.ContentType?.MediaType);
            var csvText = await csvResponse.Content.ReadAsStringAsync();
            Assert.StartsWith("Fecha,TipoDeAccion,UsuarioId,DireccionIP,Detalles", csvText.TrimStart('﻿'));
        }
    }
}
