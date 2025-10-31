using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Settings;

namespace TeamManager.Infrastructure.Providers.Security;

public class Jwt
{
    private readonly IJwtSettings _jwtSettings;

    public Jwt(IJwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public SecurityTokenDescriptor BuildTokenDescriptor(
        SigningCredentials credentials,
        ApplicationAuthUser user,
        IList<string> globalRoles,
        IList<UserTeamRoleDto> teamRoles
    )
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        ];

        foreach (var role in globalRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        foreach (var teamRole in teamRoles)
        {
            string claimValue = $"{teamRole.TeamId}:{teamRole.RoleName}";
            claims.Add(new Claim(CustomClaimTypes.TeamRole, claimValue));
        }

        var now = DateTime.UtcNow;

        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now.AddSeconds(-30),
            Expires = now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
        };
    }

    public SecurityTokenDescriptor BuildTokenDescriptor(SigningCredentials credentials, ApplicationAuthUser user,
        IList<string> roles, List<UserTeam> userTeams)
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            ..userTeams.Select(x => new Claim(ClaimValueTypes.String, x.TeamId.ToString())),
            ..roles.Select(x => new Claim(ClaimTypes.Role, x))
        ];

        var now = DateTime.UtcNow;

        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            NotBefore = now.AddSeconds(-30),
            Expires = now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
        };
    }
}