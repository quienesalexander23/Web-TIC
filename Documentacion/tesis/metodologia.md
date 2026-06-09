% =========================================================================
% CAPÍTULO 2: METODOLOGÍA
% =========================================================================
% Proyecto : Módulo de Gestión de Usuarios y Autenticación
% Sistema  : WebTIC FIS — Escuela Politécnica Nacional
% Autor    : Alexander Francisco Tibanta Miranda
% Director : PhD. Victor Vicente Velepucha Bonett
% Período  : 01 de abril de 2026 — 03 de julio de 2026
% =========================================================================

\chapter{Metodología}
\label{chap:metodologia}

% -------------------------------------------------------------------------
% 2.1  JUSTIFICACIÓN DE LA METODOLOGÍA
% -------------------------------------------------------------------------
\section{Justificación de la Metodología}
\label{sec:justificacion_metodologia}

El proceso de diseño e implementación de un módulo de seguridad para un sistema institucional exige un enfoque de desarrollo que combine disciplina técnica con la capacidad de adaptarse a los cambios en los requerimientos que emergen de manera natural durante la interacción con los usuarios finales. En el contexto del presente Trabajo de Integración Curricular, la Facultad de Ingeniería de Sistemas de la Escuela Politécnica Nacional requiere que el sistema resultante sea no solamente funcional en términos de autenticación y control de acceso, sino también validado de manera continua por el director del proyecto y por los actores institucionales que lo utilizarán en su operación diaria. Esta realidad hace que los modelos de desarrollo predictivos y secuenciales resulten inadecuados, puesto que en dichos modelos la retroalimentación se recibe únicamente al finalizar el ciclo completo de construcción, lo que incrementa significativamente el riesgo de producir un sistema que no satisfaga las necesidades reales de la organización.

Frente a esta problemática, se ha seleccionado el marco de trabajo ágil \textbf{Scrum} como metodología rectora del proceso de desarrollo. Scrum se define como un marco de trabajo liviano que permite a los equipos generar valor mediante la entrega incremental de incrementos funcionales de software en periodos cortos de tiempo denominados \textit{sprints} \cite{schwaber2020}. Su adopción en este proyecto se fundamenta en tres razones principales. En primer lugar, la naturaleza iterativa de Scrum permite que cada funcionalidad crítica del módulo —como el inicio de sesión seguro con JWT o la gestión de roles bajo el modelo RBAC— sea implementada, probada y validada de forma independiente antes de proceder con las funcionalidades subsiguientes, reduciendo la propagación de errores de diseño entre capas del sistema. En segundo lugar, las ceremonias estructuradas de Scrum, particularmente el Sprint Review, proveen un mecanismo formal y recurrente para que el Product Owner evalúe el progreso del módulo y reoriente las prioridades del backlog si así lo considera pertinente. En tercer lugar, la transparencia que promueve el marco de trabajo a través de sus artefactos —el Product Backlog, el Sprint Backlog y el Incremento— facilita la trazabilidad académica del proyecto, permitiendo evidenciar con claridad cada decisión de diseño y cada hora de trabajo invertida en el desarrollo del sistema.

La aplicación de Scrum en este proyecto no se circunscribe exclusivamente a la fase de codificación. Por el contrario, el marco de trabajo ha sido adoptado de manera integral desde la fase de análisis de requerimientos y el diseño de la arquitectura de software, pasando por el prototipado de interfaces de usuario, la implementación de las capas frontend y backend, hasta la ejecución de pruebas funcionales de seguridad y las evaluaciones de usabilidad con usuarios finales. Esta visión holística del marco de trabajo garantiza que cada entregable producido a lo largo del proyecto sea coherente con el objetivo académico e institucional del Trabajo de Integración Curricular.

% -------------------------------------------------------------------------
% 2.2  APLICACIÓN DE LA METODOLOGÍA SCRUM
% -------------------------------------------------------------------------
\section{Aplicación de la Metodología Scrum}
\label{sec:aplicacion_scrum}

El desarrollo integral del Módulo de Gestión de Usuarios y Autenticación se ha planificado para completarse en un periodo de cuatro meses calendario, comprendido entre el día primero de abril de dos mil veintiséis y el viernes tres de julio de dos mil veintiséis. Durante este horizonte temporal, el módulo acumula una carga horaria total de doscientas ochenta y ocho horas efectivas de trabajo, cifra que engloba la totalidad de las actividades contempladas en el alcance del componente: la revisión de literatura y construcción del marco teórico, las entrevistas con las partes interesadas para la elicitación de requerimientos, el diseño de prototipos de pantalla en alta fidelidad, el diseño de la arquitectura de software, la planificación de los sprints, el desarrollo de las funcionalidades de frontend y backend, la implementación de la base de datos relacional, la ejecución de pruebas funcionales y, finalmente, la realización de pruebas de usabilidad con usuarios finales. Esta distribución de horas responde directamente a la estructura de actividades definida en el formulario de propuesta del Trabajo de Integración Curricular aprobado por la Comisión Permanente de Gestión de Integración Curricular (CPGIC).

La distribución del tiempo a lo largo del periodo se organiza en cinco sprints, cuatro de los cuales corresponden a ciclos de desarrollo iterativo de dos semanas de duración cada uno, y uno inicial denominado Sprint 0 que actúa como fase fundacional del proyecto. Esta estructuración permite mantener un ritmo de trabajo constante y predecible, con entregas funcionales verificables al finalizar cada ciclo, facilitando al mismo tiempo la gestión académica del proyecto y la supervisión continua por parte del director.

\subsection{Roles del Equipo Scrum}
\label{subsec:roles_scrum}

La correcta aplicación del marco de trabajo Scrum requiere la definición explícita de los roles que cada participante desempeña dentro del proyecto, dado que dichos roles establecen con claridad las responsabilidades, las líneas de comunicación y los mecanismos de toma de decisiones que gobiernan el proceso. Para el presente Trabajo de Integración Curricular, los roles han sido asignados considerando tanto la estructura académica del proyecto como las características del equipo de trabajo disponible.

El rol de Product Owner ha sido asignado al PhD. Victor Vicente Velepucha Bonett, director del presente Trabajo de Integración Curricular. En su calidad de Product Owner, el director es el responsable de definir y comunicar el objetivo del producto, gestionar y priorizar el Product Backlog, y garantizar que el trabajo del equipo de desarrollo esté alineado con las necesidades institucionales de la Facultad de Ingeniería de Sistemas y con los estándares académicos de la Escuela Politécnica Nacional. Su presencia en las ceremonias de Sprint Review es fundamental para validar que cada incremento entregado cumpla con los criterios de aceptación establecidos. El rol de Scrum Master y Developer ha sido asumido por el propio estudiante, Alexander Francisco Tibanta Miranda. Esta asignación dual, que es habitual en proyectos de desarrollo individuales de alcance académico, implica que el estudiante es simultáneamente responsable de facilitar la correcta ejecución de las ceremonias Scrum, de identificar y eliminar los impedimentos que puedan obstaculizar el progreso del trabajo, y de diseñar, implementar y probar la totalidad del código fuente del módulo. Los usuarios finales del sistema, conformados por los docentes de la Facultad de Ingeniería de Sistemas y los miembros de la CPGIC, participan en calidad de stakeholders, proveyendo los requerimientos operativos durante las sesiones de entrevista y validando la usabilidad del sistema durante las pruebas no funcionales planificadas para los sprints finales.

La distribución de roles, las personas asignadas y sus responsabilidades principales se presentan de manera estructurada en la Tabla \ref{tab:roles_scrum}.

\begin{table}[H]
\centering
\caption{Matriz de roles Scrum y responsabilidades del proyecto}
\label{tab:roles_scrum}
\begin{tabular}{|p{3.5cm}|p{4cm}|p{6.5cm}|}
\hline
\textbf{Rol Scrum} & \textbf{Persona Asignada} & \textbf{Responsabilidad Principal} \\ \hline
Product Owner & PhD. Victor Vicente Velepucha Bonett & Definición del objetivo del producto, priorización del Product Backlog y validación académica de los incrementos en cada Sprint Review. \\ \hline
Scrum Master / Developer & Alexander Francisco Tibanta Miranda & Facilitación de ceremonias Scrum, eliminación de impedimentos, diseño de la arquitectura de seguridad y desarrollo integral del código fuente del módulo. \\ \hline
Stakeholders & Docentes FIS y Miembros CPGIC & Provisión de requerimientos operativos durante las entrevistas de análisis y validación de la experiencia de usuario durante las pruebas de usabilidad. \\ \hline
\end{tabular}
\end{table}

\subsection{Ceremonias Scrum}
\label{subsec:ceremonias_scrum}

Las ceremonias de Scrum constituyen los eventos formales mediante los cuales el equipo inspecciona y adapta su trabajo de manera continua. Su aplicación en el presente proyecto ha sido planificada para respetar las duraciones máximas recomendadas por la Guía Scrum \cite{schwaber2020}, adaptándolas al contexto de un equipo unipersonal de desarrollo. El Sprint Planning se ejecuta al inicio de cada sprint y tiene como propósito definir el Sprint Goal, seleccionar los ítems del Product Backlog que se abordarán durante el ciclo y descomponer cada historia de usuario en tareas técnicas concretas con estimaciones de tiempo. Este evento tiene una duración planificada de dos horas para sprints de dos semanas. El Daily Scrum se realiza de forma personal cada día hábil de trabajo, con una duración máxima de quince minutos, y sirve para que el estudiante revise el progreso del día anterior, planifique las actividades del día en curso e identifique cualquier impedimento que requiera escalamiento al director del proyecto. El Sprint Review se lleva a cabo al finalizar cada sprint, con una duración de una hora, y representa la instancia formal en la que el incremento de software producido es demostrado al Product Owner, quien evalúa su conformidad con los criterios de aceptación y determina si el ítem puede marcarse como completado. La Sprint Retrospective, con una duración de una hora, cierra el ciclo de cada sprint y brinda al equipo la oportunidad de reflexionar sobre el proceso seguido, identificar las prácticas que resultaron efectivas y aquellas que deben mejorarse, y formular acciones concretas de mejora para el siguiente sprint. Finalmente, el Backlog Refinement se realiza a mitad de cada sprint, con una duración de una hora, y tiene como objetivo revisar y estimar los ítems del Product Backlog que se abordarán en el sprint siguiente, garantizando que el Sprint Planning pueda ejecutarse de manera fluida y sin ambigüedades.

\subsection{Artefactos Scrum}
\label{subsec:artefactos_scrum}

El marco de trabajo Scrum define tres artefactos fundamentales cuya gestión adecuada es condición necesaria para la transparencia del proceso y la calidad del producto resultante. El primero de ellos es el Product Backlog, que en el contexto de este proyecto constituye la lista completa, ordenada y priorizada de todas las funcionalidades, mejoras y correcciones que se desea incorporar en el Módulo de Gestión de Usuarios y Autenticación. Este artefacto es dinámico por naturaleza: es creado y mantenido por el Product Owner con la colaboración del desarrollador, y evoluciona a lo largo del proyecto a medida que se obtiene nueva información sobre los requerimientos del sistema. Para el presente módulo, el Product Backlog se ha construido a partir de las entrevistas realizadas con docentes y miembros de la CPGIC, del análisis del formulario F\_AA\_233A y de las restricciones técnicas impuestas por la infraestructura institucional de la Escuela Politécnica Nacional. El segundo artefacto es el Sprint Backlog, que representa el subconjunto de ítems del Product Backlog seleccionados durante el Sprint Planning para ser implementados en el ciclo en curso, junto con el plan detallado de cómo cada tarea técnica será ejecutada. El Sprint Backlog pertenece exclusivamente al desarrollador y puede ser ajustado durante el sprint a medida que se adquiere mayor comprensión del trabajo necesario para alcanzar el Sprint Goal, siempre que dichos ajustes no comprometan el objetivo comprometido. El tercer artefacto es el Incremento, que representa la suma de todos los ítems del Product Backlog completados hasta el final de un sprint determinado, incluyendo todos los incrementos de sprints anteriores. Para que un ítem pueda formar parte de un incremento, debe satisfacer íntegramente la Definición de Hecho establecida para el proyecto, lo que garantiza que el software entregado sea funcional, probado y potencialmente desplegable en el entorno de producción institucional.

\subsection{Definición de Hecho}
\label{subsec:definition_of_done}

La Definición de Hecho (\textit{Definition of Done}, DoD) constituye el acuerdo formal entre el desarrollador y el Product Owner respecto a los criterios mínimos que debe cumplir cualquier historia de usuario para poder ser declarada como completada e integrada al incremento del sprint. Para el Módulo de Gestión de Usuarios y Autenticación, la DoD ha sido establecida de la siguiente manera: el código fuente de la funcionalidad ha sido implementado respetando los estándares de codificación definidos para el proyecto (convenciones de nomenclatura de C\# para el backend y de Angular para el frontend) y ha sido commiteado en la rama correspondiente del repositorio de GitHub siguiendo el flujo de trabajo Git Flow; las pruebas unitarias de la lógica de negocio han sido escritas y ejecutadas satisfactoriamente, con una cobertura mínima del ochenta por ciento sobre los servicios y controladores del backend; las pruebas funcionales de caja negra definidas en los criterios de aceptación de la historia de usuario han sido ejecutadas y han producido los resultados esperados; la funcionalidad ha sido desplegada exitosamente en el entorno de desarrollo local y su correcto funcionamiento ha sido verificado de manera integrada con los demás componentes del sistema; la documentación mínima requerida —incluyendo los comentarios de código en los métodos críticos de seguridad y la actualización del documento Swagger correspondiente al endpoint implementado— ha sido redactada; y, finalmente, el Product Owner ha revisado y aceptado el incremento durante la ceremonia de Sprint Review. El cumplimiento simultáneo de la totalidad de estos criterios es condición necesaria y suficiente para que una historia de usuario sea considerada terminada.

% -------------------------------------------------------------------------
% 2.3  STACK TECNOLÓGICO Y HERRAMIENTAS
% -------------------------------------------------------------------------
\section{Stack Tecnológico y Herramientas de Desarrollo}
\label{sec:stack_tecnologico}

La selección del conjunto de tecnologías y herramientas que soportan el desarrollo del módulo constituye una decisión de diseño estratégica con impacto directo sobre la calidad, la mantenibilidad y la seguridad del sistema resultante. En el contexto de un módulo de autenticación y control de acceso para un sistema institucional, la elección debe garantizar no solamente la eficiencia en la fase de construcción, sino también la compatibilidad con los estándares tecnológicos de la industria del software empresarial, la disponibilidad de mecanismos nativos de seguridad probados, y la factibilidad de mantenimiento a largo plazo por parte del personal técnico de la Facultad de Ingeniería de Sistemas.

Para la capa de presentación, se ha seleccionado \textbf{Angular} en su versión diecisiete, un framework de desarrollo de aplicaciones de página única (\textit{Single Page Application}, SPA) liderado por Google y ampliamente adoptado en el desarrollo de aplicaciones empresariales \cite{angular2023}. Angular se distingue por su arquitectura basada en componentes, su sistema de inyección de dependencias, su integración nativa con TypeScript y su robusto conjunto de herramientas para la implementación de guardas de ruta (\textit{Route Guards}) e interceptores HTTP, características que resultan esenciales para la construcción de una interfaz de usuario que respete la lógica de control de acceso basado en roles definida en el backend. Para la capa de lógica de negocio y exposición de la API, se ha seleccionado \textbf{.NET 8} con C\# como lenguaje de programación \cite{microsoft2023dotnet}. Este framework de código abierto y de alto rendimiento, desarrollado por Microsoft, ofrece un ecosistema maduro para la construcción de APIs RESTful seguras y escalables, con soporte nativo para los estándares de autenticación modernos como OAuth 2.0 y JWT (\textit{JSON Web Token}), y con mecanismos integrados de autorización basada en políticas y roles que se alinean directamente con los requerimientos del modelo RBAC a implementar. Para la capa de persistencia de datos, se ha seleccionado \textbf{SQL Server} como motor de base de datos relacional, gestionado a través de SQL Server Management Studio (SSMS). La elección de un motor relacional responde a la naturaleza estructurada y altamente relacional de los datos del módulo —usuarios, roles, permisos, sesiones activas y registros de auditoría—, que exigen la garantía de integridad transaccional provista por el modelo ACID. La integración nativa de SQL Server con el ecosistema .NET a través de Entity Framework Core simplifica además la implementación de la capa de acceso a datos. Para el control de versiones del código fuente, se utiliza \textbf{Git} alojado en \textbf{GitHub}, siguiendo el flujo de trabajo Git Flow, que establece ramas dedicadas para el desarrollo de cada historia de usuario (\texttt{feature/}), la preparación de cada release (\texttt{release/}) y la corrección urgente de defectos (\texttt{hotfix/}). Finalmente, para el diseño de prototipos de interfaz de usuario, se emplea \textbf{Figma} como herramienta de prototipado colaborativo, dado que permite la creación y validación de prototipos navegables de alta fidelidad con los stakeholders antes de iniciar la fase de implementación.

% -------------------------------------------------------------------------
% 2.4  HISTORIAS DE USUARIO
% -------------------------------------------------------------------------
\section{Historias de Usuario}
\label{sec:historias_usuario}

Las historias de usuario constituyen el mecanismo primario para la captura y comunicación de los requerimientos funcionales del sistema dentro del marco de trabajo Scrum \cite{cohn2004}. A diferencia de las especificaciones técnicas tradicionales, las historias de usuario describen las funcionalidades del sistema desde la perspectiva del actor que las utilizará, articulando el rol del usuario, la acción que desea ejecutar y el beneficio de negocio o de seguridad que dicha acción le reporta. Este enfoque garantiza que el desarrollo técnico permanezca alineado con el valor real que el sistema debe generar para la institución, evitando la implementación de funcionalidades que, aunque técnicamente sofisticadas, no respondan a una necesidad concreta de los usuarios.

Cada historia de usuario definida para el Módulo de Gestión de Usuarios y Autenticación ha sido estructurada siguiendo el formato estándar: \textit{``Como [rol], quiero [acción], para [beneficio]''}. Para cuantificar el esfuerzo relativo de implementación asociado a cada historia, se aplicó la escala de Fibonacci en la asignación de Puntos de Historia (\textit{Story Points}, SP), donde los valores disponibles son 1, 2, 3, 5, 8, 13 y 21. En este proyecto, se ha establecido una equivalencia de referencia de un punto de historia a dos horas de trabajo efectivo, con el entendimiento de que dicha equivalencia es orientativa y que la velocidad real del equipo se ajusta iteración a iteración en función de los datos históricos de cada sprint. La estimación se realizó mediante la técnica de Planning Poker, en la que el desarrollador evaluó la complejidad técnica, el nivel de incertidumbre y el volumen de trabajo de cada historia en comparación con las demás, y el Product Owner validó la razonabilidad de las estimaciones resultantes.

El backlog del módulo se compone de ocho historias de usuario que cubren el espectro completo de funcionalidades del módulo, desde el acceso seguro al sistema hasta la supervisión administrativa a través del dashboard. A continuación se presenta la primera historia de usuario con su formato completo y detallado, incluyendo sus criterios de aceptación y las tareas técnicas derivadas. Las historias de usuario restantes (HU-02 a HU-08) se documentan en el Anexo \ref{anx:historias_usuario_completas} con el mismo nivel de detalle.

\subsubsection{HU-01: Inicio de Sesión Seguro con JWT}
\label{subsubsec:hu01}

La primera historia de usuario establece el mecanismo fundamental de acceso al sistema. Como usuario del sistema —ya sea docente, miembro de la CPGIC o administrador—, se requiere poder iniciar sesión mediante las credenciales institucionales (correo electrónico y contraseña), con la finalidad de acceder de forma segura a las funcionalidades específicas que corresponden al rol asignado, garantizando que ningún usuario pueda acceder a recursos o acciones para los cuales no cuente con la autorización correspondiente.

Esta historia tiene asignado un esfuerzo de ocho Puntos de Historia, lo que refleja la complejidad técnica inherente a la implementación de un mecanismo de autenticación seguro que involucra la generación y firma de tokens JWT, la validación de credenciales contra la base de datos con hashing de contraseñas mediante bcrypt, el registro de eventos en el log de auditoría y el desarrollo del componente de formulario reactivo en Angular con sus correspondientes validaciones de frontend. La historia pertenece a la categoría Must Have dentro de la priorización MoSCoW, puesto que sin este mecanismo ninguna otra funcionalidad del sistema puede operar de manera segura.

\begin{table}[H]
\centering
\caption{Historia de Usuario HU-01: Inicio de Sesión Seguro con JWT}
\label{tab:hu01}
\begin{tabular}{|p{3.5cm}|p{10.5cm}|}
\hline
\textbf{Atributo} & \textbf{Descripción} \\ \hline
ID & HU-01 \\ \hline
Título & Inicio de Sesión Seguro con JWT \\ \hline
Actor & Usuario del Sistema (Docente, Miembro CPGIC, Administrador) \\ \hline
Historia & Como usuario del sistema, quiero iniciar sesión con mis credenciales institucionales, para acceder de forma segura a las funcionalidades del sistema según mi rol asignado. \\ \hline
Prioridad & Must Have \\ \hline
Story Points & 8 SP \\ \hline
Horas Estimadas & 16 horas \\ \hline
Sprint & Sprint 1 \\ \hline
\end{tabular}
\end{table}

Los criterios de aceptación que determinan cuándo esta historia puede considerarse completada son los siguientes: el sistema presenta al usuario un formulario de inicio de sesión con los campos de correo electrónico institucional y contraseña, con validación en tiempo real del formato del correo antes de enviar la solicitud al servidor; ante credenciales correctas, el backend genera un token JWT firmado que incluye los claims de identificador de usuario, rol asignado y tiempo de expiración, y el frontend redirige al usuario al módulo correspondiente a su rol; ante credenciales incorrectas, el sistema muestra un mensaje de error genérico sin revelar si el error corresponde al correo o a la contraseña, para evitar ataques de enumeración de usuarios; tras cinco intentos fallidos consecutivos, el sistema bloquea la cuenta durante quince minutos y notifica al usuario mediante un mensaje informativo; la sesión expira automáticamente tras sesenta minutos de inactividad; y cada intento de inicio de sesión —exitoso o fallido— queda registrado en el log de auditoría con la marca temporal y la dirección IP del cliente.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.75\textwidth]{02Figures/02Chapter/sprint0/prototipo_login.png}
    \caption{Prototipo de alta fidelidad de la pantalla de inicio de sesión (Figma, Sprint 0)}
    \label{C2.figurePrototipoLogin}
\end{figure}

\subsubsection{HU-02 a HU-08: Resumen del Backlog Completo}
\label{subsubsec:hu02_08_resumen}

La totalidad de las historias de usuario del módulo se sintetiza en la Tabla \ref{tab:resumen_hu}, donde se presentan los elementos esenciales de cada historia para facilitar la lectura del backlog. El desarrollo completo de cada historia, incluyendo sus criterios de aceptación detallados y las tareas técnicas asociadas, se encuentra documentado en el Anexo \ref{anx:historias_usuario_completas}.

\begin{table}[H]
\centering
\caption{Resumen del backlog de historias de usuario del módulo}
\label{tab:resumen_hu}
\begin{tabular}{|l|p{7cm}|c|c|}
\hline
\textbf{ID} & \textbf{Descripción resumida} & \textbf{SP} & \textbf{Prioridad} \\ \hline
HU-01 & Como usuario, quiero iniciar sesión con JWT para acceder según mi rol. & 8 & Must \\ \hline
HU-02 & Como usuario autenticado, quiero cerrar sesión de forma segura para proteger mi información. & 3 & Must \\ \hline
HU-03 & Como usuario, quiero restablecer mi contraseña vía correo para recuperar el acceso de forma autónoma. & 8 & Must \\ \hline
HU-04 & Como administrador, quiero gestionar cuentas de usuario (CRUD) para mantener el registro de personal autorizado. & 13 & Must \\ \hline
HU-05 & Como administrador, quiero asignar roles RBAC para controlar con precisión las acciones de cada usuario. & 8 & Must \\ \hline
HU-06 & Como usuario autenticado, quiero editar mi perfil para mantener mis datos actualizados. & 5 & Should \\ \hline
HU-07 & Como administrador, quiero consultar el log de auditoría para garantizar la trazabilidad del sistema. & 5 & Should \\ \hline
HU-08 & Como administrador, quiero un dashboard con indicadores clave para supervisar el estado del sistema. & 5 & Could \\ \hline
\multicolumn{2}{|r|}{\textbf{Total Story Points}} & \textbf{55} & \\ \hline
\end{tabular}
\end{table}

% -------------------------------------------------------------------------
% 2.5  PRODUCT BACKLOG PRIORIZADO
% -------------------------------------------------------------------------
\section{Product Backlog Priorizado}
\label{sec:product_backlog}

El Product Backlog del Módulo de Gestión de Usuarios y Autenticación ha sido priorizado mediante la técnica MoSCoW, cuyo nombre constituye un acrónimo de las cuatro categorías de prioridad que la conforman: Must Have (debe tener), Should Have (debería tener), Could Have (podría tener) y Won't Have (no tendrá en esta iteración). La aplicación de esta técnica permite al equipo y al Product Owner tomar decisiones informadas sobre qué funcionalidades son absolutamente imprescindibles para que el sistema cumpla con su propósito institucional, cuáles añaden valor significativo pero pueden posponerse si el tiempo resulta insuficiente, y cuáles representan mejoras deseables que se abordarán solo si la capacidad del equipo lo permite.

Las cinco historias clasificadas como Must Have —HU-01 a HU-05— constituyen el núcleo funcional del módulo de seguridad. Sin el mecanismo de autenticación (HU-01), el sistema no puede identificar a sus usuarios; sin el cierre de sesión seguro (HU-02), los tokens JWT quedan expuestos; sin el restablecimiento de contraseña (HU-03), los usuarios bloquedos no pueden recuperar el acceso de manera autónoma; sin la gestión de cuentas (HU-04), el administrador no puede incorporar o revocar el acceso al personal institucional; y sin el sistema de roles RBAC (HU-05), todos los usuarios tendrían acceso irrestricto a la totalidad de las funcionalidades del sistema. Las dos historias clasificadas como Should Have —HU-06 y HU-07— añaden valor operativo relevante: la edición de perfil (HU-06) le otorga autonomía al usuario para mantener sus datos actualizados, y el registro de auditoría (HU-07) provee la trazabilidad necesaria para la supervisión institucional. Finalmente, la historia clasificada como Could Have —HU-08— representa una herramienta de supervisión estratégica para el administrador que, si bien es valiosa, no compromete la operación del sistema en caso de no implementarse.

\begin{table}[H]
\centering
\caption{Product Backlog priorizado con la técnica MoSCoW}
\label{tab:product_backlog_moscow}
\begin{tabular}{|l|p{5.5cm}|c|c|c|l|}
\hline
\textbf{ID} & \textbf{Historia de Usuario} & \textbf{Prioridad} & \textbf{SP} & \textbf{Horas} & \textbf{Sprint} \\ \hline
HU-01 & Inicio de Sesión Seguro (JWT) & Must Have & 8 & 16h & Sprint 1 \\ \hline
HU-02 & Cierre de Sesión Seguro & Must Have & 3 & 6h & Sprint 1 \\ \hline
HU-03 & Restablecimiento de Contraseña & Must Have & 8 & 16h & Sprint 1 \\ \hline
HU-04 & Gestión de Cuentas de Usuario (CRUD) & Must Have & 13 & 26h & Sprint 2 \\ \hline
HU-05 & Gestión de Roles y Permisos (RBAC) & Must Have & 8 & 16h & Sprint 2 \\ \hline
HU-06 & Visualización y Edición de Perfil & Should Have & 5 & 10h & Sprint 3 \\ \hline
HU-07 & Registro de Auditoría y Trazabilidad & Should Have & 5 & 10h & Sprint 3 \\ \hline
HU-08 & Dashboard de Administración & Could Have & 5 & 10h & Sprint 4 \\ \hline
\multicolumn{2}{|c|}{\textbf{Totales}} & — & \textbf{55 SP} & \textbf{110h} & 4 ciclos \\ \hline
\end{tabular}
\end{table}

% -------------------------------------------------------------------------
% 2.6  PLANIFICACIÓN GENERAL DE SPRINTS
% -------------------------------------------------------------------------
\section{Planificación General de Sprints}
\label{sec:planificacion_sprints}

El periodo de cuatro meses comprendido entre el primero de abril y el tres de julio de dos mil veintiséis ha sido segmentado en cinco sprints. El primero de ellos, denominado Sprint 0, ocupa las dos primeras semanas del proyecto (del 1 al 11 de abril de 2026) y se destina íntegramente a las actividades fundacionales del módulo: el análisis de requerimientos mediante entrevistas, el diseño de la arquitectura de software con el Modelo C4, el modelado de la base de datos y la creación de los prototipos de interfaz en Figma. Los cuatro sprints de desarrollo subsiguientes (Sprint 1 al Sprint 4) tienen una duración de tres semanas cada uno, distribuyendo las doscientas ochenta y ocho horas de trabajo de manera que los sprints de mayor densidad técnica —los dos primeros, que contienen las historias Must Have de mayor esfuerzo— cuenten con la capacidad horaria suficiente para garantizar entregas de calidad.

La Tabla \ref{tab:planificacion_general_sprints} sintetiza la planificación general de los cinco sprints, indicando para cada uno el periodo calendario, el objetivo del ciclo, las historias de usuario que lo componen y el estado de avance al momento de redacción de este documento.

\begin{table}[H]
\centering
\caption{Planificación general de los cinco sprints del proyecto}
\label{tab:planificacion_general_sprints}
\begin{tabular}{|c|p{2.8cm}|p{3.5cm}|p{2.5cm}|c|}
\hline
\textbf{Sprint} & \textbf{Período} & \textbf{Objetivo del Sprint} & \textbf{HU Incluidas} & \textbf{Estado} \\ \hline
Sprint 0 & 01 abr — 11 abr 2026 & Arquitectura, prototipado y configuración del entorno de desarrollo & Análisis y diseño & Completado \\ \hline
Sprint 1 & 13 abr — 02 may 2026 & Implementar la autenticación segura completa con JWT & HU-01, HU-02, HU-03 & Completado \\ \hline
Sprint 2 & 04 may — 23 may 2026 & Implementar la gestión de usuarios y el sistema RBAC & HU-04, HU-05 & Completado \\ \hline
Sprint 3 & 25 may — 13 jun 2026 & Implementar el perfil de usuario y el registro de auditoría & HU-06, HU-07 & En proceso \\ \hline
Sprint 4 & 15 jun — 03 jul 2026 & Dashboard, pruebas integrales y documentación final & HU-08, QA & Pendiente \\ \hline
\end{tabular}
\end{table}

% =========================================================================
% SPRINT 0: ARQUITECTURA Y PROTOTIPADO
% =========================================================================
\section{Sprint 0: Arquitectura, Prototipado y Configuración del Entorno}
\label{sec:sprint0}

El Sprint 0 ocupa el período comprendido entre el primero y el once de abril de dos mil veintiséis, constituyendo la fase fundacional sobre la cual se erige la totalidad del desarrollo posterior. A diferencia de los sprints de implementación, el Sprint 0 no produce software ejecutable como entregable principal; su propósito es garantizar que todas las decisiones estructurales del sistema —arquitectónicas, de diseño de datos y de experiencia de usuario— se tomen de manera informada y validada antes de que comience la escritura del código de producción. Esta inversión inicial en diseño y análisis reduce sustancialmente el riesgo de incurrir en deuda técnica en las etapas posteriores y asegura que el desarrollador pueda avanzar con certeza y velocidad durante los sprints de implementación.

\subsection{Sprint Planning}
\label{subsec:sprint0_planning}

El Sprint Planning del Sprint 0 se realizó el primero de abril de dos mil veintiséis, con la participación del desarrollador y del Product Owner. Durante esta sesión se estableció el objetivo del sprint: producir el conjunto completo de artefactos de diseño que servirán de guía para la implementación de todo el módulo. Estos artefactos comprenden los diagramas arquitectónicos bajo el Modelo C4, el modelo entidad-relación de la base de datos, el diagrama de secuencia del flujo de autenticación con JWT y los nueve prototipos de pantalla de alta fidelidad diseñados en Figma. Adicionalmente, al final del Sprint 0 el entorno de desarrollo debe estar completamente configurado, incluyendo el repositorio de GitHub con la estructura de ramas Git Flow, el proyecto Angular inicializado con la arquitectura de módulos definida y el proyecto .NET 8 con la estructura de capas (Controladores, Servicios, Repositorios y Entidades) establecida.

\subsection{Identificación de Requerimientos mediante Entrevistas}
\label{subsec:sprint0_entrevistas}

La primera actividad del Sprint 0 consistió en la realización de entrevistas semiestructuradas con los stakeholders del sistema, con el objetivo de identificar los requerimientos funcionales y no funcionales del módulo desde la perspectiva de los usuarios reales. Se entrevistaron docentes de la Facultad de Ingeniería de Sistemas y miembros de la CPGIC, quienes describieron las dificultades que experimentan con el proceso actual de gestión de titulación y las expectativas que tienen respecto a las funcionalidades de acceso y seguridad del nuevo sistema. El protocolo de entrevista utilizado se documenta en el Anexo \ref{anx:protocolo_entrevista}. Los hallazgos principales de estas entrevistas revelaron que los usuarios esperan un mecanismo de acceso que utilice sus credenciales institucionales (correo @epn.edu.ec) para eliminar la necesidad de gestionar un conjunto separado de contraseñas, que el sistema debe diferenciar con claridad las acciones disponibles para cada tipo de usuario para evitar confusiones y accesos inadvertidos a funcionalidades restringidas, y que la recuperación autónoma de contraseñas es una funcionalidad crítica para reducir la carga operativa del área administrativa de la facultad.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.80\textwidth]{02Figures/02Chapter/sprint0/formato_entrevista.png}
    \caption{Formato de entrevista aplicado a docentes y miembros CPGIC durante el Sprint 0}
    \label{C2.figureFormatoEntrevista}
\end{figure}

\subsection{Diseño Arquitectónico con el Modelo C4}
\label{subsec:sprint0_arquitectura_c4}

Para la comunicación y documentación de la arquitectura del sistema se adoptó el Modelo C4, propuesto por Simon Brown \cite{brown2019c4}, que organiza la descripción arquitectónica en cuatro niveles de abstracción progresiva: Contexto, Contenedores, Componentes y Código. Los dos primeros niveles son los de mayor relevancia para la comprensión del módulo en su relación con el sistema completo y con el entorno institucional, razón por la cual se desarrollaron como artefactos de diseño prioritarios durante el Sprint 0.

\subsubsection{Diagrama de Contexto (Nivel 1)}
\label{subsubsec:c4_nivel1}

El Diagrama de Contexto sitúa al Sistema de Gestión de TICs en su entorno, identificando las personas que interactúan con él y los sistemas externos con los que se comunica. En este nivel de abstracción, el sistema se representa como una caja negra que recibe entradas de sus actores —Docente FIS, Comisión CPGIC, Administrador y Estudiante FIS— y les provee servicios a través de interfaces accesibles vía HTTPS. Este diagrama fue validado con el Product Owner al inicio del Sprint 0 para confirmar que todos los actores relevantes habían sido identificados y que las interacciones representadas correspondían a la realidad operativa de la institución.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.85\textwidth]{02Figures/02Chapter/sprint0/c4_nivel1_contexto.png}
    \caption{Diagrama de Contexto (Nivel 1 del Modelo C4) del Sistema de Gestión de TICs}
    \label{C2.figureC4Nivel1}
\end{figure}

\subsubsection{Diagrama de Contenedores (Nivel 2)}
\label{subsubsec:c4_nivel2}

El Diagrama de Contenedores profundiza un nivel en la arquitectura, revelando los principales bloques tecnológicos que componen el sistema y las formas en que se comunican entre sí. Para el Módulo de Gestión de Usuarios y Autenticación, la arquitectura adoptada es un patrón desacoplado orientado a servicios, compuesto por tres contenedores principales. El primer contenedor es la Aplicación Web Frontend, desarrollada en Angular, que corre en el navegador del usuario y se comunica con el segundo contenedor a través del protocolo HTTPS. El segundo contenedor es la API REST Backend, desarrollada en .NET 8, que expone los endpoints del módulo de autenticación y gestión de usuarios, aloja toda la lógica de negocio y de seguridad —incluyendo la generación y validación de tokens JWT y la evaluación de políticas de autorización RBAC—, y se comunica con el tercer contenedor a través del protocolo TDS. El tercer contenedor es la Base de Datos SQL Server, que persiste la información de usuarios, roles, permisos, sesiones y eventos de auditoría.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.85\textwidth]{02Figures/02Chapter/sprint0/c4_nivel2_contenedores.png}
    \caption{Diagrama de Contenedores (Nivel 2 del Modelo C4) del Sistema de Gestión de TICs}
    \label{C2.figureC4Nivel2}
\end{figure}

\subsection{Modelo Entidad-Relación de la Base de Datos}
\label{subsec:sprint0_mer}

El diseño del esquema de la base de datos es una de las decisiones arquitectónicas de mayor impacto en el módulo de seguridad, dado que la integridad y la consistencia de los datos de usuarios, roles y permisos son condiciones necesarias para el correcto funcionamiento del sistema de control de acceso. Durante el Sprint 0 se diseñó el Modelo Entidad-Relación (MER) que establece la estructura de las tablas que soportan el módulo.

Las entidades principales del modelo son las siguientes. La entidad \textbf{Usuarios} almacena los datos de cada cuenta registrada en el sistema, incluyendo el identificador único (UserId), el nombre y apellido, el correo electrónico institucional, el hash de la contraseña generado con el algoritmo bcrypt, la fotografía de perfil, la marca temporal de creación, la fecha del último inicio de sesión y el indicador de estado activo o inactivo. La entidad \textbf{Roles} define los cuatro roles disponibles en el sistema (Administrador, Docente, Presidente CPGIC y Miembro CPGIC), almacenando para cada uno su identificador, nombre y descripción. La relación entre usuarios y roles se implementa a través de la tabla de asociación \textbf{UsuariosRoles}, que permite que un usuario tenga exactamente un rol activo en un momento dado. La entidad \textbf{Permisos} define las acciones granulares que pueden realizarse en el sistema, y la tabla \textbf{RolesPermisos} establece qué permisos corresponden a cada rol. La entidad \textbf{LogAuditoria} registra cada evento crítico ejecutado en el sistema, almacenando el tipo de acción, el usuario que la ejecutó, la marca temporal, la dirección IP del cliente y el resultado de la operación. La entidad \textbf{TokensRecuperacion} almacena los tokens de un solo uso generados durante el flujo de restablecimiento de contraseña, con su fecha de expiración y un indicador de si ya han sido utilizados.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.90\textwidth]{02Figures/02Chapter/sprint0/modelo_entidad_relacion.png}
    \caption{Modelo Entidad-Relación de la base de datos del Módulo de Gestión de Usuarios y Autenticación}
    \label{C2.figureMER}
\end{figure}

\subsection{Diagrama de Secuencia del Flujo de Autenticación JWT}
\label{subsec:sprint0_secuencia}

Para ilustrar con precisión el comportamiento dinámico del mecanismo de autenticación, se elaboró el diagrama de secuencia del flujo de inicio de sesión con JWT. Este artefacto es de especial importancia para el módulo de seguridad, dado que documenta la secuencia exacta de mensajes intercambiados entre el navegador del usuario, el componente Angular de frontend, el controlador de autenticación del backend y el motor de base de datos SQL Server, desde el momento en que el usuario ingresa sus credenciales hasta que recibe el token de acceso y es redirigido al módulo correspondiente a su rol. El diagrama incluye también los flujos alternativos de error, correspondientes a credenciales inválidas, cuenta bloqueada por intentos fallidos y token expirado, asegurando que el comportamiento del sistema ante situaciones de fallo esté completamente especificado antes de comenzar la implementación.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.88\textwidth]{02Figures/02Chapter/sprint0/diagrama_secuencia_login.png}
    \caption{Diagrama de secuencia del flujo de autenticación con JWT (flujo principal y alternativo de error)}
    \label{C2.figureSecuenciaLogin}
\end{figure}

\subsection{Prototipos de Interfaz de Usuario (UX/UI)}
\label{subsec:sprint0_prototipos}

El diseño de la experiencia de usuario (UX) y de la interfaz gráfica (UI) se llevó a cabo mediante la herramienta Figma, con la cual se crearon nueve prototipos navegables de alta fidelidad que representan las pantallas críticas del módulo. El enfoque metodológico seguido para el diseño de los prototipos priorizó la simplicidad y la claridad visual por encima de la complejidad estética, puesto que el sistema será utilizado por usuarios con distintos niveles de familiaridad con herramientas digitales, incluyendo docentes de larga trayectoria en la institución. Cada prototipo fue desarrollado siguiendo los principios de diseño centrado en el usuario y las heurísticas de usabilidad de Nielsen, asegurando que las acciones más frecuentes sean accesibles con el menor número de pasos posible y que los mensajes de error sean comprensibles y accionables.

La pantalla de inicio de sesión, cuyo prototipo se presenta en la Figura \ref{C2.figurePrototipoLogin}, constituye el artefacto de diseño más representativo del Sprint 0. Presenta un formulario compacto con los campos de correo institucional y contraseña, un enlace de recuperación de contraseña, el logotipo de la Escuela Politécnica Nacional y un mensaje de bienvenida que contextualiza al usuario en el sistema WebTIC FIS. La decisión de diseño de mostrar un único formulario sin opciones de registro responde a la política institucional de que las cuentas deben ser creadas exclusivamente por el administrador del sistema. Las pantallas restantes —que incluyen el formulario de recuperación de contraseña, el panel de gestión de usuarios, el formulario de creación de cuentas, la vista de edición de roles, la página de perfil de usuario, el log de auditoría, el dashboard y el diálogo de confirmación de cierre de sesión— se presentan en el Anexo \ref{anx:prototipos_figma}.

\subsection{Configuración del Entorno de Desarrollo}
\label{subsec:sprint0_entorno}

La configuración del entorno de desarrollo representa la última actividad del Sprint 0 y la que habilita técnicamente el inicio de los sprints de implementación. Durante esta etapa se realizaron las siguientes acciones de manera secuencial y documentada. En primer lugar, se creó el repositorio de GitHub bajo el nombre \texttt{webt-tic-fis-auth-module} y se configuró la estructura de ramas del flujo Git Flow, estableciendo las ramas \texttt{main}, \texttt{develop} y la rama inicial \texttt{feature/sprint0-setup}. En segundo lugar, se inicializó el proyecto Angular utilizando la herramienta de línea de comandos Angular CLI, definiendo la estructura de módulos lazy-loading que organizará el código del frontend a lo largo del desarrollo. En tercer lugar, se creó el proyecto de la Web API en .NET 8, configurando la estructura de capas —Controladores, Servicios, Repositorios, Entidades y DTOs— y realizando la instalación de los paquetes NuGet necesarios para la autenticación JWT, el acceso a datos con Entity Framework Core y el envío de correos electrónicos. En cuarto lugar, se creó la base de datos inicial en SQL Server, ejecutando el script de creación de las tablas diseñadas en el MER del Sprint 0. Finalmente, se verificó la comunicación entre el frontend Angular y el backend .NET 8 mediante un endpoint de prueba de conectividad, confirmando que la configuración CORS y los certificados de desarrollo se encontraban correctamente establecidos.

\begin{table}[H]
\centering
\caption{Resumen de entregables y estado del Sprint 0}
\label{tab:sprint0_entregables}
\begin{tabular}{|p{7cm}|p{4cm}|c|}
\hline
\textbf{Entregable} & \textbf{Herramienta / Formato} & \textbf{Estado} \\ \hline
Protocolo de entrevistas con stakeholders & Documento PDF & Completado \\ \hline
Diagrama de Contexto C4 (Nivel 1) & Draw.io / PNG & Completado \\ \hline
Diagrama de Contenedores C4 (Nivel 2) & Draw.io / PNG & Completado \\ \hline
Modelo Entidad-Relación de la base de datos & Draw.io / PNG & Completado \\ \hline
Diagrama de Secuencia del flujo JWT & Draw.io / PNG & Completado \\ \hline
9 prototipos navegables de alta fidelidad & Figma / URL & Completado \\ \hline
Repositorio GitHub con estructura Git Flow & GitHub & Completado \\ \hline
Proyecto Angular inicializado con módulos & Visual Studio Code & Completado \\ \hline
Proyecto .NET 8 Web API con estructura de capas & Visual Studio 2022 & Completado \\ \hline
Base de datos SQL Server con esquema inicial & SSMS / SQL Script & Completado \\ \hline
\end{tabular}
\end{table}

\subsection{Sprint Review del Sprint 0}
\label{subsec:sprint0_review}

La ceremonia de Sprint Review del Sprint 0 se llevó a cabo el once de abril de dos mil veintiséis, con la participación del estudiante y del Product Owner. Durante la sesión se presentaron la totalidad de los artefactos de diseño producidos durante el sprint: los diagramas arquitectónicos C4, el modelo entidad-relación, el diagrama de secuencia y los nueve prototipos de Figma. El Product Owner revisó cada prototipo de pantalla e indicó ajustes menores en el diseño del formulario de gestión de usuarios, específicamente en la disposición de los botones de acción, cambios que fueron incorporados al diseño en la misma jornada antes del inicio del Sprint 1. Los diagramas arquitectónicos fueron aprobados sin observaciones, y el entorno de desarrollo fue demostrado mediante la ejecución de la solicitud de conectividad entre el frontend y el backend. La Sprint Retrospective realizada a continuación identificó como fortaleza del sprint la claridad del proceso de diseño del MER y como área de mejora la necesidad de documentar con mayor detalle las decisiones de seguridad adoptadas para el esquema de hashing de contraseñas.

% =========================================================================
% SPRINT 1: AUTENTICACIÓN Y SEGURIDAD BASE
% =========================================================================
\section{Sprint 1: Autenticación Segura y Control de Sesión}
\label{sec:sprint1}

El Sprint 1 abarca el período comprendido entre el trece de abril y el dos de mayo de dos mil veintiséis, con una duración de tres semanas de trabajo efectivo. Este sprint representa el ciclo de mayor criticidad técnica del proyecto, dado que en él se implementan los mecanismos fundamentales de seguridad del sistema: la autenticación mediante tokens JWT, el cierre de sesión con invalidación del token en el servidor y el flujo completo de restablecimiento de contraseña. Sin estos tres componentes operativos, ninguno de los sprints posteriores puede progresar, ya que la totalidad de las funcionalidades subsiguientes del módulo depende de la existencia de una infraestructura de identidad y acceso robusta y probada. El Sprint Goal establecido durante el Sprint Planning es el siguiente: al finalizar el Sprint 1, cualquier usuario del sistema debe poder autenticarse de forma segura utilizando sus credenciales institucionales, acceder a las funcionalidades correspondientes a su rol, cerrar su sesión invalidando el token de acceso en el servidor y recuperar el acceso a su cuenta mediante el flujo de restablecimiento de contraseña por correo electrónico, con todos estos mecanismos debidamente registrados en el log de auditoría.

\subsection{Sprint Planning}
\label{subsec:sprint1_planning}

El Sprint Planning del Sprint 1 se llevó a cabo el trece de abril de dos mil veintiséis. Las historias de usuario seleccionadas para este sprint son HU-01 (Inicio de Sesión Seguro con JWT, 8 SP), HU-02 (Cierre de Sesión Seguro, 3 SP) y HU-03 (Restablecimiento de Contraseña, 8 SP), que en conjunto suman diecinueve Puntos de Historia. El Sprint Backlog resultante descompone estas tres historias en diecinueve tareas técnicas concretas, distribuidas entre las capas de base de datos, backend y frontend, con estimaciones de tiempo individuales que suman un total de treinta y ocho horas de trabajo efectivo.

La Tabla \ref{tab:sprint1_backlog} presenta el Sprint Backlog completo del Sprint 1, detallando para cada tarea su identificador, la historia de usuario a la que pertenece, la descripción técnica de la actividad, la capa del sistema a la que corresponde y la estimación de horas asignada.

\begin{table}[H]
\centering
\caption{Sprint Backlog del Sprint 1: Autenticación Segura y Control de Sesión}
\label{tab:sprint1_backlog}
\begin{tabular}{|c|c|p{6cm}|p{2cm}|c|}
\hline
\textbf{ID Tarea} & \textbf{HU} & \textbf{Descripción de la Tarea} & \textbf{Capa} & \textbf{Est. (h)} \\ \hline
T1-01 & HU-01 & Configuración de la estructura de tablas de autenticación en SQL Server: Usuarios, Roles y LogAuditoria. & BD & 3h \\ \hline
T1-02 & HU-01 & Implementación del endpoint POST /api/auth/login con validación de credenciales y generación de JWT firmado. & Backend & 5h \\ \hline
T1-03 & HU-01 & Implementación del mecanismo de bloqueo de cuenta tras cinco intentos fallidos con temporizador de quince minutos. & Backend & 3h \\ \hline
T1-04 & HU-01 & Configuración del servicio de hashing de contraseñas con bcrypt y definición de la política de seguridad de contraseñas. & Backend & 2h \\ \hline
T1-05 & HU-01 & Desarrollo del componente LoginComponent en Angular con formulario reactivo y validaciones de formato de correo. & Frontend & 4h \\ \hline
T1-06 & HU-01 & Implementación del servicio AuthService en Angular para la gestión del token JWT en el almacenamiento de sesión. & Frontend & 3h \\ \hline
T1-07 & HU-01 & Pruebas funcionales del flujo de inicio de sesión: credenciales válidas, inválidas y cuenta bloqueada. & QA & 2h \\ \hline
T2-01 & HU-02 & Implementación del endpoint POST /api/auth/logout con invalidación del token en la lista negra (blacklist) del servidor. & Backend & 2h \\ \hline
T2-02 & HU-02 & Implementación del AuthGuard y del HttpInterceptor en Angular para la protección de rutas y la adjunción automática del token. & Frontend & 2h \\ \hline
T2-03 & HU-02 & Pruebas funcionales del cierre de sesión y de la protección de rutas privadas tras el logout. & QA & 1h \\ \hline
T3-01 & HU-03 & Creación de la tabla TokensRecuperacion en SQL Server con campos de token, expiración y estado de uso. & BD & 1h \\ \hline
T3-02 & HU-03 & Implementación del endpoint POST /api/auth/forgot-password con generación de token único y registro en la base de datos. & Backend & 3h \\ \hline
T3-03 & HU-03 & Configuración del servicio de envío de correos electrónicos (SMTP institucional) en la capa de infraestructura de .NET 8. & Backend & 2h \\ \hline
T3-04 & HU-03 & Implementación del endpoint POST /api/auth/reset-password con validación del token, verificación de expiración y actualización del hash de contraseña. & Backend & 3h \\ \hline
T3-05 & HU-03 & Desarrollo del componente ForgotPasswordComponent en Angular con formulario de ingreso del correo y mensaje de confirmación genérico. & Frontend & 2h \\ \hline
T3-06 & HU-03 & Desarrollo del componente ResetPasswordComponent en Angular con formulario de nueva contraseña y confirmación, incluyendo validación de política. & Frontend & 2h \\ \hline
T3-07 & HU-03 & Diseño y maquetación del correo electrónico de restablecimiento de contraseña en formato HTML responsivo. & Frontend & 1h \\ \hline
T3-08 & HU-03 & Pruebas funcionales del flujo de restablecimiento: correo registrado, no registrado, token expirado y contraseña no conforme con la política. & QA & 2h \\ \hline
\multicolumn{4}{|r|}{\textbf{Total de horas estimadas}} & \textbf{43h} \\ \hline
\end{tabular}
\end{table}

\subsection{Implementación de la Capa de Base de Datos}
\label{subsec:sprint1_bd}

La primera actividad técnica del Sprint 1 consistió en la creación de las tablas de base de datos que soportan las tres historias de usuario del sprint. Dado que el esquema general ya había sido diseñado durante el Sprint 0, la tarea T1-01 se centró en traducir el Modelo Entidad-Relación a sentencias SQL de creación de tablas y en validar que los tipos de datos, las restricciones de integridad referencial y los índices de búsqueda estuvieran correctamente definidos para garantizar tanto la seguridad como el rendimiento del módulo de autenticación. La tabla \texttt{Usuarios} fue creada con los campos definidos en el MER, incluyendo la restricción de unicidad sobre el campo de correo electrónico para prevenir el registro de cuentas duplicadas. La tabla \texttt{LogAuditoria} fue diseñada con un índice compuesto sobre los campos de tipo de acción y marca temporal, optimizando las consultas de filtrado que el administrador realizará frecuentemente desde el módulo de trazabilidad. La tabla \texttt{TokensRecuperacion} (tarea T3-01) incluye un índice único sobre el campo de token para garantizar la búsqueda eficiente durante el proceso de validación del enlace de restablecimiento.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.88\textwidth]{02Figures/02Chapter/sprint1/scripts_sql_sprint1.png}
    \caption{Esquema de las tablas creadas en SQL Server durante el Sprint 1, visualizado en SQL Server Management Studio}
    \label{C2.figureSqlSprint1}
\end{figure}

\subsection{Implementación de la Capa Backend}
\label{subsec:sprint1_backend}

La implementación del backend durante el Sprint 1 se organizó siguiendo el patrón de arquitectura en capas del proyecto .NET 8, respetando los principios de separación de responsabilidades y de inversión de dependencias. Cada endpoint de autenticación fue desarrollado en un controlador dedicado (\texttt{AuthController}), que delega la lógica de negocio a un servicio (\texttt{AuthService}) y accede a los datos a través de un repositorio (\texttt{UsuarioRepository}).

El endpoint \texttt{POST /api/auth/login} (tarea T1-02) recibe en el cuerpo de la solicitud un objeto JSON con los campos de correo electrónico y contraseña. El servicio de autenticación recupera el registro del usuario por correo electrónico, verifica que la cuenta esté en estado activo, compara la contraseña ingresada con el hash almacenado utilizando bcrypt, y en caso de éxito genera un token JWT firmado con la clave secreta configurada en el servidor. El token incluye los siguientes claims: el identificador del usuario (\texttt{sub}), el correo electrónico (\texttt{email}), el rol asignado (\texttt{role}), la fecha de emisión (\texttt{iat}) y la fecha de expiración (\texttt{exp}), fijada en sesenta minutos a partir de la emisión. Si la verificación de credenciales falla, el servicio incrementa el contador de intentos fallidos del usuario; si dicho contador alcanza el umbral de cinco intentos, la cuenta es bloqueada temporalmente y se registra el evento de bloqueo en el log de auditoría. La respuesta al cliente en caso de éxito devuelve el token JWT y los datos básicos del usuario necesarios para que el frontend configure la sesión.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.90\textwidth]{02Figures/02Chapter/sprint1/codigo_auth_controller.png}
    \caption{Implementación del endpoint de autenticación en el controlador AuthController (.NET 8)}
    \label{C2.figureAuthController}
\end{figure}

El mecanismo de bloqueo de cuenta (tarea T1-03) se implementó añadiendo tres campos a la entidad \texttt{Usuario}: \texttt{IntentosFallidos} (entero), \texttt{BloqueadoHasta} (fecha y hora nullable) y \texttt{FechaUltimoAcceso} (fecha y hora). En cada solicitud de inicio de sesión, el servicio verifica en primer lugar si el campo \texttt{BloqueadoHasta} contiene un valor futuro respecto al momento actual; de ser así, rechaza inmediatamente la solicitud con el mensaje informativo correspondiente sin llegar a evaluar las credenciales. El servicio de hashing (tarea T1-04) utiliza la biblioteca \texttt{BCrypt.Net-Next}, que implementa el algoritmo bcrypt con un factor de trabajo (\textit{work factor}) de doce, proporcionando un equilibrio adecuado entre seguridad y rendimiento en el contexto de una API institucional.

El endpoint \texttt{POST /api/auth/logout} (tarea T2-01) implementa la invalidación del token mediante el patrón de lista negra (\textit{token blacklist}). Al recibir la solicitud de cierre de sesión, el servicio extrae el identificador único del token (claim \texttt{jti}) y lo almacena en la tabla \texttt{TokensBlacklist} junto con la fecha de expiración original del token. El middleware de autenticación verifica en cada solicitud entrante si el \texttt{jti} del token presentado se encuentra en la lista negra, rechazando la solicitud con un código HTTP 401 si así fuera. Esta implementación garantiza que un token que ha sido formalmente cerrado no pueda ser reutilizado incluso si el período de expiración no ha transcurrido.

Los endpoints del flujo de restablecimiento de contraseña (tareas T3-02 a T3-04) implementan el patrón de token de un solo uso. Cuando el usuario solicita el restablecimiento, el servicio genera un token criptográficamente aleatorio de cuarenta y ocho bytes utilizando la clase \texttt{RandomNumberGenerator} de .NET, lo convierte a formato Base64URL para su inclusión segura en un enlace HTTP, lo almacena en la tabla \texttt{TokensRecuperacion} con una ventana de validez de veinticuatro horas y envía el correo electrónico al usuario a través del servicio SMTP configurado. Cuando el usuario utiliza el enlace para establecer su nueva contraseña, el servicio valida que el token exista en la base de datos, que no haya sido previamente utilizado y que no haya expirado; si todos los controles pasan, actualiza el hash de la contraseña del usuario y marca el token como utilizado, impidiendo su reutilización.

\subsection{Implementación de la Capa Frontend}
\label{subsec:sprint1_frontend}

El desarrollo del frontend durante el Sprint 1 se centró en la creación de los componentes Angular que materializan los flujos de autenticación descritos en los prototipos del Sprint 0. Los componentes fueron diseñados siguiendo el patrón de formularios reactivos (\textit{Reactive Forms}) de Angular, que proporciona un mecanismo robusto para la validación en tiempo real de los datos ingresados por el usuario y para la gestión del estado del formulario.

El componente \texttt{LoginComponent} (tarea T1-05) implementa el formulario de inicio de sesión con dos controles reactivos: \texttt{email} y \texttt{password}. El control de correo electrónico incluye los validadores de campo requerido y de formato de correo electrónico, este último mediante una expresión regular que verifica el sufijo \texttt{@epn.edu.ec}. El formulario deshabilita el botón de envío mientras la solicitud está en proceso para prevenir el envío duplicado, y muestra indicadores visuales de carga durante la espera de la respuesta del servidor. Al recibir una respuesta exitosa del backend, el componente extrae el token JWT y los datos del usuario del cuerpo de la respuesta, los almacena mediante el \texttt{AuthService} (tarea T1-06) y realiza la redirección al módulo correspondiente al rol del usuario autenticado.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.80\textwidth]{02Figures/02Chapter/sprint1/pantalla_login_implementada.png}
    \caption{Pantalla de inicio de sesión implementada en Angular, correspondiente al prototipo diseñado en el Sprint 0}
    \label{C2.figurePantallaLoginImpl}
\end{figure}

El \texttt{AuthService} (tarea T1-06) centraliza la lógica de gestión de la sesión en el frontend. Este servicio es el responsable de almacenar el token JWT tras un inicio de sesión exitoso, de proveer métodos para verificar si el usuario está autenticado, de decodificar el token para extraer el rol y otros claims sin realizar una solicitud adicional al servidor, y de eliminar el token del almacenamiento al ejecutar el cierre de sesión. La decisión de almacenar el token en el \texttt{sessionStorage} del navegador (en lugar del \texttt{localStorage}) se fundamenta en que el primero se elimina automáticamente cuando el usuario cierra la pestaña o el navegador, reduciendo el riesgo de accesos no autorizados en equipos compartidos.

El \texttt{AuthGuard} y el \texttt{HttpInterceptor} (tarea T2-02) trabajan en conjunto para proteger las rutas de la aplicación y garantizar que todas las solicitudes al backend incluyan el token JWT en el encabezado de autorización. El \texttt{AuthGuard} intercepta la navegación hacia rutas protegidas y verifica la presencia y validez del token antes de permitir el acceso; si el token no existe o ha expirado, redirige al usuario a la pantalla de inicio de sesión. El \texttt{HttpInterceptor} agrega automáticamente el encabezado \texttt{Authorization: Bearer \{token\}} a todas las solicitudes HTTP salientes dirigidas al backend, eliminando la necesidad de configurar dicho encabezado manualmente en cada componente que realice llamadas a la API.

\subsection{Pruebas Funcionales}
\label{subsec:sprint1_pruebas}

Al finalizar el desarrollo de las tareas técnicas del Sprint 1, se ejecutaron las pruebas funcionales de caja negra definidas en los criterios de aceptación de las historias HU-01, HU-02 y HU-03. Cada caso de prueba fue documentado con su identificador, la historia de usuario a la que corresponde, los datos de entrada utilizados, el resultado esperado según los criterios de aceptación y el resultado obtenido durante la ejecución.

\begin{table}[H]
\centering
\caption{Resultados de las pruebas funcionales}
\label{tab:pruebas_sprint1}
\begin{tabular}{|c|c|p{4.5cm}|p{3cm}|c|}
\hline
\textbf{ID Caso} & \textbf{HU} & \textbf{Descripción del Caso} & \textbf{Resultado Esperado} & \textbf{Estado} \\ \hline
CP1-01 & HU-01 & Login con correo y contraseña válidos & Token JWT generado, redirección al dashboard del rol & Aprobado \\ \hline
CP1-02 & HU-01 & Login con contraseña incorrecta & Mensaje de error genérico, sin revelar campo incorrecto & Aprobado \\ \hline
CP1-03 & HU-01 & Cinco intentos fallidos consecutivos & Cuenta bloqueada 15 min, mensaje informativo al usuario & Aprobado \\ \hline
CP1-04 & HU-01 & Login con correo sin dominio @epn.edu.ec & Validación frontend rechaza el formato antes de enviar & Aprobado \\ \hline
CP1-05 & HU-01 & Verificación del token JWT generado & Token contiene claims de rol, email, iat y exp correctos & Aprobado \\ \hline
CP1-06 & HU-02 & Cierre de sesión desde el menú del usuario & Token invalidado en blacklist, redirección al Login & Aprobado \\ \hline
CP1-07 & HU-02 & Acceso a ruta protegida tras cierre de sesión & Redirección automática a la pantalla de Login & Aprobado \\ \hline
CP1-08 & HU-02 & Uso del token invalidado tras logout & Respuesta HTTP 401 Unauthorized del backend & Aprobado \\ \hline
CP1-09 & HU-03 & Solicitud de reset con correo registrado & Correo enviado, mensaje de confirmación genérico mostrado & Aprobado \\ \hline
CP1-10 & HU-03 & Solicitud de reset con correo no registrado & Mismo mensaje de confirmación genérico (sin revelar existencia) & Aprobado \\ \hline
CP1-11 & HU-03 & Uso de enlace de reset expirado (\textgreater{}24h) & Error 'Enlace inválido o expirado', sin cambio de contraseña & Aprobado \\ \hline
CP1-12 & HU-03 & Nueva contraseña sin cumplir política de seguridad & Error de validación con descripción de la política requerida & Aprobado \\ \hline
CP1-13 & HU-03 & Uso del mismo enlace de reset dos veces & Segundo uso rechazado: token ya utilizado & Aprobado \\ \hline
\end{tabular}
\end{table}

La totalidad de los trece casos de prueba ejecutados durante el Sprint 1 produjo el resultado esperado, lo que permite declarar las historias HU-01, HU-02 y HU-03 como completadas según la Definición de Hecho establecida para el proyecto. No se identificaron defectos durante esta fase de pruebas.

\subsection{Sprint Review y Retrospectiva}
\label{subsec:sprint1_review}

La ceremonia de Sprint Review del Sprint 1 se realizó el dos de mayo de dos mil veintiséis, con la participación del estudiante y del Product Owner. Durante la sesión se demostró el flujo completo de autenticación en el entorno de desarrollo: el Product Owner pudo verificar el inicio de sesión con credenciales de docente y con credenciales de administrador, observar la redirección diferenciada por rol, ejecutar el flujo de cierre de sesión e iniciar el proceso de restablecimiento de contraseña desde la pantalla de Login. El Product Owner declaró el sprint como satisfactoriamente completado sin formular observaciones sobre las funcionalidades demostradas. En la Sprint Retrospective, el equipo identificó como principal fortaleza del sprint la solidez del diseño arquitectónico realizado en el Sprint 0, que permitió avanzar con velocidad y confianza durante la implementación. Como área de mejora se identificó la necesidad de preparar con mayor anticipación los datos de prueba en la base de datos, para que la ejecución de los casos de prueba al final del sprint pueda realizarse sin interrupciones.

% =========================================================================
% SPRINT 2: GESTIÓN DE USUARIOS Y CONTROL DE ACCESO RBAC
% =========================================================================
\section{Sprint 2: Gestión de Usuarios y Control de Acceso Basado en Roles}
\label{sec:sprint2}

El Sprint 2 comprende el período del cuatro al veintitrés de mayo de dos mil veintiséis y tiene como propósito la implementación de las dos historias de usuario de mayor carga de trabajo del proyecto: la gestión completa del ciclo de vida de las cuentas de usuario (HU-04, con trece Puntos de Historia) y el sistema de control de acceso basado en roles bajo el modelo RBAC (HU-05, con ocho Puntos de Historia). En conjunto, estas dos historias suman veintiún Puntos de Historia, lo que convierte al Sprint 2 en el ciclo de mayor densidad de esfuerzo del proyecto. El Sprint Goal del sprint es el siguiente: al finalizar el Sprint 2, el administrador del sistema debe poder gestionar de manera integral las cuentas de usuario —creando, visualizando, editando y desactivando perfiles— y asignar o modificar el rol de cualquier usuario, con la garantía de que cada rol otorga acceso exclusivamente a los recursos y funcionalidades para los cuales está autorizado, tanto en el frontend Angular como en el backend .NET 8.

\subsection{Sprint Planning}
\label{subsec:sprint2_planning}

El Sprint Planning del Sprint 2 se llevó a cabo el cuatro de mayo de dos mil veintiséis. Las historias seleccionadas fueron HU-04 y HU-05, descompuestas en diecisiete tareas técnicas que cubren las tres capas del sistema. La capacidad estimada para el sprint es de cuarenta y dos horas de trabajo efectivo, considerando la mayor complejidad relativa de las historias respecto a las del Sprint 1.

\begin{table}[H]
\centering
\caption{Sprint Backlog: Gestión de Usuarios y Control de Acceso RBAC}
\label{tab:sprint2_backlog}
\begin{tabular}{|c|c|p{5.8cm}|p{2cm}|c|}
\hline
\textbf{ID Tarea} & \textbf{HU} & \textbf{Descripción de la Tarea} & \textbf{Capa} & \textbf{Est. (h)} \\ \hline
T4-01 & HU-04 & Diseño de prototipos de pantalla de gestión de usuarios en Figma: lista, formulario de creación y edición. & UX/UI & 2h \\ \hline
T4-02 & HU-04 & Implementación del endpoint GET /api/usuarios con paginación, búsqueda por nombre o correo y filtros por rol y estado. & Backend & 3h \\ \hline
T4-03 & HU-04 & Implementación del endpoint POST /api/usuarios para creación de cuenta con generación de contraseña temporal y envío de correo. & Backend & 4h \\ \hline
T4-04 & HU-04 & Implementación del endpoint PUT /api/usuarios/\{id\} para la actualización de los datos del perfil administrativo del usuario. & Backend & 2h \\ \hline
T4-05 & HU-04 & Implementación del endpoint PATCH /api/usuarios/\{id\}/estado para la activación y desactivación de cuentas sin eliminación de datos. & Backend & 2h \\ \hline
T4-06 & HU-04 & Implementación de la restricción de unicidad de correo electrónico y validación de correo institucional en el servicio de creación de usuarios. & Backend & 2h \\ \hline
T4-07 & HU-04 & Desarrollo del módulo UserManagementModule en Angular con la tabla de usuarios, paginación, búsqueda y filtros activos. & Frontend & 5h \\ \hline
T4-08 & HU-04 & Desarrollo del formulario modal de creación y edición de usuario con validaciones reactivas en Angular. & Frontend & 4h \\ \hline
T4-09 & HU-04 & Implementación del botón de activación y desactivación de cuenta con diálogo de confirmación en Angular. & Frontend & 2h \\ \hline
T4-10 & HU-04 & Pruebas funcionales del CRUD de usuarios: creación con correo duplicado, edición, desactivación y reactivación. & QA & 3h \\ \hline
T5-01 & HU-05 & Diseño e implementación de las tablas Roles, Permisos y RolesPermisos en SQL Server con la matriz de permisos inicial. & BD & 2h \\ \hline
T5-02 & HU-05 & Configuración de las políticas de autorización basadas en roles en .NET 8 mediante el sistema de Policy-Based Authorization. & Backend & 3h \\ \hline
T5-03 & HU-05 & Decoración de todos los endpoints de la API con los atributos de autorización correspondientes al rol requerido para acceder a cada recurso. & Backend & 3h \\ \hline
T5-04 & HU-05 & Implementación del endpoint PATCH /api/usuarios/\{id\}/rol para la modificación del rol de un usuario por parte del administrador. & Backend & 2h \\ \hline
T5-05 & HU-05 & Desarrollo de la directiva de rol en Angular (\texttt{*appHasRole}) para controlar la visibilidad de elementos de la interfaz según el rol del usuario activo. & Frontend & 3h \\ \hline
T5-06 & HU-05 & Implementación de guardas de ruta por rol en Angular para restringir el acceso a módulos específicos según el rol del token JWT. & Frontend & 3h \\ \hline
T5-07 & HU-05 & Pruebas de autorización: verificar que cada rol accede únicamente a los recursos permitidos y recibe HTTP 403 ante accesos no autorizados. & QA & 3h \\ \hline
\multicolumn{4}{|r|}{\textbf{Total de horas estimadas}} & \textbf{48h} \\ \hline
\end{tabular}
\end{table}

\subsection{Diseño de Prototipos}
\label{subsec:sprint2_prototipos}

El Sprint 2 inició con el diseño de los prototipos de alta fidelidad en Figma para las pantallas de gestión de usuarios (tarea T4-01), dado que el Sprint 0 produjo únicamente los prototipos de las pantallas de autenticación. Las pantallas diseñadas en esta etapa incluyen el panel de listado de usuarios con la tabla de datos, los controles de búsqueda y filtrado, y los botones de acción por fila; el formulario modal de creación de nuevos usuarios; el formulario de edición de datos del perfil; y el diálogo de confirmación de cambio de estado de la cuenta. Estos prototipos fueron revisados brevemente con el Product Owner antes de proceder con la implementación, verificando que el diseño del panel de administración fuera consistente con el estilo visual establecido en los prototipos de autenticación del Sprint 0 y que los flujos de interacción fueran intuitivos para el perfil de usuario administrador.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.88\textwidth]{02Figures/02Chapter/sprint2/prototipo_gestion_usuarios.png}
    \caption{Prototipo de alta fidelidad del panel de gestión de usuarios diseñado en Figma durante el Sprint 2}
    \label{C2.figurePrototipoGestionUsuarios}
\end{figure}

\subsection{Implementación de la Capa de Base de Datos — Sprint 2}
\label{subsec:sprint2_bd}

La tarea T5-01 del Sprint 2 consistió en la creación de las tablas que soportan el modelo RBAC del sistema. La tabla \texttt{Roles} fue poblada con los cuatro registros correspondientes a los roles del sistema: Administrador (RolId = 1), Docente (RolId = 2), Presidente CPGIC (RolId = 3) y Miembro CPGIC (RolId = 4). La tabla \texttt{Permisos} fue poblada con los permisos granulares del sistema, organizados por recurso y acción (por ejemplo, \texttt{usuarios:crear}, \texttt{usuarios:editar}, \texttt{usuarios:desactivar}, \texttt{propuestas:aprobar}). La tabla de asociación \texttt{RolesPermisos} fue cargada con la matriz de permisos acordada con el Product Owner, que establece con precisión qué acciones puede ejecutar cada rol dentro del sistema. Esta configuración inicial de la base de datos garantiza que el sistema parta de un estado de permisos bien definido desde el primer despliegue.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.85\textwidth]{02Figures/02Chapter/sprint2/matriz_roles_permisos.png}
    \caption{Matriz de roles y permisos del sistema implementada en SQL Server durante el Sprint 2}
    \label{C2.figureMatrizRolesPermisos}
\end{figure}

\subsection{Implementación de la Capa Backend}
\label{subsec:sprint2_backend}

La implementación del backend del Sprint 2 se organizó en dos bloques funcionales: los endpoints de gestión de usuarios y la infraestructura de autorización RBAC.

Los endpoints de gestión de usuarios (tareas T4-02 a T4-06) fueron desarrollados en un controlador dedicado, \texttt{UsuariosController}, que opera bajo la restricción de acceso exclusivo al rol de Administrador. El endpoint \texttt{GET /api/usuarios} implementa paginación mediante los parámetros \texttt{pagina} y \texttt{tamano}, y acepta los parámetros opcionales \texttt{busqueda} (filtro de texto sobre nombre y correo), \texttt{rol} (filtro por identificador de rol) y \texttt{estado} (filtro por estado activo o inactivo), devolviendo en la respuesta tanto los registros de la página solicitada como el total de registros que satisfacen los filtros, necesario para que el frontend pueda calcular el número total de páginas. El endpoint \texttt{POST /api/usuarios} genera una contraseña temporal aleatoria para el nuevo usuario, computa su hash con bcrypt y envía al correo del nuevo usuario un mensaje con la contraseña en texto plano y la instrucción de cambiarla en el primer inicio de sesión. La decisión de generar la contraseña temporal en el servidor y enviarla por correo, en lugar de solicitar al administrador que la ingrese manualmente, responde al principio de mínima exposición de credenciales: la contraseña temporal existe únicamente durante el tiempo de tránsito del correo y el primer inicio de sesión del usuario. El endpoint \texttt{PATCH /api/usuarios/\{id\}/estado} implementa una desactivación lógica de la cuenta, actualizando únicamente el campo de estado sin eliminar ningún dato histórico del usuario, garantizando la conservación de la trazabilidad de sus acciones anteriores en el log de auditoría.

La infraestructura de autorización RBAC (tareas T5-02 a T5-04) se construyó sobre el sistema de autorización basado en políticas de ASP.NET Core. Se definió una política por rol mediante el método \texttt{AddPolicy} en la configuración de servicios del proyecto, asociando cada política al claim de rol correspondiente presente en el token JWT. Cada controlador y cada endpoint sensible de la API fue decorado con el atributo \texttt{[Authorize(Policy = "NombrePolitica")]} correspondiente, asegurando que el middleware de autorización de .NET 8 evalúe las credenciales del token en cada solicitud entrante y devuelva una respuesta HTTP 403 Forbidden ante cualquier intento de acceso no autorizado, independientemente de si dicho acceso se originó desde el frontend oficial o desde un cliente externo como Postman.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.88\textwidth]{02Figures/02Chapter/sprint2/codigo_rbac_backend.png}
    \caption{Configuración de las políticas de autorización RBAC en el backend .NET 8 durante el Sprint 2}
    \label{C2.figureCodigoRBAC}
\end{figure}

\subsection{Implementación de la Capa Frontend}
\label{subsec:sprint2_frontend}

El módulo de gestión de usuarios implementado en Angular durante el Sprint 2 constituye el componente de interfaz más complejo del proyecto en términos de interacciones y estados. El \texttt{UserManagementModule} (tarea T4-07) agrupa el componente de tabla de usuarios, el servicio de comunicación con la API y los componentes de formulario modal, organizados bajo un módulo con carga diferida (\textit{lazy loading}) que se activa únicamente cuando el usuario autenticado tiene el rol de Administrador.

El componente de tabla de usuarios implementa la integración con el endpoint paginado del backend, gestionando el estado de la página actual, el tamaño de página, los filtros activos y el indicador de carga durante las solicitudes. Los controles de búsqueda de texto utilizan un observable de Angular con un retardo de trescientos milisegundos (\textit{debounce time}) para evitar el envío de solicitudes al servidor en cada pulsación de tecla, optimizando el rendimiento de la interfaz. El formulario modal de creación y edición de usuario (tarea T4-08) reutiliza el mismo componente para ambas operaciones, diferenciando entre el modo de creación y el de edición mediante un parámetro de entrada (\textit{Input property}); en el modo de creación los campos están vacíos y son todos obligatorios, mientras que en el modo de edición los campos están prellenados con los datos actuales del usuario y solo el nombre puede ser modificado.

La directiva \texttt{*appHasRole} (tarea T5-05) extiende el sistema de plantillas de Angular para ocultar elementos de la interfaz que no corresponden al rol del usuario activo. Esta directiva lee el claim de rol del token JWT almacenado en el \texttt{AuthService} y evalúa si el rol del usuario activo coincide con cualquiera de los roles aceptados para el elemento decorado, aplicando la lógica de visualización de manera declarativa directamente en las plantillas HTML de los componentes. Las guardas de ruta por rol (tarea T5-06) implementan la misma lógica a nivel de navegación, protegiendo los módulos completos del router de Angular y devolviendo al usuario a la pantalla de acceso denegado si intenta acceder a un módulo para el cual no tiene autorización.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.85\textwidth]{02Figures/02Chapter/sprint2/panel_gestion_usuarios_implementado.png}
    \caption{Panel de gestión de usuarios implementado en Angular, con tabla de datos, búsqueda y controles de acción}
    \label{C2.figurePanelGestionUsuarios}
\end{figure}

\subsection{Pruebas Funcionales}
\label{subsec:sprint2_pruebas}

Al concluir las tareas de implementación del Sprint 2, se ejecutó el plan de pruebas funcionales correspondiente a las historias HU-04 y HU-05. Los casos de prueba cubrieron tanto los flujos principales de cada funcionalidad como los flujos alternativos de error y los escenarios de validación de permisos.

\begin{table}[H]
\centering
\caption{Resultados de las pruebas funcionales}
\label{tab:pruebas_sprint2}
\begin{tabular}{|c|c|p{4.5cm}|p{3cm}|c|}
\hline
\textbf{ID Caso} & \textbf{HU} & \textbf{Descripción del Caso} & \textbf{Resultado Esperado} & \textbf{Estado} \\ \hline
CP2-01 & HU-04 & Creación de usuario con datos válidos y correo institucional & Cuenta creada, correo con credenciales enviado al nuevo usuario & Aprobado \\ \hline
CP2-02 & HU-04 & Creación de usuario con correo ya registrado en el sistema & Error: 'El correo ya está registrado', sin crear la cuenta & Aprobado \\ \hline
CP2-03 & HU-04 & Creación de usuario con correo sin dominio @epn.edu.ec & Error de validación de formato de correo institucional & Aprobado \\ \hline
CP2-04 & HU-04 & Búsqueda de usuarios por nombre parcial & Lista filtrada mostrando solo usuarios que coinciden & Aprobado \\ \hline
CP2-05 & HU-04 & Filtrado de usuarios por rol 'Docente' & Lista filtrada mostrando únicamente docentes activos e inactivos & Aprobado \\ \hline
CP2-06 & HU-04 & Edición del nombre de un usuario existente & Datos actualizados correctamente en la base de datos & Aprobado \\ \hline
CP2-07 & HU-04 & Desactivación de una cuenta de usuario activa & Estado cambia a Inactivo; usuario no puede iniciar sesión & Aprobado \\ \hline
CP2-08 & HU-04 & Reactivación de una cuenta desactivada & Estado cambia a Activo; usuario puede iniciar sesión nuevamente & Aprobado \\ \hline
CP2-09 & HU-04 & Verificación del log de auditoría tras operaciones CRUD & Cada operación registra un evento en el log con los datos correctos & Aprobado \\ \hline
CP2-10 & HU-05 & Acceso al panel de administración como Administrador & Acceso concedido, panel de gestión de usuarios visible & Aprobado \\ \hline
CP2-11 & HU-05 & Acceso al panel de administración como Docente & Respuesta HTTP 403 Forbidden y redirección a pantalla de acceso denegado & Aprobado \\ \hline
CP2-12 & HU-05 & Acceso al panel de administración como Miembro CPGIC & Respuesta HTTP 403 Forbidden y redirección a pantalla de acceso denegado & Aprobado \\ \hline
CP2-13 & HU-05 & Cambio de rol de un usuario por el Administrador & Rol actualizado en la base de datos; cambio reflejado en el próximo login & Aprobado \\ \hline
CP2-14 & HU-05 & Acceso a endpoint de administración con token de rol Docente & Backend retorna HTTP 403 Forbidden ante la solicitud no autorizada & Aprobado \\ \hline
CP2-15 & HU-05 & Verificación de visibilidad de controles en la UI según el rol & Solo el Administrador ve los controles de gestión de usuarios & Aprobado \\ \hline
\end{tabular}
\end{table}

La totalidad de los quince casos de prueba ejecutados durante el Sprint 2 produjo el resultado esperado, declarando las historias HU-04 y HU-05 como completadas según la Definición de Hecho del proyecto.

\subsection{Sprint Review y Retrospectiva}
\label{subsec:sprint2_review}

La ceremonia de Sprint Review del Sprint 2 se realizó el veintitrés de mayo de dos mil veintiséis, con la participación del estudiante y el Product Owner. Durante la demostración, el Product Owner pudo verificar el flujo completo de creación de un nuevo usuario docente desde el panel de administración, observar el correo de bienvenida recibido con las credenciales temporales, iniciar sesión con la cuenta recién creada y confirmar que el menú de navegación mostraba únicamente las opciones correspondientes al rol de Docente. El Product Owner declaró el sprint como satisfactoriamente completado y formuló una única observación: solicitar que en el formulario de creación de usuarios se incluya un campo de selección del departamento o área al que pertenece el docente, para facilitar la organización futura del personal. Esta funcionalidad fue registrada como un nuevo ítem del Product Backlog con prioridad Could Have para ser evaluada en la planificación del Sprint 4. En la Sprint Retrospective, el equipo identificó como fortaleza del sprint la claridad que proveyó el prototipo de Figma elaborado al inicio del ciclo, que redujo significativamente el tiempo de toma de decisiones durante la implementación del módulo de gestión de usuarios. Como área de mejora se identificó la conveniencia de redactar los casos de prueba de autorización antes de iniciar la implementación, para utilizarlos como guía durante el desarrollo de las políticas RBAC en lugar de redactarlos al final del sprint.

% =========================================================================
% SPRINT 3: PERFIL DE USUARIO Y REGISTRO DE AUDITORÍA
% =========================================================================
\section{Sprint 3: Perfil de Usuario y Registro de Auditoría del Sistema}
\label{sec:sprint3}

El Sprint 3 abarca el período comprendido entre el veinticinco de mayo y el trece de junio de dos mil veintiséis. Tras los dos sprints de implementación anteriores, el sistema cuenta ya con una infraestructura de autenticación robusta y un sistema de gestión de usuarios con control de acceso diferenciado por roles. El Sprint 3 tiene como propósito consolidar la experiencia de los usuarios autenticados mediante la incorporación de dos funcionalidades complementarias: la gestión autónoma del perfil personal por parte de cada usuario (HU-06) y el registro de auditoría con trazabilidad completa de todas las acciones críticas del sistema (HU-07). Estas dos historias suman diez Puntos de Historia en total y representan el primer sprint en el que el Developer trabaja con historias de prioridad Should Have, lo que refleja el avance natural del proyecto desde el núcleo de seguridad hacia las funcionalidades de valor agregado. El Sprint Goal del ciclo es el siguiente: al finalizar el Sprint 3, cualquier usuario autenticado debe poder visualizar y actualizar sus datos de perfil y cambiar su contraseña de manera autónoma, y el administrador debe poder consultar el registro histórico de todas las acciones críticas ejecutadas en el sistema, con la capacidad de filtrar por tipo de evento, usuario y rango de fechas, y de exportar el registro en formato CSV.

\subsection{Sprint Planning}
\label{subsec:sprint3_planning}

El Sprint Planning del Sprint 3 tuvo lugar el veinticinco de mayo de dos mil veintiséis, inmediatamente después de la Sprint Retrospective del ciclo anterior. Las historias seleccionadas para este sprint son HU-06 (Visualización y Edición de Perfil, cinco Puntos de Historia) y HU-07 (Registro de Auditoría y Trazabilidad, cinco Puntos de Historia). Durante el Sprint Planning se revisaron y refinaron los criterios de aceptación de ambas historias, prestando especial atención a la política de privacidad de la HU-06 —que establece que ningún usuario puede modificar su correo electrónico ni su rol desde la pantalla de perfil— y a los requisitos de inmutabilidad del registro de auditoría establecidos en la HU-07. El Sprint Backlog resultante descompone las dos historias en doce tareas técnicas con una estimación total de veintidós horas de trabajo efectivo.

\begin{table}[H]
\centering
\caption{Sprint Backlog: Perfil de Usuario y Registro de Auditoría}
\label{tab:sprint3_backlog}
\begin{tabular}{|c|c|p{5.8cm}|p{2cm}|c|}
\hline
\textbf{ID Tarea} & \textbf{HU} & \textbf{Descripción de la Tarea} & \textbf{Capa} & \textbf{Est. (h)} \\ \hline
T6-01 & HU-06 & Diseño del prototipo de pantalla de perfil de usuario en Figma, incluyendo sección de datos personales, foto y cambio de contraseña. & UX/UI & 1h \\ \hline
T6-02 & HU-06 & Implementación del endpoint GET /api/usuarios/perfil para obtener los datos del usuario actualmente autenticado, extraídos del token JWT. & Backend & 1h \\ \hline
T6-03 & HU-06 & Implementación del endpoint PUT /api/usuarios/perfil para la actualización del nombre, apellido e información de contacto del usuario. & Backend & 2h \\ \hline
T6-04 & HU-06 & Implementación del endpoint POST /api/usuarios/perfil/foto con validación de formato (JPG, PNG) y tamaño máximo de dos megabytes. & Backend & 2h \\ \hline
T6-05 & HU-06 & Implementación del endpoint PUT /api/auth/cambiar-contrasena con verificación de la contraseña actual y validación de la nueva según la política definida. & Backend & 2h \\ \hline
T6-06 & HU-06 & Desarrollo del componente PerfilUsuarioComponent en Angular con formulario reactivo, carga de imagen y sección de cambio de contraseña. & Frontend & 3h \\ \hline
T6-07 & HU-06 & Pruebas funcionales del módulo de perfil: edición de datos, carga de foto inválida, cambio de contraseña correcta e incorrecta. & QA & 1h \\ \hline
T7-01 & HU-07 & Diseño de la tabla LogAuditoria en SQL Server con campos de tipo de acción, usuario, timestamp, IP del cliente y resultado, e índices para filtrado eficiente. & BD & 1h \\ \hline
T7-02 & HU-07 & Implementación del servicio AuditoriaService en .NET 8 con registro automático de eventos críticos mediante un middleware transversal. & Backend & 3h \\ \hline
T7-03 & HU-07 & Implementación del endpoint GET /api/auditoria con soporte de filtros por tipo de acción, identificador de usuario y rango de fechas, con paginación. & Backend & 2h \\ \hline
T7-04 & HU-07 & Implementación del endpoint GET /api/auditoria/exportar para la descarga del log filtrado en formato CSV. & Backend & 1h \\ \hline
T7-05 & HU-07 & Desarrollo del componente AuditoriaComponent en Angular con tabla de datos, controles de filtro y botón de exportación a CSV. & Frontend & 2h \\ \hline
\multicolumn{4}{|r|}{\textbf{Total de horas estimadas}} & \textbf{21h} \\ \hline
\end{tabular}
\end{table}

\subsection{Diseño de Prototipos}
\label{subsec:sprint3_prototipos}

Al igual que en el Sprint 2, el Sprint 3 inicia con el diseño del prototipo de alta fidelidad en Figma correspondiente a la pantalla de perfil de usuario (tarea T6-01), dado que esta pantalla no fue cubierta en los prototipos del Sprint 0. El prototipo fue diseñado con una estructura de dos paneles verticales: el panel izquierdo muestra la foto de perfil del usuario con un botón de cambio de imagen y el listado de datos básicos del perfil (nombre, correo, rol y fecha de registro), mientras que el panel derecho aloja el formulario de edición de nombre y apellido y la sección de cambio de contraseña con los campos de contraseña actual, nueva contraseña y confirmación. La disposición de la información en dos paneles responde al objetivo de que el usuario pueda visualizar su información actual mientras edita, reduciendo la probabilidad de errores de entrada. El prototipo fue revisado brevemente con el Product Owner durante el primer día del sprint, quien aprobó el diseño sin observaciones.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.85\textwidth]{02Figures/02Chapter/sprint3/prototipo_perfil_usuario.png}
    \caption{Prototipo de alta fidelidad de la pantalla de perfil de usuario diseñado en Figma durante el Sprint 3}
    \label{C2.figurePrototipoPerfil}
\end{figure}

\subsection{Implementación de la Capa de Base de Datos}
\label{subsec:sprint3_bd}

La tarea T7-01 del Sprint 3 completó el diseño e implementación de la tabla \texttt{LogAuditoria} en SQL Server, cuya estructura había sido esbozada en el Modelo Entidad-Relación del Sprint 0 pero no había sido materializada hasta este ciclo. La tabla fue creada con los campos definitivos: \texttt{AuditoriaId} (clave primaria autoincremental), \texttt{TipoAccion} (cadena de texto que identifica el evento, por ejemplo \texttt{LOGIN\_EXITOSO}, \texttt{USUARIO\_CREADO}, \texttt{ROL\_MODIFICADO}), \texttt{UsuarioId} (clave foránea hacia la tabla de usuarios, nullable para los intentos de acceso con correos no registrados), \texttt{CorreoSolicitante} (texto libre para registrar el correo ingresado en intentos de acceso fallidos), \texttt{Timestamp} (fecha y hora en UTC del evento), \texttt{IpCliente} (dirección IP del cliente que originó la solicitud) y \texttt{Resultado} (cadena que indica si la operación fue exitosa o el código de error correspondiente). La tabla fue protegida mediante una restricción de base de datos que impide las operaciones UPDATE y DELETE sobre sus registros, garantizando la inmutabilidad del log a nivel de motor de base de datos, independientemente de los controles de la capa de aplicación. Se crearon además dos índices no agrupados: uno sobre la columna \texttt{TipoAccion} y otro compuesto sobre \texttt{UsuarioId} y \texttt{Timestamp}, para optimizar las consultas de filtrado más frecuentes.

\subsection{Implementación de la Capa Backend — Sprint 3}
\label{subsec:sprint3_backend}

La implementación del backend del Sprint 3 se dividió en dos bloques funcionales paralelos que evolucionaron de manera independiente a lo largo del sprint, dado que no comparten dependencias de código entre sí.

El primer bloque corresponde a los endpoints de gestión de perfil (tareas T6-02 a T6-05). El endpoint \texttt{GET /api/usuarios/perfil} implementa una consulta optimizada que extrae el identificador del usuario directamente del claim \texttt{sub} del token JWT presentado en el encabezado de la solicitud, evitando la necesidad de que el cliente envíe el identificador en la URL y eliminando así el riesgo de que un usuario pueda solicitar el perfil de otro usuario manipulando el parámetro de ruta. El endpoint \texttt{PUT /api/usuarios/perfil} valida que los campos recibidos en el cuerpo de la solicitud no incluyan el correo electrónico ni el rol, rechazando la solicitud con un error HTTP 400 si cualquiera de estos campos sensibles está presente, como una capa adicional de defensa más allá del control que provee el formulario de frontend. El endpoint de carga de foto de perfil (tarea T6-04) recibe el archivo mediante una solicitud multipart/form-data, valida la extensión y el tipo MIME del archivo y el tamaño en bytes antes de almacenarlo, y retorna la URL pública de la imagen almacenada para que el frontend pueda actualizar la vista de perfil de manera inmediata. El endpoint de cambio de contraseña (tarea T6-05) exige la presentación de la contraseña actual como mecanismo de verificación de identidad, siguiendo el principio de que incluso un usuario ya autenticado debe demostrar que conoce la contraseña vigente antes de poder modificarla, protegiéndose así contra el escenario en que un atacante acceda a una sesión activa no cerrada.

El segundo bloque corresponde a la implementación del servicio de auditoría (tareas T7-02 a T7-04). El \texttt{AuditoriaService} fue diseñado como un servicio transversal que es inyectado en todos los controladores que ejecutan operaciones críticas. Su método principal, \texttt{RegistrarEvento}, recibe el tipo de acción, el identificador del usuario (si aplica), el correo solicitante y el resultado, obtiene la dirección IP del cliente desde el contexto HTTP de ASP.NET Core y persiste el registro en la tabla \texttt{LogAuditoria} de manera asíncrona, sin bloquear el flujo de respuesta de la operación principal. Esta implementación garantiza que el registro de auditoría no impacte en el tiempo de respuesta percibido por el usuario. El endpoint \texttt{GET /api/auditoria} construye dinámicamente la consulta SQL en función de los parámetros de filtro presentes en la solicitud, utilizando las capacidades de LINQ de Entity Framework Core para componer la consulta de manera segura contra inyecciones SQL. El endpoint de exportación (tarea T7-04) genera el archivo CSV directamente en memoria mediante la clase \texttt{StringBuilder} de .NET y lo devuelve al cliente como un stream de descarga con el encabezado \texttt{Content-Disposition: attachment} y el tipo de contenido \texttt{text/csv}.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.90\textwidth]{02Figures/02Chapter/sprint3/codigo_auditoria_service.png}
    \caption{Implementación del servicio AuditoriaService en .NET 8, mostrando el método de registro asíncrono de eventos}
    \label{C2.figureAuditoriaService}
\end{figure}

\subsection{Implementación de la Capa Frontend}
\label{subsec:sprint3_frontend}

El componente \texttt{PerfilUsuarioComponent} (tarea T6-06) implementa la pantalla de perfil siguiendo la estructura de dos paneles definida en el prototipo de Figma. El panel de información del perfil se alimenta del endpoint \texttt{GET /api/usuarios/perfil} al inicializarse el componente, y presenta los datos del usuario en modo de solo lectura. El control de carga de foto de perfil utiliza un elemento de entrada de archivo (\texttt{input[type=file]}) personalizado visualmente mediante CSS, que filtra por extensión en el selector del sistema operativo y realiza una validación adicional del tamaño del archivo en el frontend antes de enviarlo al servidor, informando al usuario de manera inmediata si el archivo seleccionado supera el límite de dos megabytes. El formulario de edición de datos personales utiliza el enfoque de formularios reactivos de Angular, y al recibir la confirmación del servidor de que la actualización fue exitosa, actualiza de manera optimista los datos mostrados en el panel de información sin necesidad de recargar la página. La sección de cambio de contraseña implementa un validador cruzado entre los campos de nueva contraseña y confirmación, que verifica que ambos valores sean idénticos antes de habilitar el botón de envío.

El componente \texttt{AuditoriaComponent} (tarea T7-05) presenta el log de auditoría en una tabla con paginación del lado del servidor, de modo que en ningún momento se transfiere la totalidad del historial al cliente. Los controles de filtro están implementados como un formulario reactivo compacto ubicado sobre la tabla, con un selector de tipo de acción poblado desde un enumerado de tipos conocidos, un campo de texto para buscar por correo del usuario y dos selectores de fecha para definir el rango temporal. El botón de exportación a CSV realiza la solicitud al endpoint de exportación con los mismos parámetros de filtro activos en la tabla, de modo que el archivo descargado refleja exactamente los datos que el administrador está visualizando en pantalla.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.88\textwidth]{02Figures/02Chapter/sprint3/pantalla_auditoria_implementada.png}
    \caption{Módulo de registro de auditoría implementado en Angular, con tabla de eventos, controles de filtro y opción de exportación a CSV}
    \label{C2.figurePantallaAuditoria}
\end{figure}

\subsection{Pruebas Funcionales}
\label{subsec:sprint3_pruebas}

Al finalizar las tareas de implementación del Sprint 3, se ejecutó el plan de pruebas funcionales de caja negra para las historias HU-06 y HU-07. Los casos de prueba cubrieron los flujos principales y los escenarios de borde más relevantes de cada funcionalidad.

\begin{table}[H]
\centering
\caption{Resultados de las pruebas funcionales}
\label{tab:pruebas_sprint3}
\begin{tabular}{|c|c|p{4.5cm}|p{3cm}|c|}
\hline
\textbf{ID Caso} & \textbf{HU} & \textbf{Descripción del Caso} & \textbf{Resultado Esperado} & \textbf{Estado} \\ \hline
CP3-01 & HU-06 & Edición del nombre y apellido con datos válidos & Perfil actualizado, confirmación visual al usuario & Aprobado \\ \hline
CP3-02 & HU-06 & Intento de modificar el correo desde la API directamente & Endpoint rechaza con HTTP 400: campo no modificable & Aprobado \\ \hline
CP3-03 & HU-06 & Carga de foto de perfil en formato JPG menor a 2 MB & Foto almacenada, vista de perfil actualizada & Aprobado \\ \hline
CP3-04 & HU-06 & Carga de foto en formato PDF no permitido & Error de validación: formato de archivo no aceptado & Aprobado \\ \hline
CP3-05 & HU-06 & Carga de foto JPG mayor a 2 MB & Error de validación: tamaño de archivo excede el límite & Aprobado \\ \hline
CP3-06 & HU-06 & Cambio de contraseña con contraseña actual correcta y nueva válida & Contraseña actualizada, confirmación al usuario & Aprobado \\ \hline
CP3-07 & HU-06 & Cambio de contraseña con contraseña actual incorrecta & Rechazo con mensaje de error: contraseña actual no válida & Aprobado \\ \hline
CP3-08 & HU-06 & Nueva contraseña sin cumplir la política de seguridad & Error de validación con descripción de la política requerida & Aprobado \\ \hline
CP3-09 & HU-06 & Nueva contraseña y confirmación no coinciden en el formulario & Botón de envío deshabilitado, indicador de error en campo & Aprobado \\ \hline
CP3-10 & HU-07 & Consulta del log de auditoría sin filtros activos & Lista paginada con los eventos más recientes en primer lugar & Aprobado \\ \hline
CP3-11 & HU-07 & Filtrado del log por tipo de acción LOGIN\_FALLIDO & Solo se muestran registros del tipo seleccionado & Aprobado \\ \hline
CP3-12 & HU-07 & Filtrado del log por rango de fechas válido & Solo se muestran registros dentro del rango especificado & Aprobado \\ \hline
CP3-13 & HU-07 & Intento de eliminar un registro del log desde la base de datos & Motor SQL rechaza la operación DELETE por restricción de tabla & Aprobado \\ \hline
CP3-14 & HU-07 & Exportación a CSV con filtros de tipo de acción activos & Archivo CSV descargado contiene solo los registros filtrados & Aprobado \\ \hline
CP3-15 & HU-07 & Acceso al módulo de auditoría como usuario Docente & Respuesta HTTP 403 Forbidden, redirección a pantalla de acceso denegado & Aprobado \\ \hline
\end{tabular}
\end{table}

La totalidad de los quince casos de prueba del Sprint 3 produjo el resultado esperado, permitiendo declarar las historias HU-06 y HU-07 como completadas según la Definición de Hecho del proyecto.

\subsection{Sprint Review y Retrospectiva}
\label{subsec:sprint3_review}

La ceremonia de Sprint Review del Sprint 3 se llevó a cabo el trece de junio de dos mil veintiséis. Durante la demostración, el Product Owner pudo interactuar directamente con la pantalla de perfil utilizando una cuenta de usuario de prueba: actualizó el nombre, cargó una fotografía y ejecutó el flujo de cambio de contraseña, verificando en cada paso que el comportamiento del sistema se ajustaba a los criterios de aceptación acordados. Posteriormente, el desarrollador demostró el módulo de auditoría desde la cuenta del administrador, filtrando los eventos generados durante la propia demostración —incluyendo el inicio de sesión, la actualización de perfil y el cambio de contraseña recién ejecutados— y descargando el CSV resultante. El Product Owner declaró el sprint como satisfactoriamente completado y no formuló observaciones adicionales al backlog. En la Sprint Retrospective, el equipo destacó como fortaleza la decisión de implementar el \texttt{AuditoriaService} como un servicio transversal inyectable, que permitió agregar el registro de eventos a los controladores existentes sin necesidad de modificar la lógica de negocio de cada uno de ellos. Como área de mejora, el equipo identificó la necesidad de planificar con mayor antelación la estrategia de almacenamiento de imágenes, dado que la elección del sistema de archivos del servidor como ubicación de almacenamiento durante este sprint podría requerir una migración a un servicio de almacenamiento externo en una etapa futura del proyecto.

% =========================================================================
% SPRINT 4: DASHBOARD ADMINISTRATIVO Y ASEGURAMIENTO DE CALIDAD INTEGRAL
% =========================================================================
\section{Sprint 4: Dashboard Administrativo y Aseguramiento de Calidad Integral}
\label{sec:sprint4}

El Sprint 4 abarca el período comprendido entre el quince de junio y el tres de julio de dos mil veintiséis, constituyendo el ciclo final del proyecto y el más heterogéneo en cuanto a la naturaleza de las actividades que agrupa. Este sprint tiene una doble misión: por un lado, implementar la historia de usuario HU-08 correspondiente al Dashboard de Administración, que aunque clasificada como Could Have representa una herramienta de supervisión estratégica de alto valor para el administrador del sistema; y por otro lado, ejecutar la fase integral de aseguramiento de la calidad del módulo completo, que comprende las pruebas de integración entre todas las capas del sistema, las pruebas de seguridad sobre los mecanismos de autenticación y autorización, y las pruebas de usabilidad con usuarios finales utilizando el cuestionario estandarizado System Usability Scale (SUS). El Sprint Goal del ciclo es el siguiente: al finalizar el Sprint 4, el administrador debe contar con un panel de control que le provea una visión centralizada del estado del sistema, y el módulo completo debe haber superado satisfactoriamente el plan de pruebas de integración, seguridad y usabilidad, con la documentación de resultados correspondiente que respalde la calidad del sistema entregado.

\subsection{Sprint Planning}
\label{subsec:sprint4_planning}

El Sprint Planning del Sprint 4 se realizó el quince de junio de dos mil veintiséis. Dada la doble naturaleza del sprint —desarrollo de la HU-08 y ejecución de la fase de QA—, la planificación requirió una distribución cuidadosa de la capacidad disponible. Se estimó que el desarrollo e integración del Dashboard (HU-08) consumiría aproximadamente diez horas de trabajo, mientras que la preparación y ejecución de las pruebas de integración, seguridad y usabilidad, junto con el análisis de resultados y la redacción de los informes correspondientes, requeriría las restantes horas del sprint. El Sprint Backlog resultante organiza las actividades del sprint en dos bloques secuenciales: primero el desarrollo del Dashboard (días uno al cinco del sprint), y luego la fase de QA integral (días seis al quince), de modo que las pruebas de integración y usabilidad se ejecuten sobre el sistema completamente implementado.

\begin{table}[H]
\centering
\caption{Sprint Backlog: Dashboard Administrativo y QA Integral}
\label{tab:sprint4_backlog}
\begin{tabular}{|c|c|p{5.8cm}|p{2cm}|c|}
\hline
\textbf{ID Tarea} & \textbf{HU} & \textbf{Descripción de la Tarea} & \textbf{Capa} & \textbf{Est. (h)} \\ \hline
T8-01 & HU-08 & Diseño del prototipo de pantalla del Dashboard Administrativo en Figma, incluyendo tarjetas de resumen y gráfica de actividad. & UX/UI & 1h \\ \hline
T8-02 & HU-08 & Implementación del endpoint GET /api/admin/dashboard con agregaciones de métricas de usuarios e intentos de acceso. & Backend & 3h \\ \hline
T8-03 & HU-08 & Desarrollo del componente DashboardComponent en Angular con tarjetas de métricas y gráfica de líneas mediante Chart.js. & Frontend & 4h \\ \hline
T8-04 & HU-08 & Pruebas funcionales del Dashboard con datos de prueba representativos de escenarios reales de operación. & QA & 2h \\ \hline
T9-01 & QA & Elaboración del plan de pruebas de integración del módulo completo: definición de escenarios de flujo end-to-end. & Documentación & 2h \\ \hline
T9-02 & QA & Ejecución de pruebas de integración: flujos completos de autenticación, gestión de usuarios y auditoría de extremo a extremo. & QA & 4h \\ \hline
T9-03 & QA & Ejecución de pruebas de seguridad: inyección SQL, manipulación de tokens JWT, accesos no autorizados y fuerza bruta. & QA & 4h \\ \hline
T9-04 & QA & Preparación del protocolo de pruebas de usabilidad: selección de participantes, definición de tareas y configuración del cuestionario SUS. & Documentación & 2h \\ \hline
T9-05 & QA & Ejecución de las sesiones de prueba de usabilidad con diez usuarios finales (cinco docentes y cinco miembros CPGIC). & QA & 4h \\ \hline
T9-06 & QA & Análisis estadístico de los resultados SUS, cálculo de la puntuación promedio y redacción del informe de usabilidad. & Documentación & 3h \\ \hline
T9-07 & QA & Corrección de los defectos identificados durante las fases de prueba y verificación de la corrección. & Backend/Frontend & 4h \\ \hline
T9-08 & QA & Redacción de la documentación técnica final: actualización del Swagger, manual de despliegue y guía de configuración. & Documentación & 3h \\ \hline
\multicolumn{4}{|r|}{\textbf{Total de horas estimadas}} & \textbf{36h} \\ \hline
\end{tabular}
\end{table}

\subsection{Implementación del Dashboard Administrativo}
\label{subsec:sprint4_dashboard}

El Dashboard Administrativo (HU-08) provee al administrador del sistema una vista consolidada del estado operativo del módulo de gestión de usuarios, accesible únicamente para usuarios con el rol de Administrador. El prototipo diseñado en Figma al inicio del sprint (tarea T8-01) establece una disposición de cuatro tarjetas de métricas en la parte superior de la pantalla, seguidas de una gráfica de líneas y dos tablas de actividad reciente en la parte inferior.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.88\textwidth]{02Figures/02Chapter/sprint4/prototipo_dashboard.png}
    \caption{Prototipo de alta fidelidad del Dashboard Administrativo diseñado en Figma durante el Sprint 4}
    \label{C2.figurePrototipoDashboard}
\end{figure}

El endpoint \texttt{GET /api/admin/dashboard} (tarea T8-02) ejecuta un conjunto de consultas de agregación sobre las tablas del módulo y devuelve en un único objeto JSON todas las métricas necesarias para poblar el dashboard, evitando que el frontend deba realizar múltiples solicitudes al servidor para componer la vista. Las métricas incluidas son: el número total de usuarios registrados, el número de usuarios activos e inactivos, la distribución de usuarios por rol, el número de inicios de sesión exitosos en los últimos siete días, el número de intentos fallidos de inicio de sesión en las últimas veinticuatro horas, y las cinco cuentas creadas más recientemente. Para optimizar el rendimiento de estas consultas, se evaluó la viabilidad de implementar un mecanismo de caché con una ventana de cinco minutos sobre los resultados del endpoint, dado que las métricas del dashboard no requieren precisión en tiempo real. Esta optimización fue implementada utilizando el servicio de caché en memoria (\texttt{IMemoryCache}) de ASP.NET Core.

El componente \texttt{DashboardComponent} en Angular (tarea T8-03) utiliza la biblioteca Chart.js para renderizar la gráfica de líneas de actividad de los últimos siete días, con dos series de datos: una para los inicios de sesión exitosos y otra para los intentos fallidos. Las tarjetas de métricas implementan una animación de contador al cargar la página, que incrementa el valor desde cero hasta la cifra real en el transcurso de un segundo, para crear una impresión visual de dinamismo. Las dos tablas inferiores muestran, respectivamente, los diez eventos de auditoría más recientes del sistema y las cinco cuentas creadas más recientemente, con enlaces directos al módulo de gestión de usuarios y al log de auditoría completo para facilitar la navegación.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.90\textwidth]{02Figures/02Chapter/sprint4/dashboard_implementado.png}
    \caption{Dashboard Administrativo implementado en Angular con tarjetas de métricas, gráfica de actividad y tablas de eventos recientes}
    \label{C2.figureDashboardImplementado}
\end{figure}

\subsection{Pruebas de Integración del Módulo Completo}
\label{subsec:sprint4_pruebas_integracion}

Una vez completado el desarrollo del Dashboard, el Sprint 4 avanzó hacia la fase de aseguramiento de la calidad integral del módulo, iniciando con la ejecución del plan de pruebas de integración (tarea T9-02). Las pruebas de integración verifican que las historias de usuario implementadas en los cuatro sprints de desarrollo anteriores funcionen de manera coordinada cuando se ejecutan como flujos de trabajo completos, en lugar de ser evaluadas de manera aislada como en las pruebas funcionales de cada sprint. Los escenarios de prueba de integración definidos cubren los siguientes flujos de extremo a extremo: el flujo de incorporación de un nuevo usuario, que abarca la creación de la cuenta por el administrador, el envío del correo de bienvenida, el primer inicio de sesión con la contraseña temporal y el establecimiento de una nueva contraseña; el flujo de recuperación de acceso, que cubre la solicitud de restablecimiento de contraseña, la recepción del correo, el uso del enlace y la verificación del nuevo acceso; el flujo de escalamiento de privilegios, que verifica que el cambio de rol asignado por el administrador se refleja correctamente en las opciones disponibles para el usuario al iniciar sesión con su nuevo rol; y el flujo de revocación de acceso, que comprueba que la desactivación de una cuenta impide el inicio de sesión del usuario afectado y que el evento queda registrado correctamente en el log de auditoría. Todos los escenarios de integración fueron ejecutados exitosamente, sin que se identificaran defectos de integración entre las distintas capas del sistema.

\subsection{Pruebas de Seguridad}
\label{subsec:sprint4_pruebas_seguridad}

Las pruebas de seguridad (tarea T9-03) se ejecutaron contra el entorno de pruebas del sistema, evaluando la resistencia del módulo ante los vectores de ataque más comunes en sistemas de autenticación web. Los escenarios evaluados y sus resultados se presentan en la Tabla \ref{tab:pruebas_seguridad}.

\begin{table}[H]
\centering
\caption{Resultados de las pruebas de seguridad del módulo de autenticación}
\label{tab:pruebas_seguridad}
\begin{tabular}{|c|p{4cm}|p{4cm}|c|}
\hline
\textbf{ID} & \textbf{Vector de Ataque} & \textbf{Resultado Esperado} & \textbf{Estado} \\ \hline
CS-01 & Inyección SQL en el campo de correo del formulario de login & Consulta parametrizada rechaza el intento; sin impacto en BD & Aprobado \\ \hline
CS-02 & Manipulación del payload del token JWT (cambio del claim de rol) & Backend rechaza el token con firma inválida (HTTP 401) & Aprobado \\ \hline
CS-03 & Uso de token expirado en solicitudes autenticadas & Backend rechaza el token con HTTP 401 y mensaje de sesión expirada & Aprobado \\ \hline
CS-04 & Reutilización de token tras cierre de sesión (blacklist) & Backend rechaza el token con HTTP 401 por estar en la lista negra & Aprobado \\ \hline
CS-05 & Ataque de fuerza bruta: más de 5 intentos de login consecutivos & Sistema bloquea la cuenta tras el quinto intento fallido & Aprobado \\ \hline
CS-06 & Acceso directo a endpoint de administración sin token & Backend rechaza con HTTP 401 Unauthorized & Aprobado \\ \hline
CS-07 & Acceso a endpoint de administración con token de rol Docente & Backend rechaza con HTTP 403 Forbidden & Aprobado \\ \hline
CS-08 & Reutilización del token de restablecimiento de contraseña & Sistema rechaza el segundo uso por token ya utilizado & Aprobado \\ \hline
CS-09 & Uso del token de restablecimiento expirado (más de 24 horas) & Sistema rechaza el token con mensaje de enlace expirado & Aprobado \\ \hline
CS-10 & Inyección de scripts en campos de formulario de creación de usuarios & Datos almacenados como texto plano; sin ejecución al renderizar & Aprobado \\ \hline
\end{tabular}
\end{table}

La totalidad de los diez escenarios de seguridad evaluados produjo el resultado esperado, evidenciando que el módulo resiste de manera satisfactoria los vectores de ataque más frecuentes en sistemas de autenticación institucional.

\subsection{Pruebas de Usabilidad con el System Usability Scale}
\label{subsec:sprint4_sus}

La evaluación de usabilidad del módulo se llevó a cabo mediante el cuestionario estandarizado System Usability Scale (SUS), propuesto originalmente por Brooke \cite{brooke1996sus}. El SUS consiste en diez ítems valorados en una escala Likert de cinco puntos (desde totalmente en desacuerdo hasta totalmente de acuerdo), cuya puntuación combinada produce una calificación global en el rango de cero a cien. Puntuaciones iguales o superiores a sesenta y ocho se consideran aceptables, las iguales o superiores a setenta y siete se clasifican como buenas, y las que superan los ochenta y cinco puntos se consideran excelentes.

Para la ejecución de las pruebas de usabilidad (tarea T9-05), se convocó a diez participantes voluntarios: cinco docentes de la Facultad de Ingeniería de Sistemas y cinco miembros o expresidentes de la CPGIC, seleccionados con el objetivo de que el grupo representara el perfil real de usuarios del sistema. Cada sesión de prueba tuvo una duración de treinta minutos, divididos en cinco minutos de introducción al propósito de la prueba, veinte minutos de ejecución de las tareas encomendadas y cinco minutos para la cumplimentación del cuestionario SUS. Las tareas encomendadas a cada participante fueron: iniciar sesión en el sistema con las credenciales provistas, navegar por los módulos disponibles para su rol, actualizar su nombre en la pantalla de perfil, cambiar su contraseña y cerrar la sesión de manera controlada. Un observador registró el comportamiento del participante durante la ejecución de las tareas, anotando los momentos de duda, los errores cometidos y los comentarios verbalizados de manera espontánea.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.88\textwidth]{02Figures/02Chapter/sprint4/sesion_prueba_usabilidad.png}
    \caption{Sesión de prueba de usabilidad con participante del grupo de docentes FIS, Sprint 4}
    \label{C2.figureSesionUsabilidad}
\end{figure}

Los resultados individuales del cuestionario SUS y la puntuación final de cada participante se presentan en la Tabla \ref{tab:resultados_sus}.

\begin{table}[H]
\centering
\caption{Puntuaciones individuales y promedio del cuestionario SUS aplicado al módulo}
\label{tab:resultados_sus}
\begin{tabular}{|c|l|c|c|}
\hline
\textbf{Participante} & \textbf{Perfil} & \textbf{Puntuación SUS} & \textbf{Clasificación} \\ \hline
P01 & Docente FIS & 82.5 & Buena \\ \hline
P02 & Docente FIS & 77.5 & Buena \\ \hline
P03 & Docente FIS & 85.0 & Excelente \\ \hline
P04 & Docente FIS & 80.0 & Buena \\ \hline
P05 & Docente FIS & 75.0 & Buena \\ \hline
P06 & Miembro CPGIC & 72.5 & Aceptable \\ \hline
P07 & Miembro CPGIC & 82.5 & Buena \\ \hline
P08 & Miembro CPGIC & 77.5 & Buena \\ \hline
P09 & Miembro CPGIC & 80.0 & Buena \\ \hline
P10 & Miembro CPGIC & 75.0 & Buena \\ \hline
\multicolumn{2}{|c|}{\textbf{Promedio Global}} & \textbf{78.75} & \textbf{Buena} \\ \hline
\end{tabular}
\end{table}

La puntuación SUS promedio obtenida por el módulo fue de setenta y ocho punto setenta y cinco sobre cien, clasificada como \textit{buena} según la escala de adjetivos propuesta por Bangor, Kortum y Miller \cite{bangor2009sus}. Este resultado supera el umbral mínimo de aceptabilidad de sesenta y ocho puntos y excede también el objetivo de setenta y siete puntos establecido en el plan de proyecto. Los comentarios cualitativos más frecuentes durante las sesiones de prueba señalaron como fortalezas la claridad del formulario de inicio de sesión y la lógica del menú de navegación diferenciado por rol. El único comentario recurrente de mejora indicó que el formulario de cambio de contraseña podría beneficiarse de un indicador visual de fortaleza de la contraseña mientras el usuario escribe, funcionalidad que fue registrada en el Product Backlog como ítem de mejora futura.

\begin{figure}[htbp]
    \centering
    \includegraphics[width=0.85\textwidth]{02Figures/02Chapter/sprint4/grafica_resultados_sus.png}
    \caption{Distribución de las puntuaciones SUS individuales de los diez participantes, con la línea de referencia del promedio global (78.75) y el umbral de aceptabilidad (68)}
    \label{C2.figureResultadosSUS}
\end{figure}

\subsection{Sprint Review y Retrospectiva}
\label{subsec:sprint4_review}

La ceremonia de Sprint Review del Sprint 4 se realizó el tres de julio de dos mil veintiséis, marcando la entrega formal del Módulo de Gestión de Usuarios y Autenticación al Product Owner. Durante la sesión, el desarrollador presentó de manera integral el sistema completo: el flujo de autenticación con JWT, el panel de gestión de usuarios con el sistema RBAC, los módulos de perfil y auditoría, el Dashboard Administrativo y los informes de las pruebas de integración, seguridad y usabilidad. El Product Owner revisó el informe de resultados SUS y declaró satisfacción con la puntuación obtenida, reconociendo que el módulo cumple con los objetivos de seguridad y usabilidad establecidos en la propuesta del Trabajo de Integración Curricular. La Sprint Retrospective final reflexionó sobre el proyecto en su conjunto, identificando como principal fortaleza metodológica la decisión de invertir el Sprint 0 íntegramente en diseño y análisis, que dotó al desarrollador de una hoja de ruta clara y redujo las ambigüedades técnicas durante los sprints de implementación. Como lección aprendida de mayor relevancia, el equipo identificó la conveniencia de redactar los casos de prueba de cada historia de usuario de manera concurrente con la implementación, para que sirvan como criterio de completitud durante el desarrollo y no únicamente como instrumento de verificación al final del sprint.
