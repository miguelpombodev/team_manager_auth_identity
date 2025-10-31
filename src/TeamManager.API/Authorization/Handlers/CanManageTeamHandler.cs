using Microsoft.AspNetCore.Authorization;
using TeamManager.API.Authorization.Requirements;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Entities;

namespace TeamManager.API.Authorization.Handlers;

public class ManageTeamAuthorizationHandler : AuthorizationHandler<ManageTeamRequirement, Guid>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageTeamRequirement requirement, Guid teamId)
    {
        if (context.User.IsInRole(Roles.SystemAdmin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var systemAdminClaimValue = $"{teamId}:{Roles.SystemAdmin}";
        var teamLeaderClaimValue = $"{teamId}:{Roles.TeamLeader}";

        if (context.User.HasClaim(CustomClaimTypes.TeamRole, systemAdminClaimValue) ||
            context.User.HasClaim(CustomClaimTypes.TeamRole, teamLeaderClaimValue))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}