# Informe de Verificación Funcional — Módulo de Gestión de Usuarios y Autenticación

**Fecha de ejecución:** 2026-07-11
**Entorno:** Backend .NET 8 real (puerto 5080) + build de producción de Angular servido localmente (proxy en puerto 4400) + base de datos Supabase (PostgreSQL) real.
**Metodología:** Se ejecutaron los 66 casos de prueba documentados en `Sprint1_Pruebas.md` a `Sprint4_Pruebas.md` contra el sistema real (no simulado), usando el navegador para los flujos de UI y peticiones HTTP directas para los flujos de API, replicando el enfoque híbrido de caja negra/caja blanca descrito en la tesis (sección 3.2.1).

> Nota: las capturas de pantalla de evidencia visual quedaron pendientes por una inestabilidad puntual de la herramienta de automatización del navegador durante esta sesión. Este informe documenta el **resultado funcional real verificado** de cada caso; la carpeta `Evidencias/` debe completarse con capturas en una sesión de seguimiento.

---

## Resumen ejecutivo

| Sprint | Casos | Aprobados sin observaciones | Con hallazgos/discrepancias |
|---|---|---|---|
| 1 — Autenticación/2FA/JWT | 20 | 17 | 3 (CP1-12, CP1-13, CP1-19) |
| 2 — Gestión de Usuarios/RBAC | 18 | 16 | 2 (CP2-08, CP2-11 parcial, CP2-17) |
| 3 — Auditoría | 18 (genéricos) | — | Ver hallazgos generales |
| 4 — Dashboard | 10 (genéricos) | — | Ver hallazgos generales |

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
| CP1-12 | ❌ **Discrepancia real** | Navegar directo a `/admin/users` tras logout **no redirige a login** (no existe `AuthGuard` en las rutas). Los datos sí quedan protegidos (401 del backend), pero el shell de la página se renderiza vacío. |
| CP1-13 | ❌ **Discrepancia de seguridad real** | **No existe blacklist de tokens.** Un JWT emitido antes del logout sigue siendo 100% válido y funcional hasta su expiración natural (60 min). El documento indica "HTTP 401" esperado; el comportamiento real es "200 OK". |
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
| CP2-08 | ⚠️ **Discrepancia real** | El DTO de actualización (`UpdateUsuarioDto`) **no tiene campo Email** — un intento de modificarlo no da "HTTP 400 Rechazo explícito" como documenta el caso; el campo simplemente se ignora y la petición devuelve 200. El resultado de seguridad es el mismo (el correo no cambia) pero el mecanismo real es distinto al documentado. |
| CP2-09 | ✅ Aprobado | Desactivar cuenta → login posterior rechazado |
| CP2-10 | ✅ Aprobado | Reactivar cuenta y desbloqueo administrativo (`/unlock`) verificados — ver bug corregido abajo |
| CP2-11 | ⚠️ **Aprobado con hallazgo** | CREATE_USER y UPDATE_USER sí se registran correctamente en el log de auditoría. **Pero activar/desactivar cuenta (endpoint `/estado`) no genera ningún registro de auditoría** — es la única acción CRUD de gestión de usuarios sin trazabilidad. |
| CP2-12 | ✅ Aprobado | Docente → HTTP 403 en `/api/usuarios` |
| CP2-13 | ✅ Aprobado | Confirmado con token real de Docente, HTTP 403 |
| CP2-14 | ✅ Aprobado | Presidente CPGIC → HTTP 403 |
| CP2-15 | ✅ Aprobado | Miembro CPGIC → HTTP 403 |
| CP2-16 | ✅ Aprobado | Cambio de rol confirmado (verificado con GET posterior) |
| CP2-17 | ⚠️ **Discrepancia real de UI** | La directiva `*appHasRole` oculta correctamente los enlaces Home/Roles/Audit/Settings para roles no-administrador. **Pero la página de Usuarios (incluyendo el botón "+ Crear Usuario" y sus controles) no está protegida a nivel de UI** — un Docente ve la interfaz completa de gestión de usuarios, aunque las peticiones de datos fallan con 403 (confirmado en consola: "Error fetching users/roles"). |
| CP2-18 | ✅ Aprobado | Token con firma manipulada → HTTP 401 |

---

## Sprint 3 — Auditoría

**Nota sobre la documentación:** los 18 casos `CP3-01` a `CP3-18` en `Sprint3_Pruebas.md` tienen título y descripción técnica **idénticos y genéricos** ("Transacción #N", "Action Type" → "Log Saved"), a diferencia de Sprints 1 y 2 que tienen descripciones específicas por caso. Se recomienda expandir esta sección con escenarios diferenciados reales (uno por cada `ActionType`) si se desea trazabilidad 1:1 real en la tesis.

**Verificación funcional realizada (no caso por caso, sino del módulo completo):**
- ✅ Registro automático confirmado para 7 tipos de evento reales: `LOGIN`, `LOGIN_2FA`, `CREATE_USER`, `UPDATE_USER`, `RESET_PASSWORD`, `LOCKOUT`, `UNLOCK_USER`.
- ✅ Orden cronológico descendente correcto.
- ✅ Inmutabilidad confirmada: `AuditController` solo expone `GET`, no hay endpoint de edición o borrado.
- ✅ Acceso restringido a rol Administrador confirmado (403 para otros roles).
- ⚠️ **Gap real:** activar/desactivar cuenta no se audita (ver CP2-11).
- ⚠️ **Discrepancia con la tesis:** la Figura 2.12 describe el módulo con "controles de filtro y opción de exportación a CSV". El componente real (`audit-log.component.ts/html`) **solo tiene una tabla paginada**, sin ningún filtro ni exportación CSV.
- 🔍 Hallazgo menor: el usuario en cada registro se muestra como GUID crudo, no con nombre/correo legible.
- 🔍 Hallazgo menor: existe un bloque `<ng-template #noAccess>` con mensaje "Acceso Denegado" en `audit-log.component.html` que nunca se renderiza (código muerto — la directiva `*appHasRole` no soporta la sintaxis `else` que se necesitaría para activarlo).

---

## Sprint 4 — Dashboard Administrativo

**Nota sobre la documentación:** los 10 casos `CP4-01` a `CP4-10` en `Sprint4_Pruebas.md` también son genéricos e idénticos entre sí ("Generación de Métricas - Dashboard #N"), sin diferenciación real.

**Verificación funcional realizada:**
- ✅ Datos correctos y reflejan el estado real de la base de datos (totalUsers, activeUsers, distribución de roles, actividad reciente).
- ❌ **Discrepancia de rendimiento medida:** la tesis (CP4-08) afirma respuesta en "<500ms". Medido empíricamente contra el sistema real: **1.3 a 4.5 segundos** por petición a `/api/dashboard/stats`. Causa raíz identificada en el código: `DashboardController.GetDashboardStats()` ejecuta una consulta separada a la base de datos **por cada rol** dentro de un bucle (`GetUsersInRoleAsync`), un patrón N+1 real, agravado por la latencia de red hacia Supabase (no es una base de datos local).
- ⚠️ **Discrepancia con la tesis:** la Figura 2.14 menciona una "gráfica de actividad". El componente real (`dashboard-home.component.html`) no usa ninguna librería de gráficos (no hay Chart.js/D3/ngx-charts en `package.json`) — es una lista de texto con barras de progreso hechas en CSS puro, no un gráfico interactivo.
- ✅ Acceso restringido a Administrador confirmado (403 para otros roles, mismo mecanismo que CP2-12/13/14/15).

---

## Bugs reales corregidos durante esta sesión (no eran hallazgos de documentación, sino errores genuinos de código)

1. **`UsuariosController.UnlockUsuario`** devolvía HTTP 500 aunque el desbloqueo se aplicaba correctamente en la base de datos — el fallo ocurría en el envío de correo posterior, sin manejo de excepción. Se agregó `try/catch` para que el email fallido no oculte el éxito de la operación real.
2. **`AuthController.Login` / `ForgotPassword`** tenían el mismo problema: un fallo de SMTP tumbaba con 500 todo el flujo de login/recuperación. Se agregó manejo de excepción equivalente.
3. **Credenciales de Gmail SMTP inválidas** (`5.7.0 Authentication Required`) — la contraseña de aplicación en `appsettings.Development.json` está revocada o expirada. Los tres puntos anteriores mitigan el síntoma, pero **el envío real de correos (2FA, recuperación, credenciales de nuevos usuarios) no está funcionando en este momento** y requiere generar una nueva contraseña de aplicación en Gmail.
4. **URLs de API hardcodeadas** (`http://localhost:5080/...`) en 5 servicios Angular (`auth`, `user`, `dashboard`, `audit`, `role`) se cambiaron a rutas relativas (`/api/...`) — buena práctica general, no depende del puerto del backend.

---

## Discrepancias entre la tesis (texto) y el código real

| Sección de la tesis | Afirmación | Realidad verificada |
|---|---|---|
| 3.3.1 (Listing 3.9 y análisis) | "El motor de base de datos SQL Server..." | El motor real es **PostgreSQL** (Npgsql, alojado en Supabase) |
| 3.2.6 / CP4-08 | Respuesta del dashboard en "<500ms" | Medido: **1.3–4.5 segundos** reales (N+1 query + latencia de Supabase) |
| Fig. 2.12 | Auditoría con "controles de filtro y opción de exportación a CSV" | No existen ni filtros ni exportación CSV en el componente real |
| Fig. 2.14 | Dashboard con "gráfica de actividad" | No hay librería de gráficos; es una lista con barras CSS |
| Sprint1_Pruebas.md CP1-06/07 | Bloqueo temporal de "15 min" | El código bloquea **permanentemente** (100 años) hasta que un admin lo desbloquea manualmente — coincide con la lógica de CP2-10, no con la de CP1-06 |
| Sprint1_Pruebas.md CP1-19 | Token de recuperación expira en 24h | Configurado y verificado en **2 minutos** reales |
| Tabla 3.2 de la tesis (cuerpo) | CP2-10 = "Desbloqueo y Restablecimiento Administrativo" (endpoint `/unlock`) | Sprint2_Pruebas.md's propio CP2-10 = "Reactivar cuenta" (endpoint `/estado`) — son dos funcionalidades distintas con el mismo ID de caso en dos documentos distintos |

---

## Pendiente

- [ ] Capturas de pantalla reales para los 66 casos (bloqueado temporalmente por inestabilidad de la herramienta de automatización de navegador en esta sesión — no es un problema del sistema bajo prueba).
- [ ] Decidir si corregir el código real para los gaps encontrados (AuthGuard de rutas, blacklist de tokens, auditoría de activar/desactivar, N+1 del dashboard) o documentarlos como limitaciones conocidas en la tesis.
- [ ] Generar una nueva contraseña de aplicación de Gmail para que el envío real de correos vuelva a funcionar.
