using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Providers.Authentication.Abstractions;

public interface ITokenProvider
{
    string Create(ApplicationAuthUser user, IList<string> roles);
    string CreateRefreshToken();
    Task<bool> ReplaceRefreshTokenAsync(Guid userId, string newRefreshToken);
    Task<Result<Guid>> ValidateRefreshTokenAsync(string refreshToken);
    Task<bool> DeleteRefreshTokenAsync(string refreshToken);
}