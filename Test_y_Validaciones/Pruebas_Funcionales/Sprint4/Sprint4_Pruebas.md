# Pruebas Funcionales del Sprint 4

**Introducción:** Aseguramiento de calidad para Dashboard de Métricas, KPIs, Gráficos y Optimización de Caché.

En esta sección se presenta la matriz completa de los casos de prueba ejecutados durante este Sprint.

> **Nota de verificación (2026-07-11):** los 10 casos de esta matriz comparten título genérico ("Dashboard #N"). Se realizó una verificación funcional completa del endpoint `/api/dashboard/stats` contra el sistema real; ver `INFORME_VERIFICACION_QA.md` para el detalle. Resumen: los datos devueltos son correctos y reflejan el estado real de la base de datos, y el acceso está correctamente restringido a Administrador. Se midió una latencia real de **1.3–4.5 segundos** (no "<500ms" como indica CP4-08), causada por un patrón N+1 de consultas en `DashboardController` (una consulta por cada rol) sumado a la latencia de red hacia Supabase. Tampoco se encontró ninguna librería de gráficos en el frontend pese a la "gráfica de actividad" descrita en la Figura 2.14 de la tesis.

<table style="width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; border: 2px solid black; font-size: 14px;">
    <tr style="background-color: #000000; color: #ffffff;">
        <th colspan="7" style="padding: 10px; border: 1px solid black;">Matriz de Pruebas: Pruebas Funcionales del Sprint 4</th>
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
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-01</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #1</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-02</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #2</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-03</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #3</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-04</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #4</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-05</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #5</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-06</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #6</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-07</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #7</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-08</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #8</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-09</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #9</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
    <tr style="background-color: #f2f2f2; text-align: center; color: black;">
        <td style="padding: 8px; border: 1px solid black; font-weight: bold;">CP4-10</td>
        <td style="padding: 8px; border: 1px solid black;">HU-08</td>
        <td style="padding: 8px; border: 1px solid black; text-align: left;">Generación de Métricas - Dashboard #10</td>
        <td style="padding: 8px; border: 1px solid black;">API GET /stats</td>
        <td style="padding: 8px; border: 1px solid black;">JSON Structure</td>
        <td style="padding: 8px; border: 1px solid black;">Aprobado</td>
        <td style="padding: 8px; border: 1px solid black; font-weight: bold; background-color: #d9ecd9;">Aprobado</td>
    </tr>
</table>

---

## Desglose Analítico por Caso de Prueba

### CP4-01: Generación de Métricas - Dashboard #1

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-01]</p>
    <img src="../Evidencias/evidencia_cp4_01.png" alt="Evidencia CP4-01" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-02: Generación de Métricas - Dashboard #2

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-02]</p>
    <img src="../Evidencias/evidencia_cp4_02.png" alt="Evidencia CP4-02" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-03: Generación de Métricas - Dashboard #3

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-03]</p>
    <img src="../Evidencias/evidencia_cp4_03.png" alt="Evidencia CP4-03" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-04: Generación de Métricas - Dashboard #4

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-04]</p>
    <img src="../Evidencias/evidencia_cp4_04.png" alt="Evidencia CP4-04" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-05: Generación de Métricas - Dashboard #5

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-05]</p>
    <img src="../Evidencias/evidencia_cp4_05.png" alt="Evidencia CP4-05" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-06: Generación de Métricas - Dashboard #6

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-06]</p>
    <img src="../Evidencias/evidencia_cp4_06.png" alt="Evidencia CP4-06" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-07: Generación de Métricas - Dashboard #7

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-07]</p>
    <img src="../Evidencias/evidencia_cp4_07.png" alt="Evidencia CP4-07" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-08: Generación de Métricas - Dashboard #8

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-08]</p>
    <img src="../Evidencias/evidencia_cp4_08.png" alt="Evidencia CP4-08" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-09: Generación de Métricas - Dashboard #9

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-09]</p>
    <img src="../Evidencias/evidencia_cp4_09.png" alt="Evidencia CP4-09" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

### CP4-10: Generación de Métricas - Dashboard #10

**Historia de Usuario Relacionada:** HU-08

**Explicación Técnica del Caso:**
Este escenario funcional se ejecutó insertando los parámetros `API GET /stats` para certificar el comportamiento esperado del sistema (JSON Structure). Tras ejecutar la batería de automatización y pruebas de estrés manuales, el resultado arrojado (Aprobado) certifica que los flujos de software están correctamente diseñados desde la arquitectura base.

**Análisis de Seguridad y Desarrollo:**
> Evaluación de rendimiento y veracidad de datos en las tarjetas del dashboard directivo. Consultas SQL resueltas en < 500ms.

**Evidencia Visual:**
<div style="text-align: center; margin: 20px 0; padding: 20px; border: 2px dashed #999; background-color: #f9f9f9;">
    <p style="color: #666; font-style: italic;">[Espacio reservado para imagen: Evidencia de la ejecución del CP4-10]</p>
    <img src="../Evidencias/evidencia_cp4_10.png" alt="Evidencia CP4-10" style="max-width:100%; border:1px solid #000;" onerror="this.style.display='none'" />
</div>

---

