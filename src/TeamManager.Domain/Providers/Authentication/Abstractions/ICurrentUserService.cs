using System.Security.Claims;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Providers.Authentication.Abstractions;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    ClaimsPrincipal? User { get; }
    Task<Result<ApplicationAuthUser>> GetCurrentUserOrThrow();
}