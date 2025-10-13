using System.Text;
using Microsoft.IdentityModel.Tokens;
using TeamManager.Infrastructure.Configurations;

namespace TeamManager.Infrastructure.Providers.Security;

public class Jwt
{
    public string Secret { get; }
    public string Issuer { get; }
    public string Audience { get; }
    public int ExpirationInMinutes { get; }

    private Jwt(string secret, string issuer, string audience, int expirationInMinutes)
    {
        Secret = secret;
        Issuer = issuer;
        Audience = audience;
        ExpirationInMinutes = expirationInMinutes;
    }

    public static Jwt Create()
    {
        return new Jwt(
            AppSettings.JwtSecret,
            AppSettings.JwtIssuer,
            AppSettings.JwtAudience,
            AppSettings.JwtExpirationInMinutes
        );
    }

    public TokenValidationParameters BuildTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = AppSettings.JwtIssuer,
            ValidAudience = AppSettings.JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtSecret))
        };
    }
}