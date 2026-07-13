using System.Security.Claims;

namespace CardVault.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userIdString, out var userId))
                return userId;

            throw new UnauthorizedAccessException("User ID is missing or invalid in the token.");
        }
    }
}