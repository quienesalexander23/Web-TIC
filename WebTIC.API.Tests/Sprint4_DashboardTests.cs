using System.Diagnostics;
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
    // Pruebas de integración del Sprint 4: Dashboard Administrativo.
    // Los 10 casos documentados en Sprint4_Pruebas.md (CP4-01..10) son genéricos
    // ("Dashboard #N"); estas pruebas verifican el comportamiento real del endpoint
    // /api/dashboard/stats en lugar de mapear 1:1 a IDs sin diferenciación real.
    public class Sprint4_DashboardTests : IClassFixture<WebTicApiFactory>
    {
        private readonly WebTicApiFactory _factory;
        private readonly HttpClient _client;

        public Sprint4_DashboardTests(WebTicApiFactory factory)
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

        [Fact(DisplayName = "Sprint 4: las métricas del dashboard reflejan el estado real de usuarios y roles")]
        public async Task MetricasReflejanEstadoRealDeLaBaseDeDatos()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/dashboard/stats");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            var totalUsers = body.GetProperty("totalUsers").GetInt32();
            var activeUsers = body.GetProperty("activeUsers").GetInt32();
            var inactiveUsers = body.GetProperty("inactiveUsers").GetInt32();

            Assert.Equal(totalUsers, activeUsers + inactiveUsers);
            Assert.True(totalUsers >= 5); // los 5 usuarios sembrados por TestSeeder
            Assert.True(body.GetProperty("rolesDistribution").GetProperty("Administrador").GetInt32() >= 1);

            // activityByDay: nueva serie temporal de 7 días para la gráfica real del
            // dashboard (antes no existía ningún dato de serie temporal).
            var activityByDay = body.GetProperty("activityByDay").EnumerateArray().ToList();
            Assert.Equal(7, activityByDay.Count);
            Assert.True(activityByDay.Sum(d => d.GetProperty("count").GetInt32()) >= 1); // al menos el login de este test
        }

        [Fact(DisplayName = "Sprint 4: solo Administrador puede acceder a las métricas del dashboard (RBAC)")]
        public async Task SoloAdministradorAccedeAlDashboard()
        {
            var docenteToken = await LoginAndGetTokenAsync("docente.test@epn.edu.ec");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/dashboard/stats");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", docenteToken);

            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact(DisplayName = "Sprint 4 (documental): mide la latencia real del endpoint contra la BD InMemory (referencia, no comparable 1:1 con producción/Supabase)")]
        public async Task LatenciaDelEndpoint_Referencia()
        {
            var adminToken = await LoginAndGetTokenAsync("admin.test@epn.edu.ec");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/dashboard/stats");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            var sw = Stopwatch.StartNew();
            var response = await _client.SendAsync(request);
            sw.Stop();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Nota: contra Supabase real (medido manualmente, ver INFORME_VERIFICACION_QA.md)
            // la latencia observada fue de 1.3–4.5 segundos por el patrón N+1 en
            // DashboardController.GetDashboardStats (una consulta por cada rol) sumado a la
            // latencia de red hacia Supabase. Contra la BD InMemory de esta prueba no hay
            // latencia de red, por lo que este número NO es comparable con el de producción;
            // se deja como referencia de que el N+1 sigue ocurriendo (mismo número de queries).
        }
    }
}
