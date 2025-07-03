using System.Security.Claims;

namespace Shared.Extentions;
public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)
            ?? user.FindFirst("sub");

        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
            throw new UnauthorizedAccessException("UserId claim not found or invalid.");

        return userId;
    }
}
