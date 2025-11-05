using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TeamManager.Application.Contracts.Auth;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Authentication.Abstractions;

namespace TeamManager.Application.Features.Auth;

public class ResetPasswordUseCase : IUseCase<ResetPasswordRequest, Result>
{
    private readonly UserManager<ApplicationAuthUser> _userManager;
    private readonly ILogger<ResetPasswordUseCase> _logger;

    public ResetPasswordUseCase(
        UserManager<ApplicationAuthUser> userManager,
        ILogger<ResetPasswordUseCase> logger
    )
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result> ExecuteAsync(ResetPasswordRequest request)
    {
        var getUserResult = await _userManager.FindByEmailAsync(
            request.UserEmail
        );

        if (getUserResult is null)
        {
            _logger.LogWarning(
                "[WARNING] There was an unsucessful attempt to reset a password with a wrong email address"
            );

            return Result.Failure(AuthenticationErrors.UserAccountNotFound);
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(getUserResult);
        var passwordChangeResult =
            await _userManager.ResetPasswordAsync(getUserResult, resetToken, request.NewPassword);

        if (!passwordChangeResult.Succeeded)
        {
            var errors = string.Join(",", passwordChangeResult.Errors.Select(x => x.Description));
            return Result.Failure(new Error(
                "ResetPasswordUseCase",
                Description: $"Failed to create a new member: {errors}"));
        }

        return Result.Success();
    }
}