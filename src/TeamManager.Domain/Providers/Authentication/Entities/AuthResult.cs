using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Providers.Authentication.Entities;

public class AuthResult
{
    protected AuthResult(bool isAuthenticated, string accessToken, string refreshToken)
    {
        IsAuthenticated = isAuthenticated;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public bool IsAuthenticated { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public static AuthResult Create(string accessToken, string refreshToken, bool isAuthenticated = true)
    {
        return new AuthResult(isAuthenticated, accessToken, refreshToken);
    }
}