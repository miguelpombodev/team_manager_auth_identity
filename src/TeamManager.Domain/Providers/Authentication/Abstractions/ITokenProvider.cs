using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Entities;

namespace TeamManager.Domain.Providers.Authentication.Abstractions;

public interface ITokenProvider
{
    string Create(ApplicationAuthUser user, IList<UserTeamRoleDto> userTeamRoleDto);
    string CreateRefreshToken();
    Task<bool> ReplaceRefreshTokenAsync(Guid userId, string newRefreshToken);
    Task<Result<Guid>> ValidateRefreshTokenAsync(string refreshToken);
    Task<bool> DeleteRefreshTokenAsync(string refreshToken);
}