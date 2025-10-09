using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Application.Features.Auth;
using TeamManager.Domain.Entities;

namespace TeamManager.Application.Extensions;

public static class AddUseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<RegisterTeamMember, ApplicationAuthUser>, RegisterTeamMemberUseCase>();
        
        services.AddScoped<RegisterTeamMemberUseCase>();
        
        return services;
    }
}