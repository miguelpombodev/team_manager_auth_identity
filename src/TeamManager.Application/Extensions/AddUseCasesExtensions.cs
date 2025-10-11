using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Application.Features.Auth;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Providers.Authentication.Entities;

namespace TeamManager.Application.Extensions;

public static class AddUseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<RegisterTeamMember, Result<ApplicationAuthUser>>, RegisterTeamMemberUseCase>();
        services.AddScoped<IUseCase<AuthBaseRequest, Result<(AuthResult, ApplicationAuthUser)>>, LoginTeamMemberUseCase>();
        
        services.AddScoped<RegisterTeamMemberUseCase>();
        services.AddScoped<LoginTeamMemberUseCase>();
        
        return services;
    }
}