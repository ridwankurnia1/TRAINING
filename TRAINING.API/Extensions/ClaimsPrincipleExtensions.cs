using System.Security.Claims;

namespace TRAINING.API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static string GetOrganizationId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.PrimaryGroupSid)?.Value;
        }
        public static string GetDefaultLocation(this ClaimsPrincipal user)
        {
            return user.FindFirst("locality")?.Value;
        }
    }
}