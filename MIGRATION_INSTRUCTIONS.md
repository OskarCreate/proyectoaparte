# Instrucciones de Migración - Eliminación de ASP.NET Core Identity

## ✅ Cambios Realizados

He realizado los siguientes cambios para que tu aplicación use **solo tu tabla T_Usuarios** sin depender de ASP.NET Core Identity:

### 1. **Program.cs**
- ✅ Eliminado `AddDefaultIdentity` y `AddRoles`
- ✅ Configurado autenticación basada en cookies sin Identity
- ✅ Removida asignación automática de rol "Jefe" a oskar@oskar (ya no es necesario)

### 2. **AuthController.cs**
- ✅ Implementado sistema de Claims en el login
- ✅ Ahora guarda información del usuario en cookies de autenticación Y en sesión
- ✅ El logout limpia tanto cookies como sesión

### 3. **Helpers/UserHelper.cs** (NUEVO)
- ✅ Clase helper para obtener el usuario actual de forma consistente
- ✅ Funciona con Claims (cookies) y Session (fallback)

### 4. **Controladores de Solicitudes**
Actualizados para usar `UserHelper.GetCurrentUser()`:
- ✅ AccidenteController
- ✅ EnfermedadController
- ✅ MaternidadController
- ✅ PaternidadController
- ✅ EnfermedadFamController
- ✅ FallecimientoController

### 5. **ApplicationDbContext.cs**
- ✅ Cambiado de `IdentityDbContext<IdentityUser>` a `DbContext`

## 📋 Pasos Siguientes

### Paso 1: Crear nueva migración
```powershell
dotnet ef migrations add RemoveIdentityTables
```

Esta migración eliminará las siguientes tablas de Identity de la base de datos:
- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- AspNetUserClaims
- AspNetUserLogins
- AspNetUserTokens
- AspNetRoleClaims

### Paso 2: Revisar la migración
```powershell
# Revisar el archivo generado en Data/Migrations/
# Asegúrate de que solo elimina tablas de Identity y NO toca T_Usuarios
```

### Paso 3: Aplicar la migración
```powershell
dotnet ef database update
```

### Paso 4: Compilar y ejecutar
```powershell
dotnet build
dotnet run
```

## 🧪 Cómo Probar

### 1. **Registro de Usuario**
1. Ir a `/Auth/Register`
2. Registrar un nuevo usuario (usa tu tabla T_Usuarios)
3. Verificar que el usuario aparece en la tabla `T_Usuarios` en la base de datos

### 2. **Login**
1. Ir a `/Auth/Login`
2. Iniciar sesión con el usuario creado
3. Verificar que redirige a `/Home/Index`
4. Verificar en las herramientas de desarrollador que existe una cookie `.AspNetCore.Cookies`

### 3. **Crear Solicitud de Descanso**
1. Estando logueado, ir a cualquier tipo de solicitud (ej: `/Accidente/Index`)
2. Llenar el formulario y enviar
3. **IMPORTANTE**: Ahora debe redirigir correctamente a `/DocumentoMedico/Index` sin errores
4. Verificar en la base de datos:
   - Tabla correspondiente (ej: t_accidente) tiene el registro
   - Tabla `t_Descanso` tiene el registro con `UserId` correcto

### 4. **Verificar Usuario en Descansos**
```sql
-- Ejecutar en PostgreSQL para verificar que todo está conectado
SELECT 
    d.IdDescanso,
    u.Username,
    u.Email,
    td.Nombre as TipoDescanso,
    d.FechaSolicitud
FROM t_Descanso d
INNER JOIN "T_Usuarios" u ON d.UserId = u.IdUser
INNER JOIN t_tipodescanso td ON d.TipoDescansoId = td.IdTDescanso
ORDER BY d.FechaSolicitud DESC;
```

## ⚠️ Problemas Comunes

### Problema: "No hay usuario autenticado"
**Solución**: Asegúrate de estar logueado. El sistema ahora requiere login real (no sesión manual).

### Problema: Error en `dotnet ef migrations add`
**Solución**: Instala las herramientas de EF Core si no las tienes:
```powershell
dotnet tool install --global dotnet-ef
```

### Problema: "The entity type 'IdentityUser' requires a primary key"
**Solución**: Ya está resuelto - ApplicationDbContext ya no hereda de IdentityDbContext.

### Problema: Error al aplicar migración en producción
**Solución**: Si tu base de datos en Render.com tiene datos de Identity que no quieres perder:
1. Haz backup primero
2. O comenta las líneas `migrationBuilder.DropTable()` en la migración

## 📝 Notas Importantes

1. **Ya no necesitas AspNetUsers**: Todo se maneja con T_Usuarios
2. **El sistema de roles sigue funcionando**: El campo `Rol` en T_Usuarios maneja "Jefe" y "Trabajador"
3. **La autenticación es más simple**: No hay confirmación de email, no hay 2FA - solo username/password directo
4. **Sesión + Cookies**: Usamos ambos para máxima compatibilidad con tu código existente

## 🎯 Beneficios

- ✅ Un solo lugar para gestionar usuarios (T_Usuarios)
- ✅ No más conflictos entre Identity y tu tabla personalizada
- ✅ Más fácil de entender y mantener
- ✅ Las solicitudes de descanso ahora funcionan correctamente
- ✅ El flujo completo (solicitud → documentos) ya no se rompe

## 🔧 Si Necesitas Revertir

Si algo sale mal y necesitas volver atrás:

```powershell
# Revertir a la migración anterior
dotnet ef database update <NombreMigracionAnterior>

# Eliminar la migración
dotnet ef migrations remove
```

Luego restaura los archivos originales desde Git:
```powershell
git checkout Program.cs
git checkout Data/ApplicationDbContext.cs
git checkout Controllers/AuthController.cs
```
