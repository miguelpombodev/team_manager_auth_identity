using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TeamManager.Domain.Providers.Authentication.Abstractions;

namespace TeamManager.Infrastructure.Providers.Authentication;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? User=> _httpContextAccessor.HttpContext?.User;
    
    public Guid? UserId {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            return null;
        }
    }
    
    public Guid GetUserIdOrThrow()
    {
        var userId = UserId;
        if (userId is null)
        {
            throw new InvalidOperationException("User not authenticated or Id not found in token");
        }

        return userId.Value;
    }
}