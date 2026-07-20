# Guía de Ejecución Manual y Captura de Evidencias — 66 Casos de Prueba

Esta guía te permite ejecutar cada caso de prueba documentado en `Sprint1_Pruebas.md` a `Sprint4_Pruebas.md` contra el sistema real y capturar la evidencia visual correspondiente. Cada entrada indica: cuenta a usar, pasos exactos, qué debe aparecer en la captura, y el nombre de archivo esperado por los `.md` (ya referenciado en cada `<img src="../Evidencias/evidencia_cpX_XX.png">`).

---

## 0. Preparación (una sola vez)

### 0.1 Levantar el backend
```
cd webtic-backend-api
dotnet run
```
Corre en `http://localhost:5080`. Swagger disponible en `http://localhost:5080/swagger`.

### 0.2 Levantar el frontend
```
cd webtic-frontend-app
npm start
```
Esto usa tu `proxy.conf.json` (reenvía `/api/*` al backend). Se abre en `http://localhost:4200`.

### 0.3 Carpeta de evidencias
Todas las capturas van en:
```
Test_y_Validaciones/Pruebas_Funcionales/Evidencias/
```
Créala si no existe. Nombra cada archivo exactamente como indica cada caso (`evidencia_cp1_01.png`, etc.) — así los `<img>` ya insertados en los `.md` las mostrarán automáticamente sin editar nada más.

### 0.4 Cuentas de prueba sembradas
| Correo | Password | Rol |
|---|---|---|
| `admin.webtic@epn.edu.ec` | `Santodomingo23!!` | Administrador |
| `alexander.tibanta@epn.edu.ec` | `Santodomingo23!!` | Administrador |
| `victor.velepucha@epn.edu.ec` | `Santodomingo23!!` | Docente |
| `presidente.cpgic@epn.edu.ec` | `Santodomingo23!!` | Presidente CPGIC |
| `miembro.cpgic@epn.edu.ec` | `Santodomingo23!!` | Miembro CPGIC |

Si alguna cuenta quedó bloqueada/desactivada de pruebas anteriores, entra como Admin a **Users** y usa **Desbloquear Cuenta** o el toggle de estado antes de empezar.

### 0.5 Cómo obtener el código 2FA y el token de recuperación (sin depender del correo real)
El backend imprime ambos en su propia consola (donde corriste `dotnet run`) con el prefijo `[DEV MODE]`:
```
[DEV MODE] Código 2FA para admin.webtic@epn.edu.ec: 482913
[DEV MODE] Enlace de recuperación para victor.velepucha@epn.edu.ec: http://localhost:4200/reset-password?email=...&token=...
```
Busca esa línea justo después de hacer login o pedir "¿Olvidaste tu contraseña?". El código 2FA expira en 2 minutos: cópialo e ingrésalo rápido.

> Si prefieres usar el correo real, revisa la bandeja de `alexandertibanta1@gmail.com` — pero la contraseña de aplicación de Gmail está revocada actualmente, así que probablemente no llegue nada; usa el método de consola.

### 0.6 Alternativa por API (Postman)
Para los casos que la tesis valida "en caja negra vía Postman" (2FA, fuerza bruta, RBAC, seguridad), puedes usar la colección ya preparada:
```
postman/WebTIC-Pruebas-Funcionales.postman_collection.json
postman/WebTIC-Local.postman_environment.json
```
Importa ambos en Postman, selecciona el entorno "WebTIC - Local", y corre los requests en el orden de las carpetas (Sprint 1 → 2 → 3 → 4 → Seguridad OWASP). Cada uno tiene un test automático (pestaña "Test Results") que te confirma si el resultado fue el esperado — captura esa pestaña junto con el body de la respuesta.

---

## 1. Sprint 1 — Autenticación, 2FA y Sesión (20 casos)

**CP1-01 — Login con credenciales válidas (Admin)**
1. Ve a `http://localhost:4200/login`.
2. Ingresa `admin.webtic@epn.edu.ec` / `Santodomingo23!!` → **Ingresar**.
3. Captura la pantalla de verificación 2FA (6 casillas) que aparece inmediatamente después.
4. Guarda: `evidencia_cp1_01.png`

**CP1-02 — Login (Administrador, segunda cuenta)**
1. Repite el login con `alexander.tibanta@epn.edu.ec` / `Santodomingo23!!`.
2. Completa el 2FA (ver 0.5) y espera a que cargue el panel de Usuarios.
3. Captura el panel de administración ya cargado (confirma acceso admin).
4. Guarda: `evidencia_cp1_02.png`

**CP1-03 — Contraseña incorrecta**
1. En `/login`, ingresa `victor.velepucha@epn.edu.ec` con una contraseña incorrecta (ej. `MalaClave1`).
2. Captura el cuadro rojo que aparece en el propio formulario: título "Acceso Denegado", descripción "Credenciales incorrectas. Intento 1 de 5."
3. Guarda: `evidencia_cp1_03.png`

**CP1-04 — Correo no registrado**
1. Ingresa un correo institucional que no exista, ej. `noexiste@epn.edu.ec`, con cualquier contraseña.
2. Captura el mismo tipo de cuadro rojo ("Acceso Denegado" / "Credenciales incorrectas.") — nota que es idéntico al de CP1-03 (mitigación de enumeración).
3. Guarda: `evidencia_cp1_04.png`

**CP1-05 — Dominio no institucional**
1. Escribe `usuario@gmail.com` en el campo de correo y haz clic fuera del campo (o intenta enviar).
2. Captura el mensaje de validación del propio formulario Angular (aparece antes de enviar nada al backend, ya que el campo tiene un patrón `@epn.edu.ec` obligatorio).
3. Guarda: `evidencia_cp1_05.png`

**CP1-06 — Bloqueo tras 5 intentos fallidos**
1. Usa una cuenta que puedas dejar bloqueada (recomendado: `victor.velepucha@epn.edu.ec`, y luego desbloquéala tú mismo como Admin al terminar).
2. Ingresa una contraseña incorrecta **5 veces seguidas**.
3. Captura el cuadro rojo del 5to intento: título "Acceso Bloqueado", código HTTP 423 (puedes confirmarlo en la pestaña Red/Network del navegador).
4. Guarda: `evidencia_cp1_06.png`
5. **Importante:** el bloqueo es permanente hasta que un Admin lo quite (no son 15 min reales, ver nota en el propio `Sprint1_Pruebas.md`). Después de capturar, entra como Admin a Users → busca la cuenta → **Desbloquear Cuenta**.

**CP1-07 — Intento en cuenta bloqueada**
1. Con la misma cuenta de CP1-06 aún bloqueada, intenta iniciar sesión de nuevo — esta vez con la contraseña **correcta**.
2. Captura el mismo cuadro "Acceso Bloqueado" (se rechaza sin siquiera validar la contraseña).
3. Guarda: `evidencia_cp1_07.png`
4. Recuerda desbloquear la cuenta después (Admin → Users → Desbloquear Cuenta).

**CP1-08 — Verificación claims JWT**
1. Inicia sesión como Admin normalmente (login + 2FA).
2. Abre las DevTools del navegador (F12) → pestaña **Application/Aplicación** → **Session Storage** → `http://localhost:4200` → copia el valor de `token`.
3. Pega el token en [jwt.io](https://jwt.io) (o decodifícalo tú: la parte central del JWT en Base64) y captura el panel "Payload" mostrando `sub`, `email`, `role`, `FirstName`, `LastName`, `exp`.
4. Guarda: `evidencia_cp1_08.png`

**CP1-09 — Expiración de sesión (60 min)**
1. Inicia sesión y anota la hora.
2. Espera 60 minutos reales (o, si tienes acceso al código, baja temporalmente `JwtSettings:ExpiryMinutes` en `appsettings.json` a `1` y reinicia el backend solo para esta prueba).
3. Intenta abrir `/admin/users` o refrescar la página tras el vencimiento.
4. Captura la petición con estado 401 en la pestaña Network, o la redirección a `/login`.
5. Guarda: `evidencia_cp1_09.png`
6. Si bajaste `ExpiryMinutes`, restáuralo a `60` y reinicia el backend al terminar.

**CP1-10 — Cuenta desactivada**
1. Como Admin, ve a Users, busca una cuenta de prueba y usa el botón de **Desactivar**.
2. Cierra sesión, intenta iniciar sesión con esa cuenta.
3. Captura el cuadro "Cuenta Inactiva" / "Su cuenta se encuentra inactiva. Contacte al administrador."
4. Guarda: `evidencia_cp1_10.png`
5. Reactívala después desde Admin → Users.

**CP1-11 — Cierre de sesión**
1. Con sesión iniciada, haz clic en **Logout** (barra lateral) → confirma en el modal "Cerrar Sesión".
2. Captura el momento justo antes de confirmar (el modal de confirmación) **o** la pantalla de login inmediatamente después de confirmar.
3. Guarda: `evidencia_cp1_11.png`

**CP1-12 — Ruta protegida post-logout**
1. Tras cerrar sesión (CP1-11), escribe manualmente en la barra de direcciones: `http://localhost:4200/admin/users`.
2. Captura la redirección automática a `/login` (la URL en la barra de direcciones debe mostrar `/login`, no `/admin/users`).
3. Guarda: `evidencia_cp1_12.png`

**CP1-13 — Uso de token en API post-logout**
1. Antes de cerrar sesión, copia el token de `sessionStorage` (DevTools → Application → Session Storage → `token`), igual que en CP1-08.
2. Cierra sesión normalmente (botón Logout).
3. Abre Postman (o la pestaña Network con "Copy as fetch"), y envía `GET http://localhost:5080/api/usuarios?page=1&pageSize=5` con el header `Authorization: Bearer <token copiado>`.
4. Captura la respuesta: debe ser **401 Unauthorized** (el token fue revocado al hacer logout).
5. Guarda: `evidencia_cp1_13.png`

**CP1-14 — Cierre de pestaña**
1. Inicia sesión normalmente.
2. Cierra la pestaña del navegador por completo (no solo navegar a otra URL) y vuelve a abrir `http://localhost:4200/admin/users`.
3. Captura la redirección a `/login` (confirma que `sessionStorage` no persiste entre sesiones de pestaña, a diferencia de `localStorage`).
4. Guarda: `evidencia_cp1_14.png`

**CP1-15 — Restablecimiento correo válido**
1. En `/login`, clic en **¿Olvidaste tu contraseña?**.
2. Ingresa `victor.velepucha@epn.edu.ec` → **Enviar Enlace de Recuperación**.
3. Captura el mensaje verde de éxito: "Si el correo existe y está activo, se ha enviado un enlace de recuperación."
4. Guarda: `evidencia_cp1_15.png`

**CP1-16 — Restablecimiento correo inválido**
1. Repite el mismo formulario con un correo institucional inexistente, ej. `fantasma@epn.edu.ec`.
2. Captura el **mismo** mensaje de éxito (para comparar con CP1-15 y confirmar que no hay diferencia — mitigación de enumeración).
3. Guarda: `evidencia_cp1_16.png`

**CP1-17 — Nueva contraseña exitosa**
1. Toma el enlace de recuperación de la consola del backend (0.5) tras el CP1-15, o pide uno nuevo.
2. Ábrelo en el navegador (te lleva a `/reset-password?email=...&token=...`).
3. Ingresa una contraseña nueva válida (ej. `NuevaPass2026!`) en ambos campos → **Restablecer Contraseña**.
4. Captura el mensaje verde de éxito.
5. Guarda: `evidencia_cp1_17.png`

**CP1-18 — Token de restablecimiento ya usado**
1. Inmediatamente después de CP1-17, recarga la misma URL de reset (con el mismo token) e intenta restablecer de nuevo con otra contraseña.
2. Captura el mensaje de error (token inválido/ya usado).
3. Guarda: `evidencia_cp1_18.png`
4. Recuerda que la contraseña de `victor.velepucha` quedó en `NuevaPass2026!`; puedes restablecerla de nuevo a `Santodomingo23!!` repitiendo el flujo de recuperación para dejarla lista para otras pruebas.

**CP1-19 — Token expirado**
1. Pide un nuevo enlace de recuperación (CP1-15) para cualquier cuenta.
2. Espera **más de 2 minutos reales** (la ventana real configurada, no 24h) antes de usarlo.
3. Intenta restablecer la contraseña con ese token vencido.
4. Captura el mensaje de error (token inválido/expirado).
5. Guarda: `evidencia_cp1_19.png`

**CP1-20 — Política de contraseña fallida**
1. En el formulario de reset-password, escribe una contraseña débil, ej. `abc`.
2. Observa/captura el panel de requisitos ("8 caracteres", "Una mayúscula", "Un número", "Un carácter especial") mostrando cuáles NO se cumplen (en rojo/sin check).
3. Guarda: `evidencia_cp1_20.png`

---

## 2. Sprint 2 — Gestión de Usuarios y RBAC (18 casos)

Inicia sesión como **Admin** (`admin.webtic@epn.edu.ec`) para todos los casos que requieran el panel de Usuarios.

**CP2-01 — Creación de cuenta válida**
1. En Users, clic **+ Crear Usuario**. Llena Nombre, Apellido, Correo (`@epn.edu.ec`), Rol → **Guardar**.
2. Captura el mensaje de éxito o la nueva fila en la tabla.
3. Guarda: `evidencia_cp2_01.png`

**CP2-02 — Correo ya registrado**
1. Repite la creación con el mismo correo del paso anterior.
2. Captura el error "El correo ya está registrado".
3. Guarda: `evidencia_cp2_02.png`

**CP2-03 — Dominio no institucional**
1. Intenta crear un usuario con correo `@gmail.com`.
2. Captura el error de validación (frontend o el mensaje del backend "El correo debe ser del dominio @epn.edu.ec").
3. Guarda: `evidencia_cp2_03.png`

**CP2-04 — Listado paginado**
1. En la tabla de Users, captura los controles de paginación en la parte inferior con más de una página visible.
2. Guarda: `evidencia_cp2_04.png`

**CP2-05 — Búsqueda parcial**
1. Escribe una parte de un nombre/correo en el buscador de la barra de herramientas.
2. Captura la tabla filtrada mostrando solo las coincidencias.
3. Guarda: `evidencia_cp2_05.png`

**CP2-06 — Filtro por rol**
1. Usa el desplegable "Todos los Roles" y selecciona, por ejemplo, "Docente".
2. Captura la tabla mostrando solo usuarios con ese rol.
3. Guarda: `evidencia_cp2_06.png`

**CP2-07 — Edición de datos**
1. Clic **Editar** en cualquier usuario, cambia el nombre → **Guardar**.
2. Captura el mensaje de éxito o el nombre actualizado en la tabla.
3. Guarda: `evidencia_cp2_07.png`

**CP2-08 — Modificar correo**
1. Abre **Editar** sobre un usuario. El formulario no permite cambiar el correo desde la UI normal — para probar el rechazo del backend, usa Postman: `PUT http://localhost:5080/api/usuarios/{id}` con body `{"firstName":"...","lastName":"...","role":"...","email":"otro@epn.edu.ec"}` y el token de Admin.
2. Captura la respuesta **HTTP 400** con el mensaje "El correo institucional no puede modificarse."
3. Guarda: `evidencia_cp2_08.png`

**CP2-09 — Desactivar cuenta**
1. Clic **Desactivar** en un usuario de prueba.
2. Captura el cambio de estado en la tabla (badge "Inactivo").
3. Guarda: `evidencia_cp2_09.png`

**CP2-10 — Reactivar cuenta**
1. Sobre el mismo usuario, clic de nuevo para reactivarlo (mismo botón toggle).
2. Captura el estado "Activo" restaurado.
3. Guarda: `evidencia_cp2_10.png`

**CP2-11 — Registro de auditoría CRUD**
1. Ve a **Audit Log** (solo visible/accesible como Admin).
2. Captura la tabla mostrando los eventos recientes `CREATE_USER`, `UPDATE_USER` y `TOGGLE_STATUS` generados en los pasos anteriores.
3. Guarda: `evidencia_cp2_11.png`

**CP2-12 — Acceso admin**
1. Con sesión de Admin activa, captura el panel de Users completamente cargado (confirma acceso).
2. Guarda: `evidencia_cp2_12.png`

**CP2-13 — Acceso admin (Rol Docente)**
1. Cierra sesión, inicia sesión como `victor.velepucha@epn.edu.ec` (Docente).
2. Observa que en el sidebar **no aparecen** los enlaces Home/Users/Roles/Audit/Settings (solo "Mi Perfil").
3. Para confirmar el 403 a nivel de API, usa Postman con el token de Docente contra `GET /api/usuarios`.
4. Captura el sidebar reducido y/o la respuesta 403 de Postman.
5. Guarda: `evidencia_cp2_13.png`

**CP2-14 — Acceso admin (Presidente)**
1. Repite el mismo procedimiento con `presidente.cpgic@epn.edu.ec`.
2. Guarda: `evidencia_cp2_14.png`

**CP2-15 — Acceso admin (Miembro)**
1. Repite con `miembro.cpgic@epn.edu.ec`.
2. Guarda: `evidencia_cp2_15.png`

**CP2-16 — Cambio de rol**
1. Como Admin, edita un usuario y cámbiale el Rol en el desplegable → **Guardar**.
2. Captura el nuevo rol reflejado en la tabla.
3. Guarda: `evidencia_cp2_16.png`

**CP2-17 — Visibilidad directiva *appHasRole**
1. Con sesión de un rol no-Admin (Docente/Presidente/Miembro), captura el sidebar mostrando **solo** "Mi Perfil" y el botón Logout (Home/Users/Roles/Audit/Settings ocultos).
2. Guarda: `evidencia_cp2_17.png`

**CP2-18 — Token manipulado manualmente**
1. Copia un token válido de `sessionStorage`, cambia manualmente los últimos 5 caracteres por texto arbitrario (ej. `AAAAA`).
2. En Postman, usa ese token alterado contra `GET /api/usuarios`.
3. Captura la respuesta **401 Unauthorized** (firma inválida).
4. Guarda: `evidencia_cp2_18.png`

---

## 3. Sprint 3 — Auditoría (18 casos genéricos → 6 escenarios reales)

Los 18 casos documentados (`CP3-01` a `CP3-18`) comparten texto genérico. En vez de repetir la misma captura 18 veces, genera un evento real distinto para cada uno de estos y guárdalos como `evidencia_cp3_01.png` a `evidencia_cp3_06.png` (puedes reutilizar el resto de los 18 slots referenciando estas mismas 6 evidencias, o repetir el patrón para completar):

1. **Login (`LOGIN_2FA`)** → inicia sesión como cualquier usuario → ve a Audit Log → captura la fila `LOGIN_2FA`.
2. **Creación de usuario (`CREATE_USER`)** → repite CP2-01 → captura la fila correspondiente.
3. **Edición de usuario (`UPDATE_USER`)** → repite CP2-07 → captura la fila.
4. **Toggle de estado (`TOGGLE_STATUS`)** → repite CP2-09/10 → captura la fila (antes no existía este registro; ahora sí).
5. **Bloqueo por fuerza bruta (`LOCKOUT`)** → repite CP1-06 → captura la fila.
6. **Desbloqueo administrativo (`UNLOCK_USER`)** → usa el botón "Desbloquear Cuenta" sobre una cuenta bloqueada → captura la fila.

**Además, para el módulo completo:**
- Captura los **controles de filtro** (Tipo de Acción, Desde, Hasta) en la parte superior de Audit Log, con un filtro aplicado (ej. `actionType=LOGIN_2FA`).
- Haz clic en **⬇ Exportar CSV** y captura el archivo descargado (ábrelo en Excel/Notepad para mostrar las columnas `Fecha,TipoDeAccion,UsuarioId,DireccionIP,Detalles`).

---

## 4. Sprint 4 — Dashboard Administrativo (10 casos genéricos → 3 capturas reales)

Los 10 casos (`CP4-01` a `CP4-10`) también son genéricos. Cubre estas 3 capturas reales:

1. **Dashboard completo** — inicia sesión como Admin, ve a **Home**. Captura la pantalla completa: las 3 tarjetas (Usuarios Totales/Activos/Inactivos), la **gráfica de barras de actividad de los últimos 7 días**, la distribución por roles, y la actividad reciente. Guarda como `evidencia_cp4_01.png` (y repite/reutiliza para `cp4_02` a `cp4_10` si el formato lo requiere).
2. **Latencia real** — abre DevTools → pestaña Network → recarga el Dashboard → busca la petición `stats` → captura la columna "Time" (milisegundos). Guarda como `evidencia_cp4_08.png` (corresponde al caso de rendimiento).
3. **Acceso restringido** — inicia sesión como Docente/Presidente/Miembro → confirma que no ves el enlace "Home" en el sidebar, y que `GET /api/dashboard/stats` en Postman con su token da 403.

---

## 5. Checklist final

- [ ] Todas las cuentas de prueba quedaron restauradas a su estado normal (activas, desbloqueadas, `Santodomingo23!!`).
- [ ] Todas las capturas están en `Test_y_Validaciones/Pruebas_Funcionales/Evidencias/` con el nombre exacto `evidencia_cpX_XX.png`.
- [ ] Verifica que los `<img>` ya insertados en cada `Sprint*_Pruebas.md` ahora muestran la imagen (ábrelos con un visor de Markdown/HTML; el `onerror` oculta el recuadro punteado en cuanto detecta el archivo).
