using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Providers.Authentication.Abstractions;

public interface ITokenProvider
{
    string Create(ApplicationAuthUser user, IList<string> roles);
}