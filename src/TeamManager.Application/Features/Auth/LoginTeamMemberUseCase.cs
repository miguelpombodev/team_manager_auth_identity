using Microsoft.AspNetCore.Identity;
using TeamManager.Application.Contracts.Auth;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Authentication.Abstractions;
using TeamManager.Domain.Providers.Authentication.Entities;

namespace TeamManager.Application.Features.Auth;

public class LoginTeamMemberUseCase : IUseCase<AuthBaseRequest, Result<LoginResult>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly UserManager<ApplicationAuthUser> _userManager;
    private readonly ITokenProvider _tokenProvider;

    public LoginTeamMemberUseCase(
        IMemberRepository memberRepository,
        ITokenProvider tokenProvider,
        UserManager<ApplicationAuthUser> userManager,
        ITeamRepository teamRepository
    )
    {
        _memberRepository = memberRepository;
        _tokenProvider = tokenProvider;
        _userManager = userManager;
        _teamRepository = teamRepository;
    }

    public async Task<Result<LoginResult>> ExecuteAsync(AuthBaseRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result<LoginResult>.Failure(AuthenticationErrors.UserAccountNotFound);
        }
        var globalRoles = await _userManager.GetRolesAsync(user);
        var userTeamRole = await _memberRepository.RetrieveTeamMemberRolesByEntity(user);

        var accessToken = _tokenProvider.Create(user, globalRoles, userTeamRole);
        var refreshToken = _tokenProvider.CreateRefreshToken();

        var auth = AuthResult.Create(accessToken, refreshToken);

        var result = LoginResult.Build(user, auth);

        return Result<LoginResult>.Success(result);
    }
}