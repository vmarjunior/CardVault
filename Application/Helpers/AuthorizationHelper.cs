public static class AuthorizationHelper
{
    public static void EnsureResourceOwner(Guid currentUserId, Guid resourceOwnerId)
    {
        if (currentUserId != resourceOwnerId)
            throw new UnauthorizedAccessException("Unauthorized.");
    }
}