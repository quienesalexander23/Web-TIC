import os

base_dir = r"C:\Users\pasante\PasanteKM\OneDrive - WellPerf\Escritorio\TESIS-EPN\Web-TIC\HU"

stories = {
    "Sprint1": [
        {
            "id": "HU-01",
            "title": "Autenticación Segura con JWT y 2FA",
            "role": "Usuario Final",
            "want": "iniciar sesión en el sistema mediante mis credenciales y un código de doble factor (2FA)",
            "why": "garantizar que solo yo pueda acceder a mis recursos y mantener la seguridad institucional.",
            "priority": "Must Have",
            "sp": "5 SP",
            "hours": "12 h",
            "sprint": "Sprint 1",
            "status": "Completado",
            "ac": [
                "El sistema debe permitir ingresar correo y contraseña, validando los datos contra la base de datos.",
                "Al ingresar credenciales válidas, el sistema debe enviar un código OTP de 6 dígitos al correo electrónico del usuario.",
                "El código OTP debe expirar en 5 minutos.",
                "Si el usuario ingresa el OTP correcto, el sistema otorga un token JWT y permite el acceso.",
                "Si el usuario falla el inicio de sesión 5 veces consecutivas, la cuenta se bloquea (Lockout)."
            ],
            "tasks": [
                ("Implementar endpoint POST /api/auth/login para validación inicial.", "3 h"),
                ("Configurar servicio SMTP y generación de OTP.", "3 h"),
                ("Implementar endpoint POST /api/auth/verify-2fa y generación JWT.", "4 h"),
                ("Configurar política de bloqueo por fuerza bruta (Lockout) en Identity.", "2 h")
            ]
        },
        {
            "id": "HU-02",
            "title": "Cierre de Sesión Seguro",
            "role": "Usuario autenticado del Sistema",
            "want": "cerrar mi sesión de forma segura desde cualquier pantalla del sistema",
            "why": "proteger mi información y evitar que terceros accedan a mi cuenta en caso de que abandone el equipo sin cerrar el navegador.",
            "priority": "Must Have",
            "sp": "3 SP",
            "hours": "6 h",
            "sprint": "Sprint 1",
            "status": "Completado",
            "ac": [
                "El sistema muestra un botón o enlace de 'Cerrar Sesión' visible en la barra de navegación principal para todos los usuarios autenticados.",
                "Al hacer clic en 'Cerrar Sesión', el sistema invalida el token JWT en el servidor y elimina el token del almacenamiento del cliente.",
                "Tras el cierre de sesión, el usuario es redirigido a la página de inicio de sesión.",
                "Si el usuario intenta acceder a una ruta protegida después de cerrar sesión, el sistema lo redirige automáticamente al Login.",
                "El sistema registra el evento de cierre de sesión en el log de auditoría con timestamp.",
                "La sesión se cierra automáticamente si el token JWT expira, redirigiendo al usuario al Login con un mensaje informativo."
            ],
            "tasks": [
                ("Implementación del endpoint POST /api/auth/logout en backend con invalidación de token (blacklist).", "2 h"),
                ("Desarrollo del Guard de autenticación en Angular (AuthGuard) para rutas protegidas.", "2 h"),
                ("Implementación del interceptor HTTP en Angular para adjuntar token y manejar expiración.", "1 h"),
                ("Pruebas funcionales de cierre de sesión y protección de rutas.", "1 h")
            ]
        },
        {
            "id": "HU-03",
            "title": "Recuperación de Contraseña Olvidada",
            "role": "Usuario Final",
            "want": "solicitar un enlace de restablecimiento de contraseña a mi correo electrónico registrado",
            "why": "poder recuperar el acceso a mi cuenta en caso de haber olvidado mi clave sin requerir intervención manual del administrador.",
            "priority": "Should Have",
            "sp": "3 SP",
            "hours": "8 h",
            "sprint": "Sprint 1",
            "status": "Completado",
            "ac": [
                "El sistema permite ingresar un correo electrónico en la pantalla de recuperación.",
                "Si el correo existe, el sistema envía un enlace con un token criptográfico temporal válido por 24 horas.",
                "Al acceder al enlace, el sistema permite ingresar una nueva contraseña.",
                "Si se reutiliza el enlace o el token expiró, el sistema muestra un mensaje de error claro."
            ],
            "tasks": [
                ("Implementar endpoint POST /api/auth/forgot-password.", "2 h"),
                ("Generar y enviar token temporal por correo usando Identity.", "2 h"),
                ("Implementar endpoint POST /api/auth/reset-password.", "2 h"),
                ("Crear interfaz de usuario en Angular para el flujo de recuperación.", "2 h")
            ]
        }
    ],
    "Sprint2": [
        {
            "id": "HU-04",
            "title": "Gestión Administrativa de Usuarios",
            "role": "Administrador",
            "want": "visualizar, crear, editar y desbloquear usuarios desde un panel centralizado",
            "why": "administrar el ciclo de vida de las cuentas del sistema y resolver bloqueos por fuerza bruta.",
            "priority": "Must Have",
            "sp": "5 SP",
            "hours": "14 h",
            "sprint": "Sprint 2",
            "status": "Completado",
            "ac": [
                "El sistema muestra una tabla paginada con todos los usuarios registrados.",
                "El administrador puede editar la información básica y el rol de cualquier usuario.",
                "Si un usuario está bloqueado por múltiples intentos fallidos, el sistema permite al administrador desbloquearlo manualmente.",
                "El sistema debe prevenir que un administrador elimine o bloquee su propia cuenta."
            ],
            "tasks": [
                ("Implementar CRUD de usuarios en backend (UsuariosController).", "5 h"),
                ("Implementar endpoint para desbloqueo manual de cuentas (Lockout).", "2 h"),
                ("Crear componente Angular con tabla paginada e interfaz de edición.", "5 h"),
                ("Integrar modales de confirmación para acciones críticas.", "2 h")
            ]
        },
        {
            "id": "HU-05",
            "title": "Control de Acceso Basado en Roles (RBAC)",
            "role": "Administrador de Seguridad",
            "want": "restringir el acceso a los endpoints y vistas del sistema dependiendo del rol del usuario autenticado",
            "why": "garantizar que roles inferiores (ej. Docente) no consuman recursos administrativos y prevenir escalada de privilegios.",
            "priority": "Must Have",
            "sp": "3 SP",
            "hours": "8 h",
            "sprint": "Sprint 2",
            "status": "Completado",
            "ac": [
                "Un usuario con rol Docente recibe HTTP 403 Forbidden al intentar acceder a rutas de la API del administrador.",
                "Las opciones de menú en el frontend exclusivas para Administradores no se renderizan en el DOM si el usuario no tiene dicho rol.",
                "Los permisos se validan estrictamente mediante los claims del token JWT en el backend."
            ],
            "tasks": [
                ("Decorar endpoints administrativos con [Authorize(Roles='Administrador')].", "2 h"),
                ("Configurar extracción y validación de claims de rol en Angular.", "2 h"),
                ("Crear directiva estructural *hasRole en Angular para el DOM.", "2 h"),
                ("Pruebas de acceso con diferentes roles (Caja Blanca/Negra).", "2 h")
            ]
        },
        {
            "id": "HU-06",
            "title": "Asignación Dinámica de Permisos a Roles",
            "role": "Administrador",
            "want": "poder visualizar y gestionar permisos granulares para cada rol",
            "why": "flexibilizar el modelo de seguridad sin requerir recompilar el código fuente.",
            "priority": "Could Have",
            "sp": "3 SP",
            "hours": "10 h",
            "sprint": "Sprint 2",
            "status": "Completado",
            "ac": [
                "El sistema lista todos los permisos disponibles agrupados por módulo.",
                "El administrador puede asignar o revocar permisos a roles mediante casillas de verificación.",
                "La matriz de permisos se actualiza en tiempo real en la base de datos."
            ],
            "tasks": [
                ("Crear modelo relacional de Roles y Permisos (RoleClaims).", "3 h"),
                ("Implementar endpoints en RolePermissionsController.", "3 h"),
                ("Construir interfaz en Angular para asignación matricial de permisos.", "4 h")
            ]
        }
    ],
    "Sprint3": [
        {
            "id": "HU-07",
            "title": "Registro Automático de Auditoría",
            "role": "Oficial de Seguridad",
            "want": "que el sistema registre automáticamente quién, cuándo y qué acción crítica se ejecutó",
            "why": "mantener una trazabilidad inmutable ante incidentes y cumplir con requerimientos de seguridad.",
            "priority": "Must Have",
            "sp": "5 SP",
            "hours": "12 h",
            "sprint": "Sprint 3",
            "status": "Completado",
            "ac": [
                "Cualquier cambio de estado en la base de datos (Insert, Update, Delete) debe registrar un evento de auditoría.",
                "El registro debe incluir Timestamp, ID del Usuario, Acción y Detalles del cambio.",
                "Las excepciones no controladas en el servidor se deben guardar en un log centralizado."
            ],
            "tasks": [
                ("Sobrescribir el método SaveChangesAsync en Entity Framework para capturar cambios.", "4 h"),
                ("Implementar middleware global para logs de excepciones.", "3 h"),
                ("Crear entidad AuditLog y migrar a base de datos.", "2 h"),
                ("Implementar endpoint para consulta de logs por administradores.", "3 h")
            ]
        }
    ],
    "Sprint4": [
        {
            "id": "HU-08",
            "title": "Dashboard de Métricas del Sistema",
            "role": "Administrador",
            "want": "visualizar un resumen estadístico de la distribución de usuarios y estados de conexión",
            "why": "monitorear rápidamente el uso general del módulo y detectar anomalías o bloqueos.",
            "priority": "Should Have",
            "sp": "3 SP",
            "hours": "8 h",
            "sprint": "Sprint 4",
            "status": "Completado",
            "ac": [
                "La pantalla principal debe mostrar el total de usuarios registrados.",
                "Se debe mostrar gráficamente la cantidad de usuarios bloqueados vs activos.",
                "La información debe cargarse en menos de 500ms mediante agregaciones optimizadas."
            ],
            "tasks": [
                ("Crear endpoint agrupador /api/stats/dashboard en el backend.", "3 h"),
                ("Implementar consultas optimizadas usando COUNT y GROUP BY en EF Core.", "1 h"),
                ("Diseñar componentes de visualización tipo tarjeta en Angular.", "4 h")
            ]
        }
    ]
}

def generate_html_content(story):
    # Professional B&W theme
    header_bg = "#000000"
    sub_header_bg = "#333333"
    section_bg = "#1a1a1a"
    text_light = "#ffffff"
    border_color = "#000000"
    
    # Priority representation in a B&W theme (bold/underline instead of red/green colors)
    priority_style = "font-weight: bold; text-decoration: underline;" if story['priority'] == "Must Have" else "font-weight: bold;"
    
    html = f"""<table style="width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; border: 2px solid {border_color};">
    <tr style="background-color: {header_bg}; color: {text_light};">
        <th colspan="6" style="text-align: center; padding: 12px; font-size: 18px; border: 1px solid {border_color};">{story['id']} — {story['title']}</th>
    </tr>
    <tr style="background-color: {sub_header_bg}; color: {text_light}; text-align: center; font-weight: bold;">
        <td style="padding: 10px; border: 1px solid {border_color}; width: 20%;">Rol</td>
        <td style="padding: 10px; border: 1px solid {border_color}; width: 40%;">Acción</td>
        <td style="padding: 10px; border: 1px solid {border_color}; width: 10%;">Prioridad</td>
        <td style="padding: 10px; border: 1px solid {border_color}; width: 10%;">Story Pts</td>
        <td style="padding: 10px; border: 1px solid {border_color}; width: 10%;">Horas Est.</td>
        <td style="padding: 10px; border: 1px solid {border_color}; width: 10%;">Sprint</td>
    </tr>
    <tr style="background-color: #ffffff; text-align: center; color: #000000;">
        <td style="padding: 10px; border: 1px solid {border_color};">{story['role']}</td>
        <td style="padding: 10px; border: 1px solid {border_color}; text-align: left;">{story['want']}</td>
        <td style="padding: 10px; border: 1px solid {border_color}; {priority_style}">{story['priority']}</td>
        <td style="padding: 10px; border: 1px solid {border_color};">{story['sp']}</td>
        <td style="padding: 10px; border: 1px solid {border_color};">{story['hours']}</td>
        <td style="padding: 10px; border: 1px solid {border_color}; font-weight: bold;">{story['sprint']}</td>
    </tr>
    <tr style="background-color: #f2f2f2; color: #000000;">
        <td style="padding: 10px; border: 1px solid {border_color}; font-weight: bold;">Beneficio / Para:</td>
        <td colspan="5" style="padding: 10px; border: 1px solid {border_color};">{story['why']}</td>
    </tr>
    <tr style="background-color: #ffffff; color: #000000;">
        <td style="padding: 10px; border: 1px solid {border_color}; font-weight: bold;">Estado:</td>
        <td colspan="5" style="padding: 10px; border: 1px solid {border_color}; font-weight: bold; font-style: italic;">{story['status']}</td>
    </tr>
    <tr style="background-color: {section_bg}; color: {text_light};">
        <th colspan="6" style="padding: 10px; border: 1px solid {border_color}; text-align: left;">Criterios de Aceptación</th>
    </tr>
"""
    
    # Criterios de aceptación
    for idx, ac in enumerate(story['ac'], start=1):
        bg_color = "#ffffff" if idx % 2 != 0 else "#f9f9f9"
        html += f"""    <tr style="background-color: {bg_color}; color: #000000;">
        <td style="text-align: center; font-weight: bold; border: 1px solid {border_color}; padding: 10px;">CA-{idx:02d}</td>
        <td colspan="5" style="padding: 10px; border: 1px solid {border_color};">{ac}</td>
    </tr>
"""
    
    html += f"""    <tr style="background-color: {section_bg}; color: {text_light};">
        <th colspan="6" style="padding: 10px; border: 1px solid {border_color}; text-align: left;">Tareas Técnicas de Implementación</th>
    </tr>
"""
    
    # Tareas Técnicas
    for idx, (task, hrs) in enumerate(story['tasks'], start=1):
        bg_color = "#ffffff" if idx % 2 != 0 else "#f9f9f9"
        html += f"""    <tr style="background-color: {bg_color}; color: #000000;">
        <td style="text-align: center; font-weight: bold; border: 1px solid {border_color}; padding: 10px;">T-{idx:02d}</td>
        <td colspan="4" style="padding: 10px; border: 1px solid {border_color};">{task}</td>
        <td style="text-align: center; font-weight: bold; border: 1px solid {border_color}; padding: 10px;">{hrs}</td>
    </tr>
"""

    html += "</table>\n"
    return html

# Generar archivos
for sprint, story_list in stories.items():
    sprint_dir = os.path.join(base_dir, sprint)
    if not os.path.exists(sprint_dir):
        os.makedirs(sprint_dir)
    
    for story in story_list:
        file_path = os.path.join(sprint_dir, f"{story['id']}.md")
        with open(file_path, "w", encoding="utf-8") as f:
            f.write(generate_html_content(story))
        print(f"Generado con formato tabla: {file_path}")

print("Generación de Historias de Usuario (Formato Tabla) completada exitosamente.")
