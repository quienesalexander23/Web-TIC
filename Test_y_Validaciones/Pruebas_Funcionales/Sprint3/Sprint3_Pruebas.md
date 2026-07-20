# Pruebas Funcionales del Sprint 3

**Introducción:** Aseguramiento de calidad para Registros Automáticos de Auditoría y Control de Acceso Basado en Permisos Granulares (CPGIC).

En esta sección se presenta la matriz completa de los casos de prueba ejecutados durante este Sprint. Cada caso valida una dimensión distinta y no solapada del módulo de auditoría: un tipo de evento del sistema (login, logout, bloqueo, restablecimiento, gestión de usuarios), una garantía estructural del registro (orden, integridad de campos, inmutabilidad), una capacidad de consulta (paginación, filtros, tipos de acción, exportación CSV) o la restricción de acceso por rol. La correspondencia con el módulo real (`AuditController.cs`, `AuthController.cs`, `UsuariosController.cs`) se verifica automáticamente en `Sprint3_AuditTests.cs`.

<table style="width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; border: 2px solid black; font-size: 14px;">
    <tr style="background-color: #000000; color: #ffffff;">
        <th colspan="7" style="padding: 10px; border: 1px solid black;">Matriz de Pruebas: Pruebas Funcionales del Sprint 3</th>
    </tr>
    <tr style="background-color: #333333; color: #ffffff; text-align: center;">
        <th style="padding: 8px; border: 1px solid black;">ID</th>
        <th style="padding: 8px; border: 1px solid black;">HU</th>
        <th style="padding: 8px; border: 1px solid black;">Descripción del caso</th>
        <th style="padding: 8px; border: 1px solid black;">Datos de entrada</th>
        <th style="padding: 8px; border: 1px solid black;">Resultado esperado</th>
        <th style="padding: 8px; border: 1px solid black;">Resultado real</th>
        <th style="padding: 8px; border: 1px solid black;">Estado</th>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-01</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de inicio de sesión (LOGIN_2FA)</td>
        <td style="padding: 8px; border: 1px solid black;">Login + código 2FA válido</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=LOGIN_2FA</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-02</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de cierre de sesión (LOGOUT)</td>
        <td style="padding: 8px; border: 1px solid black;">POST /api/auth/logout con token vigente</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=LOGOUT y se revoca el token</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-03</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de bloqueo de cuenta (LOCKOUT)</td>
        <td style="padding: 8px; border: 1px solid black;">5 intentos de login fallidos consecutivos</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=LOCKOUT</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-04</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de restablecimiento de contraseña (RESET_PASSWORD)</td>
        <td style="padding: 8px; border: 1px solid black;">POST /api/auth/reset-password con token válido</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=RESET_PASSWORD</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-05</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de creación de usuario (CREATE_USER)</td>
        <td style="padding: 8px; border: 1px solid black;">POST /api/usuarios con datos válidos, sesión de Administrador</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=CREATE_USER</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-06</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de edición de usuario (UPDATE_USER)</td>
        <td style="padding: 8px; border: 1px solid black;">PUT /api/usuarios/{id} con datos válidos</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=UPDATE_USER</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-07</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de activación/desactivación de cuenta (TOGGLE_STATUS)</td>
        <td style="padding: 8px; border: 1px solid black;">PATCH /api/usuarios/{id}/estado</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=TOGGLE_STATUS</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-08</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro automático de desbloqueo manual de cuenta (UNLOCK_USER)</td>
        <td style="padding: 8px; border: 1px solid black;">POST /api/usuarios/{id}/desbloquear</td>
        <td style="padding: 8px; border: 1px solid black;">Se crea un log con actionType=UNLOCK_USER</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-09</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Integridad de campos de trazabilidad forense en cada registro</td>
        <td style="padding: 8px; border: 1px solid black;">Registro de auditoría cualquiera</td>
        <td style="padding: 8px; border: 1px solid black;">Timestamp, actionType, userId, ipAddress y details presentes y no vacíos</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-10</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Orden cronológico descendente del listado de auditoría</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit con múltiples eventos generados en secuencia</td>
        <td style="padding: 8px; border: 1px solid black;">Los registros se devuelven del más reciente al más antiguo</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-11</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Paginación del listado de auditoría</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit?pageNumber=1&pageSize=10</td>
        <td style="padding: 8px; border: 1px solid black;">Respuesta con totalRecords, pageNumber, pageSize e items acotados al tamaño solicitado</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-12</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Filtro de auditoría por tipo de acción</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit?actionType=LOGIN_2FA</td>
        <td style="padding: 8px; border: 1px solid black;">Todos los items devueltos tienen actionType=LOGIN_2FA</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-13</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Filtro de auditoría por rango de fechas</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit?fromDate=...&toDate=...</td>
        <td style="padding: 8px; border: 1px solid black;">Solo se devuelven registros con timestamp dentro del rango indicado</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-14</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Filtro de auditoría por usuario responsable del evento</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit?userId={id}</td>
        <td style="padding: 8px; border: 1px solid black;">Solo se devuelven registros generados por ese usuario</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-15</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Listado de tipos de acción distintos para poblar el filtro del frontend</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit/action-types</td>
        <td style="padding: 8px; border: 1px solid black;">Devuelve la lista ordenada y sin duplicados de actionType existentes</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-16</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Exportación del registro de auditoría a CSV</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit/export (con o sin filtros)</td>
        <td style="padding: 8px; border: 1px solid black;">Archivo text/csv con encabezado Fecha,TipoDeAccion,UsuarioId,DireccionIP,Detalles</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-17</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Inmutabilidad del registro de auditoría</td>
        <td style="padding: 8px; border: 1px solid black;">DELETE /api/audit/{id} (verbo no expuesto)</td>
        <td style="padding: 8px; border: 1px solid black;">No existe operación de edición/borrado; la petición nunca resulta en 200 OK</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-18</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Restricción de acceso al módulo de auditoría por rol</td>
        <td style="padding: 8px; border: 1px solid black;">GET /api/audit con sesión de rol Docente</td>
        <td style="padding: 8px; border: 1px solid black;">403 Forbidden — solo el rol Administrador puede consultar la auditoría</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
</table>

---

## Desglose Analítico por Caso de Prueba

### CP3-01: Registro automático de inicio de sesión (LOGIN_2FA)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Al completar `POST /api/auth/verify-2fa` con un código válido, `AuthController` invoca `_auditService.LogEventAsync("LOGIN_2FA", user.Id, ipAddress, "Inicio de sesión 2FA exitoso")`. El registro queda disponible de inmediato en `GET /api/audit` con ese `actionType`.

**Análisis de Seguridad y Desarrollo:**

> Todo inicio de sesión exitoso queda trazado con el usuario y la IP de origen, cerrando el requisito de trazabilidad de accesos exigido por CPGIC.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-01]</p>
    <img src="../Evidencias/evidencia_cp3_01.png" alt="Evidencia CP3-01" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-02: Registro automático de cierre de sesión (LOGOUT)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`POST /api/auth/logout` extrae `jti`/`exp` del token del portador, inserta la entrada en `RevokedTokens` y registra `LogEventAsync("LOGOUT", userId, ipAddress, "Cierre de sesión, token revocado.")`. El token queda invalidado para cualquier petición posterior (ver CP1-13).

**Análisis de Seguridad y Desarrollo:**

> El cierre de sesión no es solo un evento de cliente: revoca el token en el servidor y dicha revocación queda auditada.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-02]</p>
    <img src="../Evidencias/evidencia_cp3_02.png" alt="Evidencia CP3-02" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-03: Registro automático de bloqueo de cuenta (LOCKOUT)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Al superar 5 intentos fallidos de login, ASP.NET Core Identity bloquea la cuenta y `AuthController` registra `LogEventAsync("LOCKOUT", user.Id, ip, "Cuenta bloqueada por superar el límite de 5 intentos fallidos.")`.

**Análisis de Seguridad y Desarrollo:**

> El bloqueo por fuerza bruta no es silencioso: queda como evidencia consultable por el Administrador, incluyendo la cuenta afectada.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-03]</p>
    <img src="../Evidencias/evidencia_cp3_03.png" alt="Evidencia CP3-03" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-04: Registro automático de restablecimiento de contraseña (RESET_PASSWORD)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`POST /api/auth/reset-password` con un token de recuperación válido registra `LogEventAsync("RESET_PASSWORD", user.Id, ipAddress, "Contraseña restablecida correctamente")` tras la actualización exitosa de la contraseña.

**Análisis de Seguridad y Desarrollo:**

> Un cambio de credenciales es un evento sensible; queda vinculado al usuario afectado para auditoría posterior ante cualquier disputa de acceso.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-04]</p>
    <img src="../Evidencias/evidencia_cp3_03.png" alt="Evidencia CP3-04" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-05: Registro automático de creación de usuario (CREATE_USER)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`UsuariosController.CrearUsuario` registra `LogEventAsync("CREATE_USER", currentUserId, ipAddress, $"Creó al usuario {user.Email}")`, dejando constancia de qué Administrador dio de alta a cada cuenta institucional.

**Análisis de Seguridad y Desarrollo:**

> La gestión de identidades (HU-06/07) queda completamente trazada: se sabe quién creó cada cuenta y cuándo.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-05]</p>
    <img src="../Evidencias/evidencia_cp3_05.png" alt="Evidencia CP3-05" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-06: Registro automático de edición de usuario (UPDATE_USER)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`UsuariosController.UpdateUsuario` registra `LogEventAsync("UPDATE_USER", currentUserId, ipAddress, $"Actualizó al usuario {user.Email}")` tras aplicar los cambios permitidos (nombre, apellido, rol; el correo institucional es inmutable, ver CP2-08).

**Análisis de Seguridad y Desarrollo:**

> Toda modificación de un perfil queda asociada al Administrador que la ejecutó, sin excepciones.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-06]</p>
    <img src="../Evidencias/evidencia_cp3_06.png" alt="Evidencia CP3-06" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-07: Registro automático de activación/desactivación de cuenta (TOGGLE_STATUS)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`UsuariosController.ToggleEstadoUsuario` registra `LogEventAsync("TOGGLE_STATUS", currentUserId, ipAddress, $"El administrador {accion} al usuario {user.Email}")`, cubriendo tanto la activación como la desactivación de cuentas.

**Análisis de Seguridad y Desarrollo:**

> Suspender o restaurar el acceso de una cuenta es una acción con impacto directo en seguridad; queda registrada igual que el resto de operaciones sobre usuarios.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-07]</p>
    <img src="../Evidencias/evidencia_cp3_07.png" alt="Evidencia CP3-07" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-08: Registro automático de desbloqueo manual de cuenta (UNLOCK_USER)

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`UsuariosController.DesbloquearUsuario` registra `LogEventAsync("UNLOCK_USER", currentUserId, ipAddress, $"Desbloqueó al usuario {user.Email} y envió enlace de recuperación")`, distinguiendo el desbloqueo manual por Administrador del autobloqueo por intentos fallidos (CP3-03).

**Análisis de Seguridad y Desarrollo:**

> El desbloqueo administrativo queda diferenciado del bloqueo automático en el propio registro, permitiendo reconstruir la secuencia completa del incidente.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-08]</p>
    <img src="../Evidencias/evidencia_cp3_08.png" alt="Evidencia CP3-08" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-09: Integridad de campos de trazabilidad forense en cada registro

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Cada fila de `LogAuditoria` expone `Timestamp`, `ActionType`, `UserId`, `IpAddress` y `Details` (`Models/LogAuditoria.cs`). Se verifica que ninguno de estos campos llegue nulo o vacío en un registro real devuelto por `GET /api/audit`.

**Análisis de Seguridad y Desarrollo:**

> Un log sin IP o sin usuario asociado no sirve como evidencia forense; el esquema obliga a que los cinco campos viajen siempre completos.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-09]</p>
    <img src="../Evidencias/evidencia_cp3_09.png" alt="Evidencia CP3-09" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-10: Orden cronológico descendente del listado de auditoría

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`ApplyFilters()` en `AuditController` aplica `OrderByDescending(l => l.Timestamp)` antes de paginar. Al generar varios eventos en secuencia y consultarlos, el orden devuelto coincide exactamente con el orden descendente esperado.

**Análisis de Seguridad y Desarrollo:**

> El Administrador necesita ver primero lo más reciente sin tener que ordenar manualmente miles de registros.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-10]</p>
    <img src="../Evidencias/evidencia_cp3_10.png" alt="Evidencia CP3-10" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-11: Paginación del listado de auditoría

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`GetAuditLogs` aplica `Skip((pageNumber - 1) * pageSize).Take(pageSize)` sobre la consulta ya filtrada y ordenada, devolviendo `TotalRecords`, `PageNumber`, `PageSize` e `Items` en la misma respuesta.

**Análisis de Seguridad y Desarrollo:**

> Con miles de eventos acumulados, el listado sigue siendo utilizable en el frontend sin cargar toda la tabla de una vez.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-11]</p>
    <img src="../Evidencias/evidencia_cp3_11.png" alt="Evidencia CP3-11" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-12: Filtro de auditoría por tipo de acción

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`ApplyFilters()` agrega `Where(l => l.ActionType == actionType)` cuando el parámetro de consulta `actionType` viene presente. Todos los ítems de la respuesta filtrada comparten ese mismo valor.

**Análisis de Seguridad y Desarrollo:**

> Permite aislar, por ejemplo, únicamente los intentos de login o únicamente las desactivaciones de cuenta durante una investigación puntual.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-12]</p>
    <img src="../Evidencias/evidencia_cp3_12.png" alt="Evidencia CP3-12" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-13: Filtro de auditoría por rango de fechas

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`ApplyFilters()` agrega `Where(l => l.Timestamp >= fromDate)` y `Where(l => l.Timestamp <= toDate)` cuando esos parámetros llegan informados, permitiendo acotar la ventana temporal de la consulta.

**Análisis de Seguridad y Desarrollo:**

> Facilita reconstruir la actividad del sistema durante una ventana específica (por ejemplo, el día de un incidente reportado).

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-13]</p>
    <img src="../Evidencias/evidencia_cp3_13.png" alt="Evidencia CP3-13" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-14: Filtro de auditoría por usuario responsable del evento

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`ApplyFilters()` agrega `Where(l => l.UserId == userId)` cuando se informa ese parámetro, devolviendo únicamente los eventos generados por esa cuenta específica.

**Análisis de Seguridad y Desarrollo:**

> Permite auditar la actividad completa de un usuario puntual (por ejemplo, ante sospecha de uso indebido de privilegios).

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-14]</p>
    <img src="../Evidencias/evidencia_cp3_14.png" alt="Evidencia CP3-14" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-15: Listado de tipos de acción distintos para poblar el filtro del frontend

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`GET /api/audit/action-types` devuelve `_context.LogAuditoria.Select(l => l.ActionType).Distinct().OrderBy(t => t)`. El frontend usa esta lista para construir el `<select>` de filtro sin valores hardcodeados que puedan desincronizarse del backend.

**Análisis de Seguridad y Desarrollo:**

> Si en el futuro se añade un nuevo `actionType`, el filtro lo refleja automáticamente sin requerir un cambio en el frontend.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-15]</p>
    <img src="../Evidencias/evidencia_cp3_15.png" alt="Evidencia CP3-15" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-16: Exportación del registro de auditoría a CSV

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`GET /api/audit/export` aplica los mismos filtros que el listado (hasta 5000 filas) y genera un archivo `text/csv` con encabezado `Fecha,TipoDeAccion,UsuarioId,DireccionIP,Detalles`, usando `CsvEscape()` para proteger comas, comillas y saltos de línea dentro de `Details`. Corresponde a la Figura 2.12 de la tesis.

**Análisis de Seguridad y Desarrollo:**

> El Administrador puede extraer evidencia de auditoría en un formato portable para entregarla a control interno o a un tercero, sin depender de acceso directo a la base de datos.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-16]</p>
    <img src="../Evidencias/evidencia_cp3_16.png" alt="Evidencia CP3-16" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-17: Inmutabilidad del registro de auditoría

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`AuditController` únicamente expone verbos `GET` (`/`, `/action-types`, `/export`). No existe ninguna ruta `PUT`/`PATCH`/`DELETE` sobre `LogAuditoria`; cualquier intento de invocar un verbo de escritura contra ese recurso nunca resulta en `200 OK`.

**Análisis de Seguridad y Desarrollo:**

> Un registro de auditoría que pudiera editarse o borrarse dejaría de ser evidencia confiable; el diseño de la API lo impide estructuralmente, no solo por validación.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-17]</p>
    <img src="../Evidencias/evidencia_cp3_17.png" alt="Evidencia CP3-17" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-18: Restricción de acceso al módulo de auditoría por rol

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
`AuditController` está decorado a nivel de clase con `[Authorize(Roles = "Administrador")]`. Un token válido de un rol distinto (Docente, Presidente, Miembro) recibe `403 Forbidden` al intentar consultar cualquiera de sus endpoints.

**Análisis de Seguridad y Desarrollo:**

> El control de acceso basado en permisos granulares (CPGIC) limita la visibilidad de la auditoría exclusivamente al rol Administrador, evitando que otros roles vean actividad ajena a su alcance.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-18]</p>
    <img src="../Evidencias/evidencia_cp3_18.png" alt="Evidencia CP3-18" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---
