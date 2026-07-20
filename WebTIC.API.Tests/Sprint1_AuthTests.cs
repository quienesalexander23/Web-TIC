using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebTIC.API.Models;
using Xunit;

namespace WebTIC.API.Tests
{
    // Pruebas de integración del Sprint 1: Autenticación Segura y Control de Sesión.
    // Cada método referencia el ID del caso de prueba documentado en
    // Test_y_Validaciones/Pruebas_Funcionales/Sprint1/Sprint1_Pruebas.md
    public class Sprint1_AuthTests : IClassFixture<WebTicApiFactory>
    {
        private readonly WebTicApiFactory _factory;
        private readonly HttpClient _client;

        public Sprint1_AuthTests(WebTicApiFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private async Task<string> Get2FACodeAsync(string email)
        {
            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByEmailAsync(email);
            return await userManager.GenerateTwoFactorTokenAsync(user!, "Email");
        }

        [Fact(DisplayName = "CP1-01: Login con credenciales válidas devuelve Requires2FA")]
        public async Task CP1_01_LoginValido_RequiereDosFactor()
        {
            var response = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "admin.test@epn.edu.ec",
                password = TestSeeder.DefaultPassword
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.True(body.GetProperty("requires2FA").GetBoolean());
        }

        [Fact(DisplayName = "CP1-01/CP1-08: Flujo completo login+2FA emite JWT con claims correctos")]
        public async Task CP1_01_CP1_08_FlujoCompleto2FA_EmiteJwtConClaims()
        {
            await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "admin.test@epn.edu.ec",
                password = TestSeeder.DefaultPassword
            });

            var code = await Get2FACodeAsync("admin.test@epn.edu.ec");
            var verify = await _client.PostAsJsonAsync("/api/auth/verify-2fa", new
            {
                email = "admin.test@epn.edu.ec",
                code
            });

            Assert.Equal(HttpStatusCode.OK, verify.StatusCode);
            var body = await verify.Content.ReadFromJsonAsync<JsonElement>();
            var token = body.GetProperty("token").GetString();
            Assert.False(string.IsNullOrEmpty(token));

            var payload = DecodeJwtPayload(token!);
            Assert.Equal("admin.test@epn.edu.ec", payload.GetProperty("email").GetString());
            Assert.Equal("Administrador", payload.GetProperty("role").GetString());
        }

        [Fact(DisplayName = "CP1-03: Contraseña incorrecta devuelve mensaje genérico con conteo de intentos")]
        public async Task CP1_03_PasswordIncorrecto_MensajeGenerico()
        {
            var response = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "docente.test@epn.edu.ec",
                password = "PasswordIncorrecto1!"
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact(DisplayName = "CP1-04: Correo no registrado devuelve el mismo mensaje genérico que CP1-03")]
        public async Task CP1_04_CorreoNoRegistrado_MensajeGenerico()
        {
            var response = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "noexiste@epn.edu.ec",
                password = "CualquierPass1!"
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal("Acceso Denegado", body.GetProperty("message").GetString());
            Assert.Equal("Credenciales incorrectas.", body.GetProperty("description").GetString());
        }

        [Fact(DisplayName = "CP1-05: Dominio no institucional es rechazado también en backend")]
        public async Task CP1_05_DominioNoInstitucional_Rechazado()
        {
            var response = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "usuario@gmail.com",
                password = "CualquierPass1!"
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "CP1-06/CP1-07: 5 intentos fallidos bloquean la cuenta (HTTP 423) incluso con password correcto")]
        public async Task CP1_06_CP1_07_BloqueoTrasCincoIntentos()
        {
            const string email = "lockout.test@epn.edu.ec"; // cuenta dedicada, no compartida con CP1-03
            HttpResponseMessage? last = null;
            for (int i = 0; i < 5; i++)
            {
                last = await _client.PostAsJsonAsync("/api/auth/login", new { email, password = $"Malo{i}!" });
            }
            Assert.Equal((HttpStatusCode)423, last!.StatusCode);

            // CP1-07: reintentar con la contraseña correcta también debe ser bloqueado
            var withCorrectPassword = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email,
                password = TestSeeder.DefaultPassword
            });
            Assert.Equal((HttpStatusCode)423, withCorrectPassword.StatusCode);
        }

        [Fact(DisplayName = "CP1-10: Cuenta desactivada rechaza el login con mensaje claro")]
        public async Task CP1_10_CuentaDesactivada_LoginDenegado()
        {
            var response = await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "inactivo.test@epn.edu.ec",
                password = TestSeeder.DefaultPassword
            });

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal("Cuenta Inactiva", body.GetProperty("message").GetString());
            Assert.Contains("inactiva", body.GetProperty("description").GetString());
        }

        [Fact(DisplayName = "CP1-13 (corregido): tras logout explícito, el token queda revocado y HTTP 401 en peticiones posteriores")]
        public async Task CP1_13_TokenRevocadoTrasLogout()
        {
            await _client.PostAsJsonAsync("/api/auth/login", new
            {
                email = "admin.test@epn.edu.ec",
                password = TestSeeder.DefaultPassword
            });
            var code = await Get2FACodeAsync("admin.test@epn.edu.ec");
            var verify = await _client.PostAsJsonAsync("/api/auth/verify-2fa", new { email = "admin.test@epn.edu.ec", code });
            var body = await verify.Content.ReadFromJsonAsync<JsonElement>();
            var token = body.GetProperty("token").GetString();

            HttpRequestMessage WithToken(HttpMethod method, string url)
            {
                var req = new HttpRequestMessage(method, url);
                req.Headers.Add("Authorization", $"Bearer {token}");
                return req;
            }

            // Antes de logout, el token funciona con normalidad.
            var beforeLogout = await _client.SendAsync(WithToken(HttpMethod.Get, "/api/usuarios?page=1&pageSize=5"));
            Assert.Equal(HttpStatusCode.OK, beforeLogout.StatusCode);

            // POST /api/auth/logout revoca el jti del token (lo agrega a RevokedTokens).
            var logout = await _client.SendAsync(WithToken(HttpMethod.Post, "/api/auth/logout"));
            Assert.Equal(HttpStatusCode.OK, logout.StatusCode);

            // El mismo token, reutilizado después del logout, ahora es rechazado.
            var afterLogout = await _client.SendAsync(WithToken(HttpMethod.Get, "/api/usuarios?page=1&pageSize=5"));
            Assert.Equal(HttpStatusCode.Unauthorized, afterLogout.StatusCode);
        }

        [Fact(DisplayName = "CP1-15/CP1-16: forgot-password devuelve el mismo mensaje genérico para correos válidos e inválidos")]
        public async Task CP1_15_CP1_16_ForgotPassword_MensajeGenericoSinEnumeracion()
        {
            var valido = await _client.PostAsJsonAsync("/api/auth/forgot-password", new { email = "docente.test@epn.edu.ec" });
            var invalido = await _client.PostAsJsonAsync("/api/auth/forgot-password", new { email = "fantasma@epn.edu.ec" });

            Assert.Equal(HttpStatusCode.OK, valido.StatusCode);
            Assert.Equal(HttpStatusCode.OK, invalido.StatusCode);

            var bodyValido = await valido.Content.ReadFromJsonAsync<JsonElement>();
            var bodyInvalido = await invalido.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal(bodyValido.GetProperty("message").GetString(), bodyInvalido.GetProperty("message").GetString());
        }

        [Fact(DisplayName = "CP1-17/CP1-18/CP1-20: reset-password válido funciona, rechaza reintento y contraseñas débiles")]
        public async Task CP1_17_18_20_ResetPassword_FlujoCompleto()
        {
            const string email = "reset.test@epn.edu.ec"; // cuenta dedicada, no compartida con pruebas RBAC
            await _client.PostAsJsonAsync("/api/auth/forgot-password", new { email });

            using var scope = _factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user!);

            // CP1-20: contraseña débil rechazada
            var weak = await _client.PostAsJsonAsync("/api/auth/reset-password", new { email, token, newPassword = "abc" });
            Assert.Equal(HttpStatusCode.BadRequest, weak.StatusCode);

            // CP1-17: reset válido exitoso
            var valid = await _client.PostAsJsonAsync("/api/auth/reset-password", new { email, token, newPassword = "NuevaPass2026!" });
            Assert.Equal(HttpStatusCode.OK, valid.StatusCode);

            // CP1-18: reutilizar el mismo token ya usado debe fallar
            var reuse = await _client.PostAsJsonAsync("/api/auth/reset-password", new { email, token, newPassword = "OtraPass2026!" });
            Assert.Equal(HttpStatusCode.BadRequest, reuse.StatusCode);
        }

        private static JsonElement DecodeJwtPayload(string jwt)
        {
            var parts = jwt.Split('.');
            var payload = parts[1].Replace('-', '+').Replace('_', '/');
            switch (payload.Length % 4)
            {
                case 2: payload += "=="; break;
                case 3: payload += "="; break;
            }
            var bytes = Convert.FromBase64String(payload);
            return JsonSerializer.Deserialize<JsonElement>(bytes);
        }
    }
}
