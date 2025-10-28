using Microsoft.AspNetCore.Authorization;
using TeamManager.API.Authorization;

namespace TeamManager.API.Extensions;

public static class AuthorizationServiceExtension
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("GlobalAdmin", policy => policy.AddRequirements(new ManageTeamRequirement()));
            options.AddPolicy("CanManageTeam", policy => policy.AddRequirements(new ManageTeamRequirement()));
            options.AddPolicy("CanViewTeam", policy => policy.AddRequirements(new ManageTeamRequirement()));
        });

        services.AddSingleton<IAuthorizationHandler, ManageTeamAuthorizationHandler>();

        return services;
    }
}