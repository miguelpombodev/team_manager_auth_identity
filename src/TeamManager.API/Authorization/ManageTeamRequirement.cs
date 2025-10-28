using Microsoft.AspNetCore.Authorization;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Entities;

namespace TeamManager.API.Authorization;

public class ManageTeamRequirement : IAuthorizationRequirement
{
}

public class ManageTeamAuthorizationHandler : AuthorizationHandler<ManageTeamRequirement, Guid>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageTeamRequirement requirement, Guid teamId)
    {
        if (context.User.IsInRole(Roles.SystemAdmin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var teamAdminClaimValue = $"{teamId}:{Roles.TeamLeader}";
        var teamLeaderClaimValue = $"{teamId}:{Roles.TeamLeader}";

        if (context.User.HasClaim(CustomClaimTypes.TeamRole, teamAdminClaimValue) ||
            context.User.HasClaim(CustomClaimTypes.TeamRole, teamLeaderClaimValue))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}