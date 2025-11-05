using Microsoft.AspNetCore.Authorization;
using TeamManager.API.Authorization.Requirements;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Entities;

namespace TeamManager.API.Authorization.Handlers;

public class ManageTeamAuthorizationHandler : AuthorizationHandler<ManageTeamRequirement, Guid>
{
    private readonly ILogger<ManageTeamAuthorizationHandler> _logger;

    // 2. Injetar o ILogger
    public ManageTeamAuthorizationHandler(ILogger<ManageTeamAuthorizationHandler> logger)
    {
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ManageTeamRequirement requirement,
        Guid teamId)
    {
        if (context.User.IsInRole(Roles.SystemAdmin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var teamLeaderClaimValue = $"{teamId}:{Roles.TeamLeader}";
        var userTeamRoles = context.User.Claims
            .Where(c => c.Type == CustomClaimTypes.TeamRole)
            .Select(c => c.Value);
        _logger.LogWarning("Claims 'TeamRole' encontradas no token: {UserClaims}", userTeamRoles);
        if (
            context.User.HasClaim(CustomClaimTypes.TeamRole, teamLeaderClaimValue)
        )
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}