using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Providers.Authentication.Abstractions;

namespace TeamManager.Infrastructure.Providers.Security;

public class TokenProvider : ITokenProvider
{
    private readonly JwtSecurityTokenHandler _handler = new();
    // private readonly IDatabase _redis;

    // private const string RedisRefreshTokenPrefix = "refresh_token:";
    private readonly TokenValidationParameters _validationParameters;

    private readonly Jwt _jwt;
    
    public TokenProvider(IConfiguration configuration)
    {
        // _redis = redis.GetDatabase();

        _jwt = Jwt.Create();
        _validationParameters = _jwt.BuildTokenValidationParameters();
    }
    
    public string Create(ApplicationAuthUser user, IList<string> roles)
    {
        var credentials = BuildCredentials();

        var tokenDescriptor = BuildTokenDescriptor(credentials, user, roles);

        var token = _handler.CreateToken(tokenDescriptor);

        return _handler.WriteToken(token);
    }
    
    private SigningCredentials BuildCredentials()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));

        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    private SecurityTokenDescriptor BuildTokenDescriptor(SigningCredentials credentials, ApplicationAuthUser user, IList<string> roles)
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
            Expires = now.AddMinutes(_jwt.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
        };
    }
}