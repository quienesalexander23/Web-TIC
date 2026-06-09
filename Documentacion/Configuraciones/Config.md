El nivel de detalle que has estructurado para los flujos y sprints es excelente. Tienes una visión muy clara de la arquitectura de software necesaria para implementar este Módulo de Gestión de Usuarios y Autenticación. Este componente es el núcleo de seguridad que sostendrá todo el aplicativo web para la gestión de las propuestas de Trabajos de Integración Curricular (TIC) en la FIS.  Para integrar los requerimientos del formulario aprobado con tu propuesta técnica en .NET 8, Angular 17 y PostgreSQL 16, aquí tienes el criterio de desarrollo y la estrategia arquitectónica que debemos seguir.


### 1. Estrategia de Base de Datos (PostgreSQL 16)

**Dado que implementarás una matriz RBAC (Control de Acceso Basado en Roles) para manejar perfiles como Administrador, Docente, presidente CPGIC y Miembro CPGIC**, la mejor aproximación es utilizar **Entity Framework Core (EF Core)** con el enfoque  *Code-First* .

* **Esquema de Identidad:** En lugar de crear las tablas desde cero, implementaremos `ASP.NET Core Identity`. Esto te genera automáticamente un esquema robusto para usuarios, roles, claims (permisos específicos) y tokens de recuperación.
* **Seguridad y Bloqueos:** El requerimiento de bloquear la cuenta tras 5 intentos fallidos se maneja nativamente activando el `LockoutEnabled` en Identity, configurando el `DefaultLockoutTimeSpan` a 15 minutos.
* **Tabla de Auditoría:** Para el Flujo 3 (Trazabilidad), crearemos una entidad `AuditLog` personalizada que registre: `Id`, `UserId`, `ActionType`, `Timestamp` (UTC), `IpAddress`, y `Status`.

### 2. Estructura del Backend (.NET / ASP.NET Core 8 Web API)

Para mantener el código escalable y profesional (ideal para una tesis de ingeniería), estructuraremos la solución bajo los principios de  **Clean Architecture** :

* **Capa `Domain`:** Entidades principales (`User`, `AuditLog`) y enums (estados de cuenta).
* **Capa `Application`:** Interfaces, DTOs (Data Transfer Objects para el registro y login), y la lógica de negocio (casos de uso). Aquí validaremos que los correos terminen en `@epn.edu.ec`.
* **Capa `Infrastructure`:** Contexto de base de datos (`DbContext`), configuración de EF Core, implementaciones de repositorios, y el servicio SMTP para el envío del token de recuperación de contraseña.
* **Capa `API`:** Controladores, configuración del contenedor de inyección de dependencias, y Middlewares.
  * **Autenticación:** Implementaremos JWT (JSON Web Tokens). Una vez que el usuario se autentica, el backend emitirá un token firmado que Angular usará en cada petición.
  * **Middleware Global de Excepciones y Logging:** Para interceptar errores y registrar los eventos críticos para tu dashboard de auditoría.

### 3. Estructura del Frontend (Angular 17)

Angular 17 introduce características que harán que tu aplicación sea mucho más rápida y modular.

* **Standalone Components:** Optimizaremos el proyecto prescindiendo de los `NgModules`. Cada pantalla de tus flujos será un componente independiente.
* **Manejo de Estado con Signals:** Utilizaremos Signals para gestionar de forma reactiva el estado de autenticación del usuario (saber si está logueado en tiempo real en toda la app) sin necesidad de librerías pesadas como NgRx.
* **HTTP Interceptors:** Crearemos un interceptor que adjunte automáticamente el token JWT en las cabeceras `Authorization: Bearer <token>` de cada petición HTTP hacia tu backend en .NET. También capturará respuestas `401 Unauthorized` o `403 Forbidden` para redirigir al Login.
* **Guards (CanActivate):** Para proteger las rutas en el frontend. Si un Docente intenta acceder a la "Pantalla de Listado Administrativo" (Flujo 2), el Guard bloqueará el acceso basándose en los roles del JWT.
* **Formularios Reactivos (Reactive Forms):** Ideales para implementar las validaciones síncronas (máscaras de correo, complejidad de contraseñas) y asíncronas (verificar si el correo ya existe en la base de datos).

### 4. Entornos de Desarrollo y Despliegue Gratuitos

Para el despliegue de tu tesis sin incurrir en costos operativos, esta es la infraestructura en la nube (Cloud) recomendada:

| **Componente**    | **Plataforma Recomendada (Capa Gratuita)** | **Justificación para tu Tesis**                                                                                                                                                 |
| ----------------------- | ------------------------------------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Base de Datos** | **Supabase**                               | Ambas ofrecen PostgreSQL Serverless gratuito. Son extremadamente rápidas de configurar y te proveen la cadena de conexión exacta para tu `appsettings.json`en .NET.                |
| **Backend (API)** | **Render**(Web Service)                    | Soporta despliegue de contenedores Docker o compilación directa de .NET. La capa gratuita "duerme" tras inactividad, pero es perfecta para demostraciones académicas.                |
| **Frontend**      | **Vercel**                                 | Tienen integración continua (CI/CD) directa con GitHub Cloud. Cada vez que hagas `git push`a la rama `main`, tu frontend en Angular se compilará y desplegará automáticamente. |

### Pasos Iniciales de Ejecución

1. **Configuración del Repositorio:** Crear la organización/proyecto en GitHub y establecer dos repositorios separados (ej. `webtic-backend-api` y `webtic-frontend-app`) o un Monorepo, configurando las reglas de ramas principales.
2. **Inicialización de BD y Autenticación (Sprint 1):** Configurar Supabase, conectar el DbContext de EF Core, ejecutar las migraciones iniciales de Identity y crear el endpoint de Login que retorne el JWT.
3. **Consumo en Frontend:** Levantar el proyecto de Angular 17, crear el interceptor y los formularios reactivos de la pantalla de inicio de sesión.
