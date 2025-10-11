using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Members.Errors;

public static class AuthenticationErrors
{
    public static readonly Error UserAccountNotFound = new Error("Account.UserNotFound", 404, "User not found");
}