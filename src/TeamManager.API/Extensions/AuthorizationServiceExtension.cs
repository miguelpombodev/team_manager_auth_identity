using Microsoft.AspNetCore.Authorization;
using TeamManager.API.Authorization;
using TeamManager.API.Authorization.Handlers;
using TeamManager.API.Authorization.Requirements;
using TeamManager.Domain.Common.Auth;

namespace TeamManager.API.Extensions;

public static class AuthorizationServiceExtension
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // options.AddPolicy("GlobalAdmin", policy => policy.AddRequirements(new ManageTeamRequirement()));
            options.AddPolicy(AuthPolicies.CanCreateTeam, policy => policy.AddRequirements(new CreateTeamRequirements()));
            options.AddPolicy(AuthPolicies.CanManageTeam, policy => policy.AddRequirements(new ManageTeamRequirement()));
            // options.AddPolicy("CanViewTeam", policy => policy.AddRequirements(new ManageTeamRequirement()));
        });

        services.AddSingleton<IAuthorizationHandler, ManageTeamAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, CanCreateTeamHandler>();

        return services;
    }
}