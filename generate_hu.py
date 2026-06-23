import os

base_dir = r"C:\Users\pasante\PasanteKM\OneDrive - WellPerf\Escritorio\TESIS-EPN\Web-TIC\HU"

stories = {
    "Sprint1": [
        {
            "id": "HU-01",
            "title": "Autenticación Segura con JWT y 2FA",
            "role": "Usuario Final",
            "want": "iniciar sesión en el sistema mediante mis credenciales y un código de doble factor (2FA)",
            "why": "para garantizar que solo yo pueda acceder a mis recursos y mantener la seguridad institucional",
            "ac": [
                "**Dado que** ingreso credenciales válidas, **cuando** el sistema las procesa, **entonces** me envía un código OTP de 6 dígitos a mi correo electrónico.",
                "**Dado que** recibo el código OTP, **cuando** lo ingreso correctamente en menos de 5 minutos, **entonces** se me otorga un token JWT y accedo al sistema.",
                "**Dado que** soy un atacante, **cuando** fallo el inicio de sesión 5 veces consecutivas, **entonces** el sistema bloquea temporalmente la cuenta (Lockout) por motivos de fuerza bruta."
            ],
            "tasks": [
                "Implementar endpoint `POST /api/auth/login`.",
                "Configurar servicio de envío de correos SMTP.",
                "Implementar endpoint `POST /api/auth/verify-2fa` y generar token JWT.",
                "Configurar política de bloqueo por fuerza bruta en ASP.NET Core Identity."
            ]
        },
        {
            "id": "HU-02",
            "title": "Recuperación de Contraseña Olvidada",
            "role": "Usuario Final",
            "want": "solicitar un enlace de restablecimiento de contraseña a mi correo electrónico registrado",
            "why": "para poder recuperar el acceso a mi cuenta en caso de haber olvidado mi clave sin requerir intervención manual del administrador",
            "ac": [
                "**Dado que** olvido mi contraseña, **cuando** ingreso mi correo en la pantalla de recuperación, **entonces** el sistema me envía un enlace con un token de un solo uso válido por 24 horas.",
                "**Dado que** accedo al enlace recibido, **cuando** establezco mi nueva contraseña, **entonces** el sistema la encripta y actualiza exitosamente.",
                "**Dado que** intento reutilizar un enlace de recuperación, **cuando** ingreso a él, **entonces** el sistema lo rechaza indicando que ya fue utilizado o expiró."
            ],
            "tasks": [
                "Implementar endpoint `POST /api/auth/forgot-password`.",
                "Generar token criptográfico temporal en Identity.",
                "Implementar endpoint `POST /api/auth/reset-password`.",
                "Crear interfaz de usuario en Angular para el flujo de recuperación."
            ]
        },
        {
            "id": "HU-03",
            "title": "Cierre de Sesión Seguro (Blacklist)",
            "role": "Usuario Final",
            "want": "cerrar mi sesión activa explícitamente",
            "why": "para invalidar mi token actual y evitar que terceros puedan acceder al sistema si dejo mi sesión abierta",
            "ac": [
                "**Dado que** tengo una sesión activa, **cuando** presiono el botón de cerrar sesión, **entonces** mi token JWT se envía a una lista negra (Blacklist) en el backend.",
                "**Dado que** mi token fue invalidado, **cuando** intento acceder a cualquier ruta protegida con él, **entonces** el backend devuelve un error HTTP 401 Unauthorized."
            ],
            "tasks": [
                "Implementar mecanismo de Blacklist usando Redis o base de datos en memoria.",
                "Implementar endpoint `POST /api/auth/logout`.",
                "Crear interceptor en Angular para purgar el sessionStorage y redirigir al Login."
            ]
        }
    ],
    "Sprint2": [
        {
            "id": "HU-04",
            "title": "Gestión Administrativa de Usuarios",
            "role": "Administrador",
            "want": "visualizar, crear, editar y desbloquear usuarios desde un panel centralizado",
            "why": "para administrar el ciclo de vida de las cuentas del sistema y resolver bloqueos por fuerza bruta",
            "ac": [
                "**Dado que** soy administrador, **cuando** accedo al panel de usuarios, **entonces** visualizo una tabla paginada con todos los registros.",
                "**Dado que** un usuario está bloqueado (Lockout), **cuando** presiono la opción de desbloquear, **entonces** el sistema restablece sus intentos fallidos y reactiva la cuenta."
            ],
            "tasks": [
                "Implementar CRUD de usuarios en el backend (`UsuariosController`).",
                "Implementar endpoint específico para desbloqueo manual.",
                "Crear vista en Angular para listar y editar usuarios."
            ]
        },
        {
            "id": "HU-05",
            "title": "Control de Acceso Basado en Roles (RBAC)",
            "role": "Administrador de Seguridad",
            "want": "restringir el acceso a los endpoints y vistas del sistema dependiendo del rol del usuario autenticado",
            "why": "para garantizar que un Docente o Estudiante no pueda consumir recursos administrativos, previniendo la escalada de privilegios",
            "ac": [
                "**Dado que** soy un Docente, **cuando** intento acceder al panel de administración, **entonces** el backend rechaza la petición con HTTP 403 Forbidden.",
                "**Dado que** soy un Docente, **cuando** navego en el frontend, **entonces** las opciones del menú exclusivas para Administradores se ocultan dinámicamente."
            ],
            "tasks": [
                "Decorar endpoints críticos con `[Authorize(Roles='Administrador')]`.",
                "Extraer el claim de rol del token JWT en Angular.",
                "Crear la directiva `hasRole` en Angular para ocultar elementos del DOM."
            ]
        },
        {
            "id": "HU-06",
            "title": "Asignación Dinámica de Permisos a Roles",
            "role": "Administrador",
            "want": "poder gestionar y asignar permisos granulares a los roles existentes",
            "why": "para flexibilizar el modelo de seguridad sin tener que recompilar la aplicación",
            "ac": [
                "**Dado que** soy administrador, **cuando** accedo a la gestión de roles, **entonces** puedo ver los permisos atados a cada rol.",
                "**Dado que** modifico los permisos de un rol, **cuando** guardo los cambios, **entonces** la matriz de accesos se actualiza en la base de datos."
            ],
            "tasks": [
                "Crear modelo de datos relacional para Roles y Permisos.",
                "Implementar endpoints en `RolePermissionsController`.",
                "Construir interfaz en Angular para asignación visual de permisos mediante checkboxes."
            ]
        }
    ],
    "Sprint3": [
        {
            "id": "HU-07",
            "title": "Registro Automático de Auditoría",
            "role": "Oficial de Seguridad",
            "want": "que el sistema registre automáticamente quién, cuándo y qué acción administrativa se ejecutó",
            "why": "para mantener una trazabilidad inmutable ante incidentes y cumplir con las normativas institucionales",
            "ac": [
                "**Dado que** un Administrador edita un usuario, **cuando** se guarda el cambio, **entonces** se genera un registro en la tabla de Logs con el ID del administrador, la acción y la fecha.",
                "**Dado que** ocurre una excepción no controlada en el backend, **cuando** el middleware la intercepta, **entonces** el error detallado se guarda en el log."
            ],
            "tasks": [
                "Implementar un middleware global de captura de logs y excepciones.",
                "Crear entidad y tabla de Auditoría en Entity Framework Core.",
                "Crear endpoint de solo-lectura para consultar los logs generados."
            ]
        }
    ],
    "Sprint4": [
        {
            "id": "HU-08",
            "title": "Dashboard de Métricas del Sistema",
            "role": "Administrador",
            "want": "visualizar un resumen estadístico de la distribución de usuarios y sus estados de conexión",
            "why": "para monitorear rápidamente el uso general del módulo y la salud del sistema",
            "ac": [
                "**Dado que** accedo a la página principal del panel, **cuando** la vista carga, **entonces** se renderizan tarjetas con KPIs (total usuarios, bloqueados, inactivos).",
                "**Dado que** los datos cambian en la base de datos, **cuando** refresco la vista, **entonces** las gráficas y métricas se actualizan de manera performante."
            ],
            "tasks": [
                "Crear endpoint `/api/stats/dashboard` en el backend para agrupar métricas.",
                "Diseñar el Dashboard en Angular usando componentes de tarjetas visuales.",
                "Asegurar que la consulta estadística sea optimizada en EF Core."
            ]
        }
    ]
}

def create_markdown_content(story):
    content = f"# {story['id']}: {story['title']}\n\n"
    content += "## 1. Descripción\n"
    content += f"- **Como** {story['role']}\n"
    content += f"- **Quiero** {story['want']}\n"
    content += f"- **Para** {story['why']}\n\n"
    
    content += "## 2. Criterios de Aceptación\n"
    for i, ac in enumerate(story['ac'], 1):
        content += f"{i}. {ac}\n"
    content += "\n"
    
    content += "## 3. Tareas Técnicas\n"
    for task in story['tasks']:
        content += f"- [ ] {task}\n"
    content += "\n"
    
    content += "## 4. Notas Adicionales\n"
    content += "- Revisar que la implementación cumpla con las normativas OWASP Top 10 aplicables para esta sección.\n"
    
    return content

# Generar archivos
for sprint, story_list in stories.items():
    sprint_dir = os.path.join(base_dir, sprint)
    if not os.path.exists(sprint_dir):
        os.makedirs(sprint_dir)
    
    for story in story_list:
        file_path = os.path.join(sprint_dir, f"{story['id']}.md")
        with open(file_path, "w", encoding="utf-8") as f:
            f.write(create_markdown_content(story))
        print(f"Generado: {file_path}")

print("Generación de Historias de Usuario completada exitosamente.")
