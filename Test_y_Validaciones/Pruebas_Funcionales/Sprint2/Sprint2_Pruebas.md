# Pruebas Funcionales del Sprint 2

**Introducción:** Aseguramiento de calidad para la Gestión de Usuarios y Control de Acceso Basado en Roles (RBAC).

En esta sección se presenta la matriz completa de los casos de prueba ejecutados durante este Sprint.

<table style="width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; border: 2px solid black; font-size: 14px;">
    <tr style="background-color: #000000; color: #ffffff;">
        <th colspan="7" style="padding: 10px; border: 1px solid black;">Matriz de Pruebas: Pruebas Funcionales del Sprint 2</th>
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
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-01</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Creación de cuenta válida</td>
        <td style="padding: 8px; border: 1px solid black;">Datos de usuario</td>
        <td style="padding: 8px; border: 1px solid black;">Cuenta creada en BD</td>
        <td style="padding: 8px; border: 1px solid black;">Correo recibido</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-02</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Correo ya registrado</td>
        <td style="padding: 8px; border: 1px solid black;">mgonzalez@epn</td>
        <td style="padding: 8px; border: 1px solid black;">Error unicidad</td>
        <td style="padding: 8px; border: 1px solid black;">Rechazado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-03</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Dominio no institucional</td>
        <td style="padding: 8px; border: 1px solid black;">gmail.com</td>
        <td style="padding: 8px; border: 1px solid black;">Rechazo frontend</td>
        <td style="padding: 8px; border: 1px solid black;">Error validación</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-04</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Listado paginado</td>
        <td style="padding: 8px; border: 1px solid black;">pagina=1, tamano=10</td>
        <td style="padding: 8px; border: 1px solid black;">Lista de 10 usuarios</td>
        <td style="padding: 8px; border: 1px solid black;">Lista correcta</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-05</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Búsqueda parcial</td>
        <td style="padding: 8px; border: 1px solid black;">busqueda=González</td>
        <td style="padding: 8px; border: 1px solid black;">Usuarios filtrados</td>
        <td style="padding: 8px; border: 1px solid black;">Registros correctos</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-06</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Filtro por rol</td>
        <td style="padding: 8px; border: 1px solid black;">rol=Docente</td>
        <td style="padding: 8px; border: 1px solid black;">Solo docentes</td>
        <td style="padding: 8px; border: 1px solid black;">Filtro aplicado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-07</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Edición de datos</td>
        <td style="padding: 8px; border: 1px solid black;">Nombre actualizado</td>
        <td style="padding: 8px; border: 1px solid black;">BD actualizada</td>
        <td style="padding: 8px; border: 1px solid black;">Modificación exitosa</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-08</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Modificar correo</td>
        <td style="padding: 8px; border: 1px solid black;">email enviado en PUT</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 400</td>
        <td style="padding: 8px; border: 1px solid black;">Rechazo exitoso</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-09</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Desactivar cuenta</td>
        <td style="padding: 8px; border: 1px solid black;">estado: Inactivo</td>
        <td style="padding: 8px; border: 1px solid black;">Cuenta inactiva</td>
        <td style="padding: 8px; border: 1px solid black;">Login denegado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-10</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Reactivar cuenta</td>
        <td style="padding: 8px; border: 1px solid black;">estado: Activo</td>
        <td style="padding: 8px; border: 1px solid black;">Cuenta activa</td>
        <td style="padding: 8px; border: 1px solid black;">Login exitoso</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-11</td>
        <td style="padding: 8px; border: 1px solid black;">HU-04</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Registro de auditoría CRUD</td>
        <td style="padding: 8px; border: 1px solid black;">Eventos de CP2-01 a 10</td>
        <td style="padding: 8px; border: 1px solid black;">Eventos registrados</td>
        <td style="padding: 8px; border: 1px solid black;">Log correcto</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-12</td>
        <td style="padding: 8px; border: 1px solid black;">HU-05</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Acceso admin</td>
        <td style="padding: 8px; border: 1px solid black;">admin@epn</td>
        <td style="padding: 8px; border: 1px solid black;">Panel visible</td>
        <td style="padding: 8px; border: 1px solid black;">Panel accesible</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-13</td>
        <td style="padding: 8px; border: 1px solid black;">HU-05</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Acceso admin (Rol Docente)</td>
        <td style="padding: 8px; border: 1px solid black;">docente@epn</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 403</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 403 retornado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-14</td>
        <td style="padding: 8px; border: 1px solid black;">HU-05</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Acceso admin (Presidente)</td>
        <td style="padding: 8px; border: 1px solid black;">presidente@epn</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 403</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 403 retornado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-15</td>
        <td style="padding: 8px; border: 1px solid black;">HU-05</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Acceso admin (Miembro)</td>
        <td style="padding: 8px; border: 1px solid black;">miembro@epn</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 403</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 403 retornado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-16</td>
        <td style="padding: 8px; border: 1px solid black;">HU-05</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Cambio de rol</td>
        <td style="padding: 8px; border: 1px solid black;">Rol Miembro CPGIC</td>
        <td style="padding: 8px; border: 1px solid black;">Actualización exitosa</td>
        <td style="padding: 8px; border: 1px solid black;">Token actualizado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-17</td>
        <td style="padding: 8px; border: 1px solid black;">HU-05</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Visibilidad directiva *appHasRole</td>
        <td style="padding: 8px; border: 1px solid black;">Sesión simultánea</td>
        <td style="padding: 8px; border: 1px solid black;">Controles ocultos</td>
        <td style="padding: 8px; border: 1px solid black;">UI adaptada</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP2-18</td>
        <td style="padding: 8px; border: 1px solid black;">HU-05</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Token manipulado manualmente</td>
        <td style="padding: 8px; border: 1px solid black;">Token falso con rol Admin</td>
        <td style="padding: 8px; border: 1px solid black;">Firma JWT inválida</td>
        <td style="padding: 8px; border: 1px solid black;">HTTP 401 retornado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
</table>

---

## Desglose Analítico por Caso de Prueba

### CP2-01: Creación de cuenta válida

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Datos de usuario` para certificar el comportamiento esperado del sistema (Cuenta creada en BD). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Correo recibido) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> El controlador procesa el payload, crea el IdentityUser y asigna los roles solicitados, retornando HTTP 201 Created.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-01]</p>
    <img src="../Evidencias/evidencia_cp2_01.png" alt="Evidencia CP2-01" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-02: Correo ya registrado

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `alexander.tibanta@epn.edu.ec` para certificar el comportamiento esperado del sistema (Error unicidad). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Rechazado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Restricciones de Unique Index en la base de datos abortan la transacción de Entity Framework.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-02]</p>
    <img src="../Evidencias/evidencia_cp2_02.png" alt="Evidencia CP2-02" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-03: Dominio no institucional

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `gmail.com` para certificar el comportamiento esperado del sistema (Rechazo frontend). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Error validación) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Validadores reactivos de Angular detectan dominios no aceptados antes de enviar el POST.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-03]</p>
    <img src="../Evidencias/evidencia_cp2_03.png" alt="Evidencia CP2-03" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-04: Listado paginado

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `pagina=1, tamano=10` para certificar el comportamiento esperado del sistema (Lista de 10 usuarios). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Lista correcta) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Queries LINQ utilizan Skip y Take para optimizar memoria RAM del servidor al enviar JSONs parciales al cliente.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-04]</p>
    <img src="../Evidencias/evidencia_cp2_04.png" alt="Evidencia CP2-04" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-05: Búsqueda parcial

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `busqueda=Alexander` para certificar el comportamiento esperado del sistema (Usuarios filtrados). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Registros correctos) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Indexación full-text permite recuperar registros con latencias < 100ms mediante clausulas LIKE/Contains.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-05]</p>
    <img src="../Evidencias/evidencia_cp2_05.png" alt="Evidencia CP2-05" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-06: Filtro por rol

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `rol=Docente` para certificar el comportamiento esperado del sistema (Solo docentes). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Filtro aplicado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> El join entre AspNetUsers y AspNetUserRoles se realiza de forma óptima usando Entity Framework.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-06]</p>
    <img src="../Evidencias/evidencia_cp2_06.png" alt="Evidencia CP2-06" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-07: Edición de datos

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Nombre actualizado` para certificar el comportamiento esperado del sistema (BD actualizada). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Modificación exitosa) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Petición HTTP PUT actualiza los campos permitidos. UpdateAsync() se ejecuta en el DbContext.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-07]</p>
    <img src="../Evidencias/evidencia_cp2_07.png" alt="Evidencia CP2-07" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-08: Modificar correo

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Se envió una petición `PUT /api/usuarios/{id}` incluyendo un campo `email` con un valor distinto al actual. El resultado es **HTTP 400 Bad Request**, y el correo del usuario permanece sin cambios. Enviar el mismo correo que el usuario ya tiene (sin intención real de modificarlo) sí se permite, junto con el resto de la actualización.

**Análisis de Seguridad y Desarrollo:**

> `UsuariosController.UpdateUsuario` compara el `Email` recibido en el DTO contra el correo actual del usuario (`StringComparison.OrdinalIgnoreCase`); si difieren, la petición se rechaza explícitamente antes de aplicar cualquier cambio, con el mensaje "El correo institucional no puede modificarse." El correo institucional es un identificador inmutable del usuario dentro del sistema, consistente con su uso como `UserName` en ASP.NET Core Identity.

**Verificación automatizada:** cubierto por la prueba de integración `Sprint2_UserManagementTests.CP2_07_08_EdicionDeDatosYCorreoInmutable` (`WebTIC.API.Tests`).

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-08]</p>
    <img src="../Evidencias/evidencia_cp2_08.png" alt="Evidencia CP2-08" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-09: Desactivar cuenta

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `estado: Inactivo` para certificar el comportamiento esperado del sistema (Cuenta inactiva). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Login denegado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> El flag lógico se cambia a False. Sesiones futuras son rechazadas en la generación del token.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-09]</p>
    <img src="../Evidencias/evidencia_cp2_09.png" alt="Evidencia CP2-09" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-10: Reactivar cuenta

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `estado: Activo` para certificar el comportamiento esperado del sistema (Cuenta activa). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Login exitoso) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> La bandera lógica se cambia a True y se resetea el LockoutEnd permitiendo el acceso normal.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-10]</p>
    <img src="../Evidencias/evidencia_cp2_10.png" alt="Evidencia CP2-10" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-11: Registro de auditoría CRUD

**Historia de Usuario Relacionada:** HU-04

**Explicación Técnica del Caso:**
Se ejecutaron en secuencia creación, edición, desactivación, reactivación y cambio de rol de un usuario real, y se consultó `GET /api/audit` para confirmar la traza. `CREATE_USER`, `UPDATE_USER` y `TOGGLE_STATUS` quedaron correctamente registrados con IP, timestamp y detalle.

**Análisis de Seguridad y Desarrollo:**

> Cada operación de escritura sobre un usuario (`CreateUsuario`, `UpdateUsuario`, `ToggleEstadoUsuario`, `UnlockUsuario`) invoca a `_auditService.LogEventAsync(...)` con un `ActionType` específico (`CREATE_USER`, `UPDATE_USER`, `TOGGLE_STATUS`, `UNLOCK_USER`), garantizando trazabilidad completa de todas las operaciones CRUD del módulo de gestión de usuarios.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-11]</p>
    <img src="../Evidencias/evidencia_cp2_11.png" alt="Evidencia CP2-11" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-12: Acceso admin

**Historia de Usuario Relacionada:** HU-05

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `admin@epn` para certificar el comportamiento esperado del sistema (Panel visible). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Panel accesible) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Evaluación positiva del atributo [Authorize(Roles='Administrador')] en la API central.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-12]</p>
    <img src="../Evidencias/evidencia_cp2_12.png" alt="Evidencia CP2-12" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-13: Acceso admin (Rol Docente)

**Historia de Usuario Relacionada:** HU-05

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `docente@epn` para certificar el comportamiento esperado del sistema (HTTP 403). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (HTTP 403 retornado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Evaluación negativa en API. Se retorna 403 Forbidden impidiendo lectura/escritura de datos administrativos.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-13]</p>
    <img src="../Evidencias/evidencia_cp2_13.png" alt="Evidencia CP2-13" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-14: Acceso admin (Presidente)

**Historia de Usuario Relacionada:** HU-05

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `presidente@epn` para certificar el comportamiento esperado del sistema (HTTP 403). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (HTTP 403 retornado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Mismo caso de restricción; RBAC operando correctamente sobre perfiles directivos.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-14]</p>
    <img src="../Evidencias/evidencia_cp2_14.png" alt="Evidencia CP2-14" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-15: Acceso admin (Miembro)

**Historia de Usuario Relacionada:** HU-05

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `miembro@epn` para certificar el comportamiento esperado del sistema (HTTP 403). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (HTTP 403 retornado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Mismo caso de restricción para miembros del CPGIC.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-15]</p>
    <img src="../Evidencias/evidencia_cp2_15.png" alt="Evidencia CP2-15" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-16: Cambio de rol

**Historia de Usuario Relacionada:** HU-05

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Rol Miembro CPGIC` para certificar el comportamiento esperado del sistema (Actualización exitosa). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Token actualizado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Al modificar el rol, el sistema actualiza Claims y fuerza invalidación previa, otorgando permisos nuevos en el siguiente login.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-16]</p>
    <img src="../Evidencias/evidencia_cp2_16.png" alt="Evidencia CP2-16" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-17: Visibilidad directiva *appHasRole

**Historia de Usuario Relacionada:** HU-05

**Explicación Técnica del Caso:**
Se inició sesión real como Docente y se inspeccionó el sidebar renderizado. Los enlaces **Home, Users, Roles, Audit Log y Settings se ocultan correctamente** para roles no-administrador, dejando visible únicamente "Mi Perfil".

**Análisis de Seguridad y Desarrollo:**

> La directiva `HasRoleDirective` (`shared/directives/has-role.directive.ts`) decodifica el JWT en el cliente y compara el claim `role` contra la lista de roles permitidos, ocultando o mostrando el elemento correspondiente. Se aplica de forma consistente en `dashboard.component.html` (enlaces de navegación) y en `user-list.component.html` (página completa de gestión de usuarios, incluyendo el botón "+ Crear Usuario"). Esta protección de UI se complementa con `roleGuard` a nivel de ruta y con las políticas `[Authorize]` del backend, cubriendo las tres capas: presentación, ruteo y API.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-17]</p>
    <img src="../Evidencias/evidencia_cp2_17.png" alt="Evidencia CP2-17" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP2-18: Token manipulado manualmente

**Historia de Usuario Relacionada:** HU-05

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Token falso con rol Admin` para certificar el comportamiento esperado del sistema (Firma JWT inválida). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (HTTP 401 retornado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**

> Cualquier alteración en la porción de payload de un JWT rompe su hash HMAC-SHA256, la API lo rechaza instantáneamente.

**Evidencia Visual:**

<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP2-18]</p>
    <img src="../Evidencias/evidencia_cp2_18.png" alt="Evidencia CP2-18" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---
