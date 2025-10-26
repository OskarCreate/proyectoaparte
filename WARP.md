# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

This is an ASP.NET Core 9.0 MVC web application for managing employee medical leave requests (subsidios) with PostgreSQL database backend. The system handles different types of leave (accidents, maternity, paternity, illness, family illness, and death) with workflow for submission, validation by ESSALUD, and approval by supervisors ("Jefe" role).

## Technology Stack

- **Framework**: ASP.NET Core 9.0 MVC
- **Database**: PostgreSQL (via Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4)
- **Authentication**: ASP.NET Core Identity with roles ("Jefe", "Trabajador")
- **ORM**: Entity Framework Core 9.0.6
- **Session Management**: Distributed Memory Cache with 30-minute timeout

## Essential Commands

### Build and Run
```powershell
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run

# Run with watch (auto-reload on changes)
dotnet watch run
```

### Database Management
```powershell
# Add new migration
dotnet ef migrations add <MigrationName>

# Update database to latest migration
dotnet ef database update

# Remove last migration (if not applied)
dotnet ef migrations remove

# View database connection
# Check appsettings.json ConnectionStrings:DefaultConnection
```

### Docker
```powershell
# Build Docker image
docker build -t proyectoingsoft -f DockerFile .

# Run container (requires PORT environment variable)
docker run -e PORT=8080 -p 8080:8080 proyectoingsoft
```

## Architecture

### Database Context
- **ApplicationDbContext** (Data/ApplicationDbContext.cs): Central EF Core context extending IdentityDbContext
- Uses PostgreSQL with custom configurations for array columns and timestamp handling
- Seeds initial TipoDescanso data (6 types of leave)

### Key Domain Models (Models/)
- **User**: Custom user table (T_Usuarios) separate from Identity, includes employee info (DNI, cargo, razón social)
- **Descanso**: Central entity linking users to leave requests with three status fields:
  - EstadoESSALUD: "En Proceso" | "Válido" | "No válido"
  - EstadoSubsidioA: "Descanso Activo" (employee view)
  - EstadoSubsidioJ: "Pendiente" (supervisor view)
- **TipoDescanso**: Leave types (Enfermedad, Maternidad, Paternidad, etc.)
- **Specific leave types**: Accidente, Maternidad, Paternidad, Enfermedad, EnfermedadFam, Fallecimiento
- **DocumentoMedico**: Medical documents with PDF storage (byte[])
- **Notification**: User notifications with state tracking and document attachments
- **NotificacionSimulada**: Simulated notifications for testing
- **CodigoSocial**: Social security codes linked to users

### Controllers Architecture
Controllers follow feature-based organization:

- **Leave Type Controllers**: AccidenteController, MaternidadController, PaternidadController, EnfermedadController, EnfermedadFamController, FallecimientoController
  - Handle form submission for each leave type
  - Create Descanso records linking to specific leave entity
  
- **ListaController**: Main workflow controller for ESSALUD and supervisor roles
  - Index(): General list of all requests
  - DescansosProlongados(): Filter leaves > 30 days
  - SubsidiosJefe(): Supervisor view for approval
  - SupervisionSubsidios(): Detailed supervision with filtering
  - ValidarSubsidioA/ValidarSubsidioJ: Update estado fields
  - EnviarObservacion(): Create notifications for workers

- **SistemaSolicitudController**: Request management system
  - ListaSolicitudes(): Splits view into "Pendientes" and "Procesadas" tabs
  - ActualizarEstadosESSALUD(): Batch update endpoint for ESSALUD validation

- **NotificationController**: Display user notifications filtered by logged-in user

- **SimulacionesController**: Create test notifications (NotificacionSimulada)

- **AuthController**: Custom authentication with User table validation

- **TrabajadorController**: Employee management

- **DocumentoMedicoController**: Medical document upload/viewing

- **MonitoreoController**: Dashboard/monitoring views

### ViewModels Pattern
Some ViewModels exist in Models/ folder (SimulacionNotificacionViewModel, SupervisionSubsidioViewModel, TrabajadorSeleccionadoViewModel), others in ViewModels/ folder. When creating new ViewModels, check existing location patterns for the feature.

### Views Structure
Views are organized by controller name with shared layouts in Views/Shared/:
- _Layout.cshtml: Main layout with navigation
- _LoginPartial.cshtml: Authentication UI
- Partial views often prefixed with underscore (_DetalleDescanso.cshtml, _NotificationDetail.cshtml)

## Important Patterns

### Database Querying
All controllers use ApplicationDbContext with eager loading via `.Include()`:
```csharp
var descanso = _context.DbSetDescanso
    .Include(d => d.User)
    .Include(d => d.TipoDescanso)
    .Include(d => d.Accidente)
    .FirstOrDefault(d => d.IdDescanso == id);
```

### Estado Fields Workflow
The Descanso entity uses three separate estado fields for different workflow stages:
1. EstadoESSALUD: ESSALUD validation ("En Proceso" → "Válido"/"No válido")
2. EstadoSubsidioA: Employee-facing status ("Descanso Activo")
3. EstadoSubsidioJ: Supervisor approval ("Pendiente" → "Aprobado"/"Rechazado")

### Role-Based Access
System uses ASP.NET Core Identity roles:
- "Jefe": Supervisor with approval permissions
- "Trabajador": Regular employees (default)

The "oskar@oskar" user is auto-assigned "Jefe" role on startup (Program.cs lines 56-73).

### Session Usage
Session is configured with 30-minute timeout and used to track user context. Remember to check session state when working on authentication flows.

### PostgreSQL Specifics
- Connection string in appsettings.json points to Render.com hosted database
- Array columns (text[]) require explicit HasColumnType configuration
- Timestamp columns use "timestamp without time zone"

## Database Connection

The DefaultConnection in appsettings.json connects to a PostgreSQL instance on Render.com. The connection string includes sensitive credentials—DO NOT commit changes that expose these values. When modifying connection strings, use user secrets or environment variables.

## Common Development Patterns

### Adding New Leave Type
1. Create model in Models/ (inherit appropriate base or follow Accidente pattern)
2. Add DbSet to ApplicationDbContext
3. Create migration: `dotnet ef migrations add Add<TypeName>`
4. Create controller following AccidenteController pattern
5. Add TipoDescanso seed data if needed
6. Create views in Views/<TypeName>/

### Adding New Migration
Always include descriptive name and review generated migration before updating database:
```powershell
dotnet ef migrations add DescriptiveNameHere
# Review Data/Migrations/<timestamp>_DescriptiveNameHere.cs
dotnet ef database update
```

### Working with Documents
DocumentoMedico stores PDFs as byte arrays (Archivo property). When displaying:
- Use VerDocumento action with inline Content-Disposition header
- Render in iframe for in-page viewing
- Check null and empty byte[] before accessing

### Creating Notifications
Use Notification entity to inform users of estado changes:
```csharp
var notification = new Notification
{
    UserId = user.IdUser.ToString(),
    Titulo = "Title",
    Mensaje = "Message body",
    Estado = "Estado type",
    Fecha = DateTime.UtcNow,
    Detalle = "Additional details",
    DocumentoAdjuntos = new List<string>()
};
_context.Notifications.Add(notification);
await _context.SaveChangesAsync();
```

## Notes

- Some ViewModels like ErrorViewModel remain in Models/ due to scaffolding
- Lista model is a DTO used for list views (not a database entity)
- User table is separate from IdentityUser—authentication uses Identity, but application data uses User
- The codebase uses Spanish naming conventions for domain entities and properties
- Some controllers return HTML Content() directly instead of views (e.g., ListaController.EnviarObservacion)
