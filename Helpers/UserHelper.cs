using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using proyectoIngSoft.Data;
using proyectoIngSoft.Models;

namespace proyectoIngSoft.Helpers
{
    public static class UserHelper
    {
        /// <summary>
        /// Obtiene el usuario actual desde Claims o Session
        /// </summary>
        public static User? GetCurrentUser(HttpContext httpContext, ApplicationDbContext context)
        {
            // Intentar obtener desde Claims primero
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
            {
                return context.DbSetUser.FirstOrDefault(u => u.IdUser == userId);
            }

            // Fallback a Session (para compatibilidad con código antiguo)
            var userIdSession = httpContext.Session.GetInt32("UserId");
            if (userIdSession.HasValue)
            {
                return context.DbSetUser.FirstOrDefault(u => u.IdUser == userIdSession.Value);
            }

            // Fallback a Email en Session
            var userEmail = httpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userEmail))
            {
                return context.DbSetUser.FirstOrDefault(u => u.Email == userEmail);
            }

            return null;
        }

        /// <summary>
        /// Verifica si hay un usuario autenticado
        /// </summary>
        public static bool IsUserAuthenticated(HttpContext httpContext)
        {
            // Verificar Claims
            if (httpContext.User.Identity?.IsAuthenticated == true)
                return true;

            // Verificar Session
            if (httpContext.Session.GetInt32("UserId").HasValue)
                return true;

            if (!string.IsNullOrEmpty(httpContext.Session.GetString("User")))
                return true;

            return false;
        }
    }
}
