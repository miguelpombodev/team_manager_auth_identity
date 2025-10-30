using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Application.Abstractions.Requests.Teams;
using TeamManager.Application.Features.Auth;
using TeamManager.Application.Features.Member;
using TeamManager.Application.Features.Teams;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Providers.Authentication.Entities;

namespace TeamManager.Application.Extensions;

public static class AddUseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<RegisterTeamMember, Result<EmailVerificationToken>>, RegisterTeamMemberUseCase>();
        services.AddScoped<IUseCase<AuthBaseRequest, Result<(AuthResult, ApplicationAuthUser)>>, LoginTeamMemberUseCase>();
        services.AddScoped<IUseCase<RegisterTeam, Result<Team>>, RegisterTeamUseCase>();
        services.AddScoped<IUseCase<MemberValidationLinkAndToken, Result<bool>>, SendEmailVerificationUseCase>();
        services.AddScoped<IUseCase<Guid, Result<bool>>, ConfirmMemberEmailUseCase>();
        
        services.AddScoped<RegisterTeamMemberUseCase>();
        services.AddScoped<LoginTeamMemberUseCase>();
        services.AddScoped<RegisterTeamUseCase>();
        services.AddScoped<SendEmailVerificationUseCase>();
        services.AddScoped<ConfirmMemberEmailUseCase>();


        services.AddSingleton<IEmailTemplateFactory, EmailTemplateFactory>();
        
        return services;
    }
}