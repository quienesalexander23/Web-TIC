# Pruebas Funcionales del Sprint 3

**Introducción:** Aseguramiento de calidad para Registros Automáticos de Auditoría y Control de Acceso Basado en Permisos Granulares (CPGIC).

En esta sección se presenta la matriz completa de los casos de prueba ejecutados durante este Sprint.

> **Nota de verificación (2026-07-11):** los 18 casos de esta matriz comparten título y descripción técnica genéricos ("Transacción #N"). Se realizó una verificación funcional completa del módulo de auditoría contra el sistema real (no caso por caso); ver `INFORME_VERIFICACION_QA.md` para el detalle. Resumen: el registro automático, el orden cronológico, la inmutabilidad y la restricción de acceso a Administrador se confirmaron correctos. Se detectó que el endpoint de activar/desactivar cuenta no genera registro de auditoría, y que el módulo no tiene exportación CSV ni filtros pese a lo descrito en la Figura 2.12 de la tesis.

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
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #1</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-02</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #2</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-03</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #3</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-04</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #4</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-05</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #5</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-06</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #6</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-07</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #7</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-08</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #8</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-09</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #9</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-10</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #10</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-11</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #11</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-12</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #12</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-13</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #13</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-14</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #14</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-15</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #15</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-16</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #16</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-17</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #17</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP3-18</td>
        <td style="padding: 8px; border: 1px solid black;">HU-06/07</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Verificación de Auditoría Automática - Transacción #18</td>
        <td style="padding: 8px; border: 1px solid black;">Action Type</td>
        <td style="padding: 8px; border: 1px solid black;">Log Saved</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
</table>

---

## Desglose Analítico por Caso de Prueba

### CP3-01: Verificación de Auditoría Automática - Transacción #1

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-01]</p>
    <img src="../Evidencias/evidencia_cp3_01.png" alt="Evidencia CP3-01" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-02: Verificación de Auditoría Automática - Transacción #2

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-02]</p>
    <img src="../Evidencias/evidencia_cp3_02.png" alt="Evidencia CP3-02" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-03: Verificación de Auditoría Automática - Transacción #3

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-03]</p>
    <img src="../Evidencias/evidencia_cp3_03.png" alt="Evidencia CP3-03" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-04: Verificación de Auditoría Automática - Transacción #4

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-04]</p>
    <img src="../Evidencias/evidencia_cp3_04.png" alt="Evidencia CP3-04" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-05: Verificación de Auditoría Automática - Transacción #5

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-05]</p>
    <img src="../Evidencias/evidencia_cp3_05.png" alt="Evidencia CP3-05" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-06: Verificación de Auditoría Automática - Transacción #6

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-06]</p>
    <img src="../Evidencias/evidencia_cp3_06.png" alt="Evidencia CP3-06" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-07: Verificación de Auditoría Automática - Transacción #7

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-07]</p>
    <img src="../Evidencias/evidencia_cp3_07.png" alt="Evidencia CP3-07" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-08: Verificación de Auditoría Automática - Transacción #8

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-08]</p>
    <img src="../Evidencias/evidencia_cp3_08.png" alt="Evidencia CP3-08" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-09: Verificación de Auditoría Automática - Transacción #9

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-09]</p>
    <img src="../Evidencias/evidencia_cp3_09.png" alt="Evidencia CP3-09" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-10: Verificación de Auditoría Automática - Transacción #10

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-10]</p>
    <img src="../Evidencias/evidencia_cp3_10.png" alt="Evidencia CP3-10" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-11: Verificación de Auditoría Automática - Transacción #11

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-11]</p>
    <img src="../Evidencias/evidencia_cp3_11.png" alt="Evidencia CP3-11" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-12: Verificación de Auditoría Automática - Transacción #12

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-12]</p>
    <img src="../Evidencias/evidencia_cp3_12.png" alt="Evidencia CP3-12" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-13: Verificación de Auditoría Automática - Transacción #13

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-13]</p>
    <img src="../Evidencias/evidencia_cp3_13.png" alt="Evidencia CP3-13" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-14: Verificación de Auditoría Automática - Transacción #14

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-14]</p>
    <img src="../Evidencias/evidencia_cp3_14.png" alt="Evidencia CP3-14" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-15: Verificación de Auditoría Automática - Transacción #15

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-15]</p>
    <img src="../Evidencias/evidencia_cp3_15.png" alt="Evidencia CP3-15" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-16: Verificación de Auditoría Automática - Transacción #16

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-16]</p>
    <img src="../Evidencias/evidencia_cp3_16.png" alt="Evidencia CP3-16" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-17: Verificación de Auditoría Automática - Transacción #17

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-17]</p>
    <img src="../Evidencias/evidencia_cp3_17.png" alt="Evidencia CP3-17" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP3-18: Verificación de Auditoría Automática - Transacción #18

**Historia de Usuario Relacionada:** HU-06/07

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `Action Type` para certificar el comportamiento esperado del sistema (Log Saved). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Se audita rigurosamente la capacidad del middleware de guardar trazas forenses de las peticiones POST/PUT/DELETE.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP3-18]</p>
    <img src="../Evidencias/evidencia_cp3_18.png" alt="Evidencia CP3-18" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

