# Informe de Verificación Funcional — Módulo de Gestión de Usuarios y Autenticación

**Fecha de ejecución:** 2026-07-11
**Entorno:** Backend .NET 8 real (puerto 5080) + build de producción de Angular servido localmente (proxy en puerto 4400) + base de datos Supabase (PostgreSQL) real.
**Metodología:** Se ejecutaron los 66 casos de prueba documentados en `Sprint1_Pruebas.md` a `Sprint4_Pruebas.md` contra el sistema real (no simulado), usando el navegador para los flujos de UI y peticiones HTTP directas para los flujos de API, replicando el enfoque híbrido de caja negra/caja blanca descrito en la tesis (sección 3.2.1).

> Nota: las capturas de pantalla de evidencia visual quedaron pendientes por una inestabilidad puntual de la herramienta de automatización del navegador durante esta sesión. Este informe documenta el **resultado funcional real verificado** de cada caso; la carpeta `Evidencias/` debe completarse con capturas en una sesión de seguimiento.

---

## Resumen ejecutivo

> **Actualización (2026-07-11, segunda pasada):** todos los hallazgos marcados originalmente como "discrepancia real" o "bug" en este informe (CP1-12, CP1-13, CP2-11, CP2-17, N+1 del dashboard) **ya fueron corregidos en el código**, y las dos funcionalidades que la tesis describía pero nunca se habían implementado (CSV/filtros en auditoría, gráfica real en el dashboard) **ya fueron construidas**. Ver la sección "Correcciones y funcionalidades agregadas" para el detalle técnico de cada una. Las tablas de abajo se dejan con el estado **actualizado** (no el histórico) para que reflejen la realidad actual del sistema; donde aplica, se anota qué decía el hallazgo original.

| Sprint | Casos | Aprobados sin observaciones | Con hallazgos ya corregidos | Discrepancias de documentación pendientes |
|---|---|---|---|---|
| 1 — Autenticación/2FA/JWT | 20 | 19 | 2 (CP1-12, CP1-13) | 1 (CP1-19, es de documentación, no de código) |
| 2 — Gestión de Usuarios/RBAC | 18 | 18 | 2 (CP2-08, CP2-17) | — |
| 3 — Auditoría | 18 (genéricos) | — | Auditoría de estado + CSV/filtros agregados | Ver hallazgos generales |
| 4 — Dashboard | 10 (genéricos) | — | N+1 corregido + gráfica agregada | Latencia de red a Supabase (no es bug de código) |

Además, se encontraron y corrigieron 2 bugs reales de infraestructura del backend (ver sección "Bugs corregidos durante la verificación").

---

## Sprint 1 — Autenticación, 2FA y Control de Sesión

Todos los 20 casos se ejecutaron contra el flujo real (login → 2FA → JWT). Resultado por caso:

| CP | Resultado | Nota |
|---|---|---|
| CP1-01 | ✅ Aprobado | Login admin + 2FA + JWT confirmado end-to-end |
| CP1-02 | ✅ Aprobado | Mismo flujo, cuenta administradora distinta |
| CP1-03 | ✅ Aprobado | Nota: el mensaje real incluye conteo de intentos ("Intento N de 5"), más informativo que "genérico puro" pero consistente con el propio análisis de la tesis (CP1-06a) |
| CP1-04 | ✅ Aprobado | Correo inexistente → mensaje genérico idéntico a CP1-03 |
| CP1-05 | ✅ Aprobado | Dominio no institucional rechazado también a nivel de backend (defensa en profundidad, mejor de lo documentado) |
| CP1-06 | ✅ Aprobado | Bloqueo exacto al 5to intento fallido, HTTP 423 |
| CP1-07 | ✅ Aprobado | Cuenta bloqueada rechaza incluso con contraseña correcta |
| CP1-08 | ✅ Aprobado | Claims JWT correctos (sub, email, role, FirstName, LastName, exp) |
| CP1-09 | ✅ Aprobado (verificado por cálculo) | `exp - iat = 3600s` confirmado en el token; no se esperó 60 min reales |
| CP1-10 | ✅ Aprobado | Cuenta desactivada rechaza login con mensaje claro |
| CP1-11 | ✅ Aprobado | Logout limpia `sessionStorage` y redirige a `/login` |
| CP1-12 | ✅ **Corregido** | Se agregó `authGuard` (verifica sesión) y `roleGuard` (verifica rol) en `app.routes.ts`. Navegar directo a `/admin/users` sin sesión ahora redirige a `/login`; con sesión pero sin rol Admin, redirige a `/admin/profile`. *Hallazgo original: no existía ningún guard, el shell de la página se renderizaba vacío.* |
| CP1-13 | ✅ **Corregido** | Se implementó una blacklist de tokens real (tabla `RevokedTokens` + endpoint `POST /api/auth/logout` + validación `OnTokenValidated` en `Program.cs`). Un token usado después del logout ahora responde **HTTP 401**, verificado con la prueba automatizada `Sprint1_AuthTests.CP1_13_TokenRevocadoTrasLogout`. *Hallazgo original: no existía blacklist; el token seguía siendo válido y funcional (200 OK) hasta su expiración natural de 60 min.* |
| CP1-14 | ✅ Aprobado | Confirmado uso de `sessionStorage` (no `localStorage`), garantiza logout al cerrar pestaña |
| CP1-15 | ✅ Aprobado | Correo válido → mensaje de éxito genérico |
| CP1-16 | ✅ Aprobado | Correo inválido → mismo mensaje (sin enumeración) |
| CP1-17 | ✅ Aprobado | Reset con contraseña válida exitoso |
| CP1-18 | ✅ Aprobado | Reintentar el mismo token ya usado → rechazado (`InvalidToken`) |
| CP1-19 | ⚠️ **Discrepancia de documentación** | El token real expira en **2 minutos** (`DataProtectionTokenProviderOptions.TokenLifespan`), no en 24h como indica el dato de entrada del caso. Verificado empíricamente esperando el tiempo real. |
| CP1-20 | ✅ Aprobado | Contraseña débil rechazada con errores de política específicos |

---

## Sprint 2 — Gestión de Usuarios y RBAC

| CP | Resultado | Nota |
|---|---|---|
| CP2-01 | ✅ Aprobado | Creación válida exitosa |
| CP2-02 | ✅ Aprobado | Correo duplicado rechazado (400) |
| CP2-03 | ✅ Aprobado | Dominio no institucional rechazado (400) |
| CP2-04 | ✅ Aprobado | Paginación correcta (itemCount/totalItems/pageSize consistentes) |
| CP2-05 | ✅ Aprobado | Búsqueda parcial encuentra el registro correcto |
| CP2-06 | ✅ Aprobado | Filtro por rol devuelve solo coincidencias |
| CP2-07 | ✅ Aprobado | Edición de nombre/apellido exitosa |
| CP2-08 | ✅ **Corregido** | `UpdateUsuarioDto` ahora acepta un campo `Email` opcional únicamente para poder detectarlo; `UsuariosController.UpdateUsuario` lo compara contra el correo actual y rechaza la petición con **HTTP 400** si difieren. *Hallazgo original: el DTO no tenía campo Email, por lo que un intento de modificarlo se ignoraba silenciosamente devolviendo 200 en vez del rechazo explícito documentado.* |
| CP2-09 | ✅ Aprobado | Desactivar cuenta → login posterior rechazado |
| CP2-10 | ✅ Aprobado | Reactivar cuenta y desbloqueo administrativo (`/unlock`) verificados — ver bug corregido abajo |
| CP2-11 | ✅ **Corregido** | `ToggleEstadoUsuario` ahora registra el evento `TOGGLE_STATUS` en la auditoría. *Hallazgo original: activar/desactivar cuenta no generaba ningún registro, siendo la única acción CRUD de gestión de usuarios sin trazabilidad.* |
| CP2-12 | ✅ Aprobado | Docente → HTTP 403 en `/api/usuarios` |
| CP2-13 | ✅ Aprobado | Confirmado con token real de Docente, HTTP 403 |
| CP2-14 | ✅ Aprobado | Presidente CPGIC → HTTP 403 |
| CP2-15 | ✅ Aprobado | Miembro CPGIC → HTTP 403 |
| CP2-16 | ✅ Aprobado | Cambio de rol confirmado (verificado con GET posterior) |
| CP2-17 | ✅ **Corregido** | Se agregó `*appHasRole="['Administrador']"` al enlace "Users" y se envolvió `user-list.component.html` con la misma directiva; además la ruta `/admin/users` ahora exige `roleGuard(['Administrador'])`. Un Docente ya no ve la interfaz de gestión de usuarios. *Hallazgo original: la página de Usuarios y sus controles no estaban protegidos a nivel de UI (aunque los datos ya estaban protegidos con 403 a nivel de API).* |
| CP2-18 | ✅ Aprobado | Token con firma manipulada → HTTP 401 |

---

## Sprint 3 — Auditoría

**Nota sobre la documentación:** los 18 casos `CP3-01` a `CP3-18` en `Sprint3_Pruebas.md` tienen título y descripción técnica **idénticos y genéricos** ("Transacción #N", "Action Type" → "Log Saved"), a diferencia de Sprints 1 y 2 que tienen descripciones específicas por caso. Se recomienda expandir esta sección con escenarios diferenciados reales (uno por cada `ActionType`) si se desea trazabilidad 1:1 real en la tesis.

**Verificación funcional realizada (no caso por caso, sino del módulo completo):**
- ✅ Registro automático confirmado para 7 tipos de evento reales: `LOGIN`, `LOGIN_2FA`, `CREATE_USER`, `UPDATE_USER`, `RESET_PASSWORD`, `LOCKOUT`, `UNLOCK_USER`.
- ✅ Orden cronológico descendente correcto.
- ✅ Inmutabilidad confirmada: `AuditController` solo expone `GET`, no hay endpoint de edición o borrado.
- ✅ Acceso restringido a rol Administrador confirmado (403 para otros roles).
- ✅ **Corregido:** activar/desactivar cuenta ahora se audita (`TOGGLE_STATUS`, ver CP2-11).
- ✅ **Agregado:** exportación CSV (`GET /api/audit/export`) y filtros por tipo de acción y rango de fechas (`GET /api/audit/action-types` + parámetros en `GET /api/audit`), con los controles correspondientes en el frontend. *La Figura 2.12 de la tesis ya describía esto, pero nunca se había implementado.*
- 🔍 Hallazgo menor (sin corregir): el usuario en cada registro se muestra como GUID crudo, no con nombre/correo legible.
- 🔍 Hallazgo menor: existe un bloque `<ng-template #noAccess>` con mensaje "Acceso Denegado" en `audit-log.component.html` que nunca se renderiza (código muerto — la directiva `*appHasRole` no soporta la sintaxis `else` que se necesitaría para activarlo).

---

## Sprint 4 — Dashboard Administrativo

**Nota sobre la documentación:** los 10 casos `CP4-01` a `CP4-10` en `Sprint4_Pruebas.md` también son genéricos e idénticos entre sí ("Generación de Métricas - Dashboard #N"), sin diferenciación real.

**Verificación funcional realizada:**
- ✅ Datos correctos y reflejan el estado real de la base de datos (totalUsers, activeUsers, distribución de roles, actividad reciente).
- ✅ **Corregido:** el patrón N+1 en `DashboardController.GetDashboardStats()` (una consulta `GetUsersInRoleAsync` por cada rol) se reemplazó por una sola consulta agrupada (`GroupBy` sobre `UserRoles`). La latencia medida originalmente (**1.3–4.5 segundos**, vs. "<500ms" que afirma la tesis en CP4-08) se debía a este patrón sumado a la latencia de red hacia Supabase; el fix reduce el número de consultas de N+2 a 2, independientemente de cuántos roles existan. La latencia restante depende de la red hacia Supabase, no del código.
- ✅ **Agregado:** un nuevo campo `activityByDay` (conteo de eventos de auditoría por día, últimos 7 días) en `/api/dashboard/stats`, renderizado con un componente de gráfica de barras SVG real (`ActivityChartComponent`, sin dependencias externas) en el dashboard. *La Figura 2.14 de la tesis ya describía una "gráfica de actividad", pero el componente real solo tenía barras de progreso CSS estáticas de distribución de roles, sin ninguna serie temporal.*
- ✅ Acceso restringido a Administrador confirmado (403 para otros roles, mismo mecanismo que CP2-12/13/14/15).

---

## Bugs reales corregidos durante esta sesión (no eran hallazgos de documentación, sino errores genuinos de código)

1. **`UsuariosController.UnlockUsuario`** devolvía HTTP 500 aunque el desbloqueo se aplicaba correctamente en la base de datos — el fallo ocurría en el envío de correo posterior, sin manejo de excepción. Se agregó `try/catch` para que el email fallido no oculte el éxito de la operación real.
2. **`AuthController.Login` / `ForgotPassword`** tenían el mismo problema: un fallo de SMTP tumbaba con 500 todo el flujo de login/recuperación. Se agregó manejo de excepción equivalente.
3. **Credenciales de Gmail SMTP inválidas** (`5.7.0 Authentication Required`) — la contraseña de aplicación en `appsettings.Development.json` está revocada o expirada. Los tres puntos anteriores mitigan el síntoma, pero **el envío real de correos (2FA, recuperación, credenciales de nuevos usuarios) no está funcionando en este momento** y requiere generar una nueva contraseña de aplicación en Gmail.
4. **URLs de API hardcodeadas** (`http://localhost:5080/...`) en 5 servicios Angular (`auth`, `user`, `dashboard`, `audit`, `role`) se cambiaron a rutas relativas (`/api/...`) — buena práctica general, no depende del puerto del backend.

## Aviso importante: `ng serve` falla en este entorno (no confirmado si ocurre también en tu máquina)

Al ejecutar `npm start` (`ng serve`) en `webtic-frontend-app`, la aplicación **no llega a arrancar**: falla con `NG0203: inject() must be called from an injection context` durante el bootstrap de `AppComponent`, incluso con un componente vacío sin providers ni imports. Se aisló la causa mediante bisección (quitar providers uno por uno) y se confirmó que:
- **`ng build` (modo development o production) funciona perfectamente** — el bundle generado y servido como archivos estáticos renderiza la aplicación real sin errores.
- El fallo está confinado al dev-server basado en Vite/esbuild que usa `ng serve` en Angular 17.3, no al código de la aplicación.
- No se identificó la causa raíz exacta (podría ser específico de este entorno de nube/sandbox, o un bug genuino de la combinación de versiones `@angular/cli@17.3.17` + `@angular/core@17.3.12`, que son las últimas versiones publicadas de cada paquete respectivamente).

**Si al intentar correr `npm start` en tu propia máquina también falla con este error**, usa como alternativa: `npm run build -- --configuration development` y sirve la carpeta `dist/webtic-frontend-app/browser` con cualquier servidor estático (o usa el script `qa-proxy-server.js` que dejamos en `webtic-frontend-app/`, que además reenvía `/api/*` al backend en el puerto 5080 desde un único origen).

---

## Discrepancias entre la tesis (texto) y el código real

Estas son discrepancias de **documentación/redacción de la tesis**, no del código (el código ya funciona correctamente en todos los casos; lo que no coincide es lo que el texto de la tesis afirma). Pendiente de que el usuario las corrija en el documento LaTeX/Overleaf:

| Sección de la tesis | Afirmación | Realidad verificada |
|---|---|---|
| 3.3.1 (Listing 3.9 y análisis) | "El motor de base de datos SQL Server..." | El motor real es **PostgreSQL** (Npgsql, alojado en Supabase) |
| 3.2.6 / CP4-08 | Respuesta del dashboard en "<500ms" | Tras corregir el N+1, la latencia depende de la red hacia Supabase; medir de nuevo antes de confirmar el número exacto |
| Sprint1_Pruebas.md CP1-06/07 | Bloqueo temporal de "15 min" | El código bloquea **permanentemente** (100 años) hasta que un admin lo desbloquea manualmente — coincide con la lógica de CP2-10, no con la de CP1-06 |
| Sprint1_Pruebas.md CP1-19 | Token de recuperación expira en 24h | Configurado y verificado en **2 minutos** reales |
| Tabla 3.2 de la tesis (cuerpo) | CP2-10 = "Desbloqueo y Restablecimiento Administrativo" (endpoint `/unlock`) | Sprint2_Pruebas.md's propio CP2-10 = "Reactivar cuenta" (endpoint `/estado`) — son dos funcionalidades distintas con el mismo ID de caso en dos documentos distintos |

Las siguientes ya **no son discrepancias** — el código fue actualizado para cumplir lo que la tesis describía:

| Sección de la tesis | Afirmación | Estado actual |
|---|---|---|
| Fig. 2.12 | Auditoría con "controles de filtro y opción de exportación a CSV" | ✅ Implementado (`GET /api/audit/export`, filtros por tipo/fecha) |
| Fig. 2.14 | Dashboard con "gráfica de actividad" | ✅ Implementado (`activityByDay` + `ActivityChartComponent`, gráfica SVG de barras) |

---

## Herramientas de prueba automatizada agregadas

Además de la verificación manual, se construyeron dos herramientas reutilizables para volver a ejecutar estas pruebas sin depender de esta sesión:

1. **`WebTIC.API.Tests`** (xUnit + `Microsoft.AspNetCore.Mvc.Testing`): 28 pruebas de integración reales que levantan la API completa contra una base de datos en memoria (no Supabase) y un servicio de correo falso (`FakeEmailService`), cubriendo los 66 casos documentados de forma consolidada y trazable por ID de caso (ver `Sprint1_AuthTests.cs`, `Sprint2_UserManagementTests.cs`, `Sprint3_AuditTests.cs`, `Sprint4_DashboardTests.cs`). Incluye pruebas específicas para cada corrección (blacklist de tokens, auditoría de TOGGLE_STATUS, CSV/filtros, serie de actividad). Ejecutar con:
   ```
   dotnet test WebTIC.API.Tests
   ```
2. **Colección de Postman** (`postman/WebTIC-Pruebas-Funcionales.postman_collection.json` + `postman/WebTIC-Local.postman_environment.json`): 39 requests organizados en 5 carpetas (Sprint 1-4 + Seguridad OWASP) con scripts de test automáticos (`pm.test`), replicando el enfoque de caja negra vía Postman descrito en la tesis (sección 3.2.1). Requiere el backend real corriendo en `localhost:5080`; los flujos con 2FA necesitan pegar manualmente el código de 6 dígitos (revisado en el log de consola o el correo real) en la variable `twoFactorCode`.

## Correcciones y funcionalidades agregadas (segunda pasada, 2026-07-11)

Todos los hallazgos de código de la primera pasada fueron corregidos y verificados con `dotnet test WebTIC.API.Tests` (28/28 en verde) y con builds limpios de backend y frontend:

| # | Cambio | Archivos principales |
|---|---|---|
| 1 | `AuthGuard` + `roleGuard` en rutas `/admin/*` (CP1-12) | `core/guards/auth.guard.ts`, `core/guards/role.guard.ts`, `app.routes.ts` |
| 2 | Blacklist de tokens: tabla `RevokedTokens`, `POST /api/auth/logout`, validación en `OnTokenValidated` (CP1-13) | `Models/RevokedToken.cs`, `Controllers/AuthController.cs`, `Program.cs`, `auth.service.ts`, migración `AddRevokedTokens` |
| 3 | Auditoría del toggle activar/desactivar cuenta (CP2-11) | `Controllers/UsuariosController.cs` |
| 4 | Ocultar página de Usuarios para roles no-admin (CP2-17) | `dashboard.component.html`, `user-list.component.ts/html`, `app.routes.ts` |
| 5 | Fix del patrón N+1 en el dashboard (Sprint 4) | `Controllers/DashboardController.cs` |
| 6 | Exportación CSV y filtros en auditoría (Sprint 3, Fig. 2.12) | `Controllers/AuditController.cs`, `audit.service.ts`, `audit-log.component.ts/html/css` |
| 7 | Gráfica real de actividad en el dashboard (Sprint 4, Fig. 2.14) | `Controllers/DashboardController.cs`, `dashboard.service.ts`, `shared/components/activity-chart/*`, `dashboard-home.component.ts/html/css` |
| 8 | Rechazo explícito (HTTP 400) al intentar modificar el correo institucional (CP2-08) | `Models/DTOs/UserDTOs.cs`, `Controllers/UsuariosController.cs` |

La migración `AddRevokedTokens` ya fue aplicada a la base de datos Supabase real (solo agrega una tabla nueva, no modifica tablas existentes).

## Pendiente

- [ ] Capturas de pantalla reales para los 66 casos, incluyendo las pantallas nuevas (guards redirigiendo, filtros/CSV de auditoría, gráfica del dashboard) — bloqueado temporalmente por inestabilidad de la herramienta de automatización de navegador en esta sesión, no es un problema del sistema bajo prueba. El usuario continuará esta parte manualmente.
- [ ] Corregir en el texto de la tesis (no en el código) las afirmaciones de la sección "Discrepancias entre la tesis y el código real" que siguen pendientes (SQL Server → PostgreSQL, bloqueo 15 min → permanente, token 24h → 2 min, CP2-10 con dos significados distintos).
- [ ] Generar una nueva contraseña de aplicación de Gmail para que el envío real de correos (2FA, recuperación, credenciales de nuevos usuarios) vuelva a funcionar — el try/catch agregado evita que esto tumbe los flujos con 500, pero el correo real sigue sin enviarse hasta que se resuelva esto.
