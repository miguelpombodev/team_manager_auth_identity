using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Members.Errors;

public static class AuthenticationErrors
{
    public static readonly Error UserAccountNotFound = new Error(
        "Account.UserNotFound",
        404,
        "User not found");
    
    public static readonly Error UserActionNotAuthorized = new Error(
        "Account.UserActionNotAuthorized",
        403,
        "User cannot perform the specific action");

    public static readonly Error RefreshTokenNotFound = new(
        "Authentication.RefreshTokenNotFound",
        404,
        "Refresh Token not found! Please be sure!");
    
    public static readonly Error RefreshTokenGenerationError = new(
        "Authentication.RefreshTokenGenerationError",
        409,
        "There was not possible to generate a new refresh token");

    public static readonly Error DeleteRefreshTokenError = new(
        "Authentication.DeleteRefreshTokenError",
        409,
        "Refresh Token could not be deleted! Please try again");
}