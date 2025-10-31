using Microsoft.AspNetCore.Authorization;
using TeamManager.API.Authorization.Requirements;
using TeamManager.Domain.Entities;

namespace TeamManager.API.Authorization.Handlers;

public class CanCreateTeamHandler : AuthorizationHandler<CreateTeamRequirements>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateTeamRequirements requirement)
    {
        if (context.User.IsInRole(Roles.SystemAdmin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        return Task.CompletedTask;
    }
}