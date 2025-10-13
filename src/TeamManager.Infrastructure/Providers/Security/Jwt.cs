using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TeamManager.Domain.Entities;
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
    
    public SecurityTokenDescriptor BuildTokenDescriptor(SigningCredentials credentials, ApplicationAuthUser user, IList<string> roles)
    {
        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            ..roles.Select(x => new Claim(ClaimTypes.Role, x))
        ];
        
        var now = DateTime.UtcNow;

        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now.AddSeconds(-30),
            Expires = now.AddMinutes(AppSettings.JwtExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = AppSettings.JwtIssuer,
            Audience = AppSettings.JwtAudience,
        };
    }
}