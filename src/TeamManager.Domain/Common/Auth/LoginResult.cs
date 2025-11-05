using TeamManager.Domain.Entities;
using TeamManager.Domain.Providers.Authentication.Entities;

namespace TeamManager.Domain.Common.Auth;

public class LoginResult
{
    public ApplicationAuthUser User { get; set; }
    public AuthResult Tokens { get; set; }

    private LoginResult(ApplicationAuthUser user, AuthResult tokens)
    {
        User = user;
        Tokens = tokens;
    }

    public static LoginResult Build(ApplicationAuthUser user, AuthResult token)
    {
        return new LoginResult(user, token);
    }
}