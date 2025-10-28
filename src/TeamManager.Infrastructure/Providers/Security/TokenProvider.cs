using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Authentication.Abstractions;

namespace TeamManager.Infrastructure.Providers.Security;

public class TokenProvider : ITokenProvider
{
    private const string RedisRefreshTokenPrefix = "refresh_token:";

    private readonly JwtSecurityTokenHandler _handler = new();
    private readonly IDatabase _redis;

    private readonly TokenValidationParameters _validationParameters;

    private readonly Jwt _jwt;

    public TokenProvider(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();

        _jwt = Jwt.Create();
        _validationParameters = _jwt.BuildTokenValidationParameters();
    }

    public string Create(ApplicationAuthUser user, IList<string> globalRoles, IList<UserTeamRoleDto> userTeamRoleDto)
    {
        var credentials = BuildCredentials();

        var tokenDescriptor = _jwt.BuildTokenDescriptor(credentials, user, globalRoles,userTeamRoleDto);

        var token = _handler.CreateToken(tokenDescriptor);

        return _handler.WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }

    public async Task<bool> ReplaceRefreshTokenAsync(Guid userId, string newRefreshToken)
    {
        var key = RedisRefreshTokenPrefix.Concat(newRefreshToken).ToString();

        var ttl = TimeSpan.FromDays(7);
        var userData = $"{userId}";

        return await _redis.StringSetAsync(key, userData, ttl);
    }

    public async Task<Result<Guid>> ValidateRefreshTokenAsync(string refreshToken)
    {
        var key = RedisRefreshTokenPrefix.Concat(refreshToken).ToString();
        var value = await _redis.StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return Result<Guid>.Failure(AuthenticationErrors.RefreshTokenNotFound);

        return Result<Guid>.Success(Guid.Parse(value!));
    }

    public async Task<bool> DeleteRefreshTokenAsync(string refreshToken)
    {
        var result = await _redis.KeyDeleteAsync(RedisRefreshTokenPrefix.Concat(refreshToken).ToString());

        return result;
    }

    private SigningCredentials BuildCredentials()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));

        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }
}