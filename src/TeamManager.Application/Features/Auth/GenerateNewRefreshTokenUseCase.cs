using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Authentication.Abstractions;
using TeamManager.Domain.Providers.Authentication.Entities;

namespace TeamManager.Application.Features.Auth;

public class GenerateNewRefreshTokenUseCase : IUseCase<Result<AuthResult>>
{
    private const string LoginProvider = "TeamManager";
    private const string TokenName = "refresh_token";

    private readonly ITokenProvider _tokenProvider;
    private readonly UserManager<ApplicationAuthUser> _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMemberRepository _memberRepository;

    private readonly ILogger<GenerateNewRefreshTokenUseCase> _logger;

    public GenerateNewRefreshTokenUseCase(
        ITokenProvider tokenProvider,
        UserManager<ApplicationAuthUser> userManager,
        ICurrentUserService currentUserService,
        IMemberRepository memberRepository,
        ILogger<GenerateNewRefreshTokenUseCase> logger
    )
    {
        _tokenProvider = tokenProvider;
        _userManager = userManager;
        _currentUserService = currentUserService;
        _memberRepository = memberRepository;
        _logger = logger;
    }


    public async Task<Result<AuthResult>> ExecuteAsync()
    {
        var userClaimsPrincipal = _currentUserService.User;

        if (userClaimsPrincipal is null)
        {
            _logger.LogWarning(
                "[WARNING] There was an unsucessful attempt to generate a new refresh token with a authenticated user (Nullable ClaimsPrincipal).");
            return Result<AuthResult>.Failure(AuthenticationErrors.UserAccountNotFound);
        }

        var getUserResult = await _userManager.GetUserAsync(
            userClaimsPrincipal
        );

        if (getUserResult is null)
        {
            _logger.LogWarning(
                "[WARNING] There was an unsucessful attempt to log with {User}, please check its informations",
                userClaimsPrincipal
            );

            return Result<AuthResult>.Failure(AuthenticationErrors.UserAccountNotFound);
        }

        var newRefreshToken = _tokenProvider.CreateRefreshToken();

        var setAuthenticationResult =
            await _userManager.SetAuthenticationTokenAsync(getUserResult, LoginProvider, TokenName, newRefreshToken);

        if (!setAuthenticationResult.Succeeded)
        {
            var errors = setAuthenticationResult.Errors.Select(x => new
            {
                x.Code,
                x.Description
            });

            _logger.LogWarning(
                "[WARNING] There are some errors  try to set new refreshToken to user {User} - Errors - {Errors}",
                getUserResult,
                errors
            );
            return Result<AuthResult>.Failure(AuthenticationErrors.RefreshTokenGenerationError);
        }

        var globalRoles = await _userManager.GetRolesAsync(
            getUserResult
        );

        var userTeamRoles = await _memberRepository.RetrieveTeamMemberRolesByEntity(
            getUserResult
        );

        var newAccessToken = _tokenProvider.Create(
            getUserResult,
            globalRoles,
            userTeamRoles
        );

        return Result<AuthResult>.Success(
            AuthResult.Create(
                newAccessToken,
                newRefreshToken
            )
        );
    }
}