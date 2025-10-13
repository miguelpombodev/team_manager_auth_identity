using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Members.Errors;

public static class AuthenticationErrors
{
    public static readonly Error UserAccountNotFound = new Error(
        "Account.UserNotFound",
        404,
        "User not found");

    public static readonly Error RefreshTokenNotFound = new(
        "Authentication.RefreshTokenNotFound",
        404,
        "Refresh Token not found! Please be sure!");

    public static readonly Error DeleteRefreshTokenError = new(
        "Authentication.DeleteRefreshTokenError",
        409,
        "Refresh Token could not be deleted! Please try again");
}