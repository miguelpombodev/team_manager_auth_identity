using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Contracts.Auth;
using TeamManager.Application.Contracts.Members;
using TeamManager.Application.Contracts.Teams;
using TeamManager.Application.Features;
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
        services
            .AddScoped<IUseCase<AuthBaseRequest, Result<(AuthResult, ApplicationAuthUser)>>, LoginTeamMemberUseCase>();
        services.AddScoped<IUseCase<RegisterTeam, Result<Team>>, RegisterTeamUseCase>();
        services.AddScoped<IUseCase<MemberValidationLinkAndToken, Result<bool>>, SendEmailVerificationUseCase>();
        services.AddScoped<IUseCase<Guid, Result<bool>>, ConfirmMemberEmailUseCase>();
        services.AddScoped<IUseCase<Result<AuthResult>>, GenerateNewRefreshTokenUseCase>();
        services.AddScoped<IUseCase<ResetPasswordRequest, Result>, ResetPasswordUseCase>();
        services.AddScoped<IUseCase<Guid, Result<List<Team>>>, GetLoggedMemberTeams>();
        services.AddScoped<IUseCase<AddNewMemberInTeamRequest, Result<UserTeam>>, AddNewMemberInTeamUseCase>();
        services.AddScoped<IUseCase<RemoveMemberFromTeamRequest, Result>, DeleteMemberFromTeam>();

        services.AddScoped<RegisterTeamMemberUseCase>();
        services.AddScoped<LoginTeamMemberUseCase>();
        services.AddScoped<RegisterTeamUseCase>();
        services.AddScoped<SendEmailVerificationUseCase>();
        services.AddScoped<ConfirmMemberEmailUseCase>();
        services.AddScoped<GenerateNewRefreshTokenUseCase>();
        services.AddScoped<ResetPasswordUseCase>();
        services.AddScoped<GetLoggedMemberTeams>();
        services.AddScoped<AddNewMemberInTeamUseCase>();
        services.AddScoped<DeleteMemberFromTeam>();

        services.AddScoped<ClaimsPrincipal>();

        return services;
    }
}