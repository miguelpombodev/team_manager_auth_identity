using System.Security.Claims;

namespace TeamManager.Domain.Providers.Authentication.Abstractions;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    ClaimsPrincipal? User { get; }
    Guid GetUserIdOrThrow();
}