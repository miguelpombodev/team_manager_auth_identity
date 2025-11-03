using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Authentication.Abstractions;

namespace TeamManager.Infrastructure.Providers.Authentication;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationAuthUser> _userManager;
    private readonly ILogger<CurrentUserService> _logger;


    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationAuthUser> userManager,
        ILogger<CurrentUserService> logger
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public Guid? UserId
    {
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

    public async Task<Result<ApplicationAuthUser>> GetCurrentUserOrThrow()
    {
        var userClaimsPrincipal = User;

        if (userClaimsPrincipal is null)
        {
            _logger.LogWarning(
                "[WARNING] There was an unsucessful attempt to generate a new refresh token with a authenticated user (Nullable ClaimsPrincipal).");
            return Result<ApplicationAuthUser>.Failure(AuthenticationErrors.UserAccountNotFound);
        }

        var getUserResult = await _userManager.GetUserAsync(
            userClaimsPrincipal
        );

        if (getUserResult is null)
        {
            _logger.LogWarning(
                "[WARNING] There was an unsucessful attempt to log with {User}, please check its informations",
                userClaimsPrincipal
            );

            return Result<ApplicationAuthUser>.Failure(AuthenticationErrors.UserAccountNotFound);
        }

        return Result<ApplicationAuthUser>.Success(getUserResult);
    }
}