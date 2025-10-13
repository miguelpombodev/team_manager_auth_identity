using Microsoft.AspNetCore.Identity;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Authentication.Abstractions;
using TeamManager.Domain.Providers.Authentication.Entities;

namespace TeamManager.Application.Features.Auth;

public class LoginTeamMemberUseCase : IUseCase<AuthBaseRequest, Result<(AuthResult, ApplicationAuthUser)>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly UserManager<ApplicationAuthUser> _userManager;
    private readonly ITokenProvider _tokenProvider;

    public LoginTeamMemberUseCase(IMemberRepository memberRepository, ITokenProvider tokenProvider, UserManager<ApplicationAuthUser> userManager)
    {
        _memberRepository = memberRepository;
        _tokenProvider = tokenProvider;
        _userManager = userManager;
    }

    public async Task<Result<(AuthResult, ApplicationAuthUser)>> ExecuteAsync(AuthBaseRequest request)
    {
        var user = await _memberRepository.RetrieveEntityByEmailAsync(request.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result<(AuthResult, ApplicationAuthUser)>.Failure(AuthenticationErrors.UserAccountNotFound);
        }
        
        var roles = await _memberRepository.RetrieveMemberRolesByEntity(user);

        var accessToken = _tokenProvider.Create(user, roles);
        var refreshToken = _tokenProvider.CreateRefreshToken();
        
        var auth = AuthResult.Create(accessToken, refreshToken);

        return Result<(AuthResult, ApplicationAuthUser)>.Success(data: (auth, user));
    }
}