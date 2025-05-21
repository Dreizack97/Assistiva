# Assistiva 📘 **Manual Técnico - Assistiva**

**Versión:** 1.1
**Fecha:** 2025-05-19
**Autor:** Equipo de Desarrollo JJ-Software

---

## 📑 Índice

1. [Introducción](#introduccion)
2. [Requisitos del Sistema](#requisitos-del-sistema)
3. [Instalación y Configuración](#instalacion-y-configuracion)
4. [Arquitectura del Sistema](#arquitectura-del-sistema)
5. [Base de Datos](#base-de-datos)
6. [Estructura del Código](#estructura-del-codigo)
7. [Controladores MVC](#controladores-mvc)
8. [Seguridad](#seguridad)
9. [Pruebas](#pruebas)
10. [Despliegue](#despliegue)
11. [Mantenimiento y Soporte](#mantenimiento-y-soporte)
12. [Glosario](#glosario)
13. [Anexos](#anexos)

---

<a id="introduccion"></a>
## 1. Introducción

El **Sistema Assistiva** es una plataforma web desarrollada en **ASP .NET MVC** dirigida a la enseñanza de matemáticas para personas con discapacidad. Su objetivo principal es ofrecer recursos didácticos adaptados (fórmulas, ejercicios, referencias) que faciliten el aprendizaje en función de las necesidades específicas de cada discapacidad.

**Audiencia objetivo:**

* Desarrolladores encargados de mantenimiento y evolución
* Administradores de sistemas e integradores
* Personal de soporte y capacitación

---

<a id="requisitos-del-sistema"></a>
## 2. Requisitos del Sistema

### Alojamiento de la plataforma web (Recomendados)

### Hardware

* CPU: Intel® Xeon® Silver 4514Y
* RAM: 32 GB mínimo
* Espacio en disco: 40 GB disponibles

### Software

* [Microsoft SQL Server Express 2022](https://www.microsoft.com/es-mx/sql-server/sql-server-downloads)
* [Microsoft SQL Server Manager Studio](https://learn.microsoft.com/es-es/ssms/download-sql-server-management-studio-ssms)
* [ASP .NET Core 9.0 Runtime - Windows Hosting Bundle](https://dotnet.microsoft.com/es-es/download/dotnet/9.0)
* IIS 10

### Requisitos para desarrolladores (mínimos)

### Hardware

* CPU: Intel Core i5 10ma generación
* RAM: 32 GB mínimo
* Espacio en disco: 60 GB

### Software

* Microsoft SQL Server Express 2022
* Microsoft SQL Server Manager Studio
* Microsoft Visual Studio ó Microsoft Visual Studio Code
* .NET 9.0 SDK
* Git

### Dependencias NuGet

* Microsoft.EntityFrameworkCore.SqlServer 9.0.3 (Entity)
* Microsoft.EntityFrameworkCore.Tools 9.0.3 (Entity)
* MailKit 4.11.0 (BLL)
* AutoMapper 14.0.0 (AppUI)
* Moq 4.20.72 (TestProject)
* NUnit 4.2.2 (TestProject)
* Selenium.WebDriver 4.32.0 (TestProject)

### Requisitos para usuario (mínimos)

### Hardware

* CPU: Intel Core i3 8va generación
* RAM: 8 GB
* Espacio en disco: 2 GB

### Software

* Navegador de internet (Google Chrome, Edge, Safari, Mozilla Firefox)

---

<a id="instalación-y-configuracion"></a>
## 3. Instalación y Configuración

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/Dreizack97/Assistiva.git
   ```
2. Abrir la solución en Visual Studio.
3. Restaurar paquetes NuGet:

   ```powershell
   dotnet restore
   ```
4. Ejecutar script de base de datos:

   ```sql
   CREATE DATABASE Assistiva
   GO

   USE Assistiva
   GO

   CREATE TABLE Roles...
   ```
5. Configurar la cadena de conexión en *appsettings.json*:

   ```json
    "ConnectionStrings": {
        "SQLString": "Server = localhost\\SQLEXPRESS; DataBase = Assistiva; Integrated Security = True; Encrypt = False"
    }
   ```
---

<a id="arquitectura-del-sistema"></a>
## 4. Arquitectura del Sistema

El sistema sigue una **arquitectura en capas** para separar responsabilidades y facilitar pruebas y mantenimiento:

* **AppUI:** Presentación (MVC, Razor Views, recursos estáticos).
* **BLL (Business Logic Layer):** Lógica de negocio y validaciones.
* **DAO (Data Access Objects):** Interfaces y repositorios genéricos.
* **DAL (Data Access Layer):** Implementación concreta de acceso a datos (EF Core).
* **Entity:** Definición de modelos y entidades del dominio.
* **IoC (Inversión de Control):** Configuración de dependencias y contenedor de inyección.

Diagrama simplificado:

```plaintext
AppUI ↔ BLL ↔ DAO ↔ DAL ↔ SQL Server
         ↑
       Entity
         ↑
        IoC
```

---

<a id="base-de-datos"></a>
## 5. Base de Datos

Construida para **Microsoft SQL Server**, la base de datos `Assistiva` incluye tablas clave para gestionar roles, usuarios, estudiantes, discapacidades, materias y estructuras de aulas.

### Tablas Principales

* **Roles**: Catálogo de roles del sistema (Administrador, Director, Docente, Auxiliar, Estudiante).
* **Users**: Usuarios del sistema con credenciales seguras (hash, salt), rol y datos de recuperación.
* **Disabilities**: Tipos de discapacidad soportados (Auditiva, Motora, Visual) con descripción.
* **Students**: Datos detallados de alumnos (vinculados a Users).
* **StudentDisabilities**: Asociación n\:m entre estudiantes y discapacidades.
* **Subjects**: Materias de matemáticas (código, nombre, descripción).
* **Formulas**: Fórmulas asociadas a cada materia (contenido en LaTeX).
* **Classrooms**: Aulas virtuales enlazadas a un docente.
* **ClassroomStudents**: Inscripción de alumnos en aulas.
* **ClassroomSubjects**: Asignación de materias a aulas.

### Diagrama entidad - relación

![Diagrama entidad - relación](https://raw.githubusercontent.com/Dreizack97/Assistiva/refs/heads/main/diagram-er.png)

> Consulte el script adjunto (`Assistiva.sql`) para la creación completa de esquemas, restricciones y datos iniciales.

---

<a id="estructura-del-codigo"></a>
## 6. Estructura del Código

```plaintext
/Assistiva
│
├── AppUI/                    # Aplicación MVC (Controllers, Views, wwwroot)
│   ├── appsettings.json      # Configuración de cadena de conexión
├── BLL/                      # Lógica de negocio (servicios, validaciones)
├── DAO/                      # Interfaces de repositorio genérico
├── DAL/                      # Implementaciones EF Core de repositorios
├── Entity/                   # Entidades y DTOs
├── IoC/                      # Configuración de inyección de dependencias
└── Assistiva.sln             # Solución Visual Studio
```

---

<a id="controladores-mvc"></a>
### 7. Controladores MVC

El sistema utiliza controladores ASP.NET MVC para manejar la lógica de enrutamiento y vistas. Cada controlador está asociado a una funcionalidad específica:

#### Ejemplos de Controladores

* StudentsController: Gestiona creación, edición y listado de estudiantes.
* SubjectsController: Administra materias y visualización de fórmulas.
* ClassroomsController: Maneja la asignación de aulas y estudiantes.

#### Vistas

Se usan Razor Views (.cshtml) para renderizar el contenido HTML.

Estructura basada en layout y vistas parciales para modularidad.

### Ejemplo:

#### Vista

```html
   <form method="post" asp-area="School" asp-controller="Formulas" asp-action="Upsert">
      <input asp-for="@Model.FormulaId" type="hidden" />
      <input asp-for="@Model.SubjectId" value="@ViewBag.SubjectId" type="hidden" />
      <div class="card-body">
         <div class="row gx-3 mb-3">
            <div class="col-sm-6">
               <label asp-for="@Model.Name" class="small mb-1"></label>
               <input asp-for="@Model.Name" class="form-control form-control-sm" placeholder="Nombre de la formula" />
               <span asp-validation-for="@Model.Name" class="small text-danger"></span>
            </div>
            <div class="col-sm-6">
               <label asp-for="@Model.Content" class="small mb-1"></label>
               <input asp-for="@Model.Content" class="form-control form-control-sm" placeholder="Contenido de la formula" oninput="renderPreview()" />
            </div>
         </div>
         <div class="row gx-3 mb-3">
            <div class="col-sm-12">
               <label asp-for="@Model.Description" class="small mb-1"></label>
               <textarea asp-for="@Model.Description" class="form-control form-control-sm" rows="5" placeholder="Descripción"></textarea>
            </div>
         </div>
      </div>
      <div class="card-footer d-flex justify-content-end">
         <button type="submit" class="btn btn-sm btn-success me-2">Agregar fórmula</button>
         <a class="btn btn-sm btn-primary" asp-area="School" asp-controller="Formulas" asp-action="Index" asp-route-SubjectId="@ViewBag.SubjectId">Regresar</a>
      </div>
   </form>
```
#### Controlador

```csharp
   [HttpPost]
   public async Task<IActionResult> Upsert(FormulaModel formula)
   {
      if (ModelState.IsValid)
      {
         try
         {
            await _formulaService.CreateAsync(_mapper.Map<Formula>(formula));
            TempData["success"] = "Formula registrada exitosamente.";

            return Redirect($"/School/Subjects/{formula.SubjectId}/Formulas");
         }
         catch (Exception ex)
         {
            TempData["info"] = ex.Message;
         }
      }

      ViewBag.SubjectId = formula.SubjectId;
      return View(formula);
   }
```

#### Negocio

```csharp
   public async Task<Formula> CreateAsync(Formula formula)
   {
      Formula? oFormula = await _repository.GetByFilterAsync(f => f.Name == formula.Name && f.SubjectId == formula.SubjectId);

      if (oFormula != null)
         throw new TaskCanceledException("Ya existe una fórmula con el mismo nombre para la misma asignatura.");

      Formula _formula = await _repository.AddAsync(formula);

      if (_formula.FormulaId == 0)
         throw new TaskCanceledException("Ocurrió un error al intentar registrar la fórmula.");

      return _formula;
   }
```

#### Acceso a datos

```csharp
   public async Task<TEntity> AddAsync(TEntity entity)
   {
      ArgumentNullException.ThrowIfNull(entity, nameof(entity));

      try
      {
         await _dbContext.Set<TEntity>().AddAsync(entity);
         await _dbContext.SaveChangesAsync();
         return entity;
      }
      catch (DbUpdateException)
      {
         throw;
      }
   }
```

> Nota: No se implementan servicios API REST. Toda la interacción se realiza vía formularios y vistas MVC tradicionales.

---

<a id="seguridad"></a>
## 8. Seguridad

* **Autenticación:** ASP.NET Core Identity.
* **Autorización:** Roles (`Admin`, `Docente`, `Estudiante`).
* **Protección:** Validaciones en servidor, antiforgery tokens, sanitización de entradas contra XSS.

---

<a id="pruebas"></a>
## 9. Pruebas

El sistema Assistiva ha sido sometido a pruebas automáticas unitarias y de integración, así como pruebas funcionales de interfaz utilizando Selenium.

### Pruebas Unitarias (xUnit + Moq)

Ubicadas en el proyecto TestProject, las pruebas se centran en la clase UserService, validando la lógica de negocio relacionada con usuarios:

* **CreateAsync_ValidUser_ReturnsCreatedUser:** Verifica la creación de un usuario válido y el envío de correo.

* **GetByIdAsync_ExistingUser_ReturnsUser:** Obtiene correctamente un usuario existente.

* **GetByIdAsync_NonExistingUser_ReturnsNull:** Retorna null si el usuario no existe.

* **GetAllAsync_ReturnsListOfUsers:** Retorna una lista de usuarios del repositorio.

### Pruebas Funcionales (Selenium)

Pruebas de interfaz realizadas con SafariDriver:

* **UserCanSignIn_ValidCredentials:** Simula el inicio de sesión de un usuario con credenciales válidas y verifica la carga de la pantalla de bienvenida.

* **UserCanViewFormula:** Verifica que un estudiante autenticado puede acceder a la sección de materias y visualizar una fórmula específica.

Estas pruebas aseguran la funcionalidad básica del sistema, validando tanto la lógica de negocio como la experiencia del usuario final.

---

<a id="despliegue"></a>
## 10. Despliegue

### Entorno de Desarrollo

* Ejecutar el proyecto mediante `dotnet run` o IIS Express desde Visual Studio.

### Entorno de Producción

**Pasos para desplegar en un servidor Windows con IIS:**

1. **Publicación del proyecto:**

   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. **Configuración de IIS:**

   * Crear un nuevo *sitio web* en IIS.
   * Apuntar el directorio físico al contenido de la carpeta `./publish`.
   * Asegurarse de tener instalado el *Hosting Bundle* de .NET 9.
   * Configurar el *Application Pool* con .NET CLR `No Managed Code` y en modo `Integrated`.

3. **Cadena de conexión:**

   * Editar `appsettings.Production.json` para establecer la cadena a SQL Server en producción.

4. **Base de datos:**

   * Ejecutar el script `Assistiva.sql` utilizando Sql Server Management Studio.

5. **Seguridad:**

   * Configurar HTTPS con un certificado SSL válido.
   * Abrir el puerto necesario (ej. 443) en el firewall del servidor.
   * Restringir accesos por IP o redes si aplica.

6. **Supervisión y logs:**

   * Habilitar logs de errores en `appsettings.json`.
   * Instalar herramientas de monitoreo como ELMAH, Application Insights o similar.

7. **Consideraciones de CDN / Recursos:**

   * Opcionalmente, mover recursos estáticos a un CDN.
   * Minificar y comprimir scripts y hojas de estilo.


---

<a id="mantenimiento-y-soporte"></a>
## 11. Mantenimiento y Soporte

* Repositorio en GitHub: ramas *main* (producción) y *feature* (desarrollo).
* Issues y Pull Requests para nuevas funcionalidades.
* Contacto: [soporte@assistiva.com](mailto:soporte@assistiva.com)

---

<a id="glosario"></a>
## 12. Glosario

| Término                          | Definición                                                                                                                                       |
|----------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| **ASP.NET MVC 9**                | Framework de desarrollo web de Microsoft basado en el patrón Modelo-Vista-Controlador. Versión 9.                                               |
| **Arquitectura en Capas**        | Estilo de diseño que divide la aplicación en capas con responsabilidades específicas: UI, lógica de negocio, acceso a datos, entre otras.       |
| **AppUI**                        | Capa de presentación responsable de la interacción visual con el usuario.                                                                       |
| **BLL (Business Logic Layer)**   | Capa de lógica de negocio que centraliza las reglas, procesos y validaciones de la aplicación.                                                  |
| **DAO (Data Access Object)**     | Patrón que aísla la lógica de acceso a datos del resto de la aplicación.                                                                        |
| **DAL (Data Access Layer)**      | Capa que ejecuta directamente las operaciones sobre la base de datos.                                                                           |
| **Entity**                       | Proyecto que contiene las clases que representan las entidades de la base de datos.                                                             |
| **IoC (Inversión de Control)**   | Principio que permite desacoplar dependencias mediante inyección, promoviendo mantenibilidad y testeo.                                         |
| **Microsoft SQL Server**         | Sistema de gestión de bases de datos relacionales utilizado en el proyecto.                                                                     |
| **Modelo Vista Controlador (MVC)** | Patrón de diseño que separa la lógica de presentación, control y datos.                                                                         |
| **Despliegue**                   | Proceso de publicación y configuración de la aplicación en un entorno de producción.                                                            |
| **IIS (Internet Information Services)** | Servidor web de Microsoft que aloja y ejecuta aplicaciones .NET en entornos Windows.                                               |
| **HTTPS**                        | Protocolo seguro de transferencia de datos entre cliente y servidor mediante cifrado.                                                           |
| **Entity Framework (EF)**        | Framework de mapeo objeto-relacional (ORM) utilizado para interactuar con la base de datos usando clases.                                      |
| **Controlador (Controller)**     | Componente que gestiona las solicitudes del usuario y coordina las respuestas a través de modelos y vistas.                                     |
| **Vista (View)**                 | Componente visual de la interfaz de usuario que muestra datos y permite interacción.                                                            |
| **Modelo (Model)**               | Representación de los datos de la aplicación, generalmente ligada a las entidades de base de datos.                                             |
| **Inclusión Educativa**          | Práctica de integrar a personas con discapacidad en entornos de aprendizaje accesibles y personalizados.                                       |
| **Script SQL**                   | Conjunto de instrucciones escritas en lenguaje SQL utilizadas para definir o manipular una base de datos.                                       |

---

<a id="anexos"></a>
## 13. Anexos

* **Script Base de Datos:** `Assistiva.sql`
* **Diagrama ER:** `diagrama-er.png`
* **Manual técnico:** `README.md`