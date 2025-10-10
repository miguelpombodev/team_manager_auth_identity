using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Members.Abstractions;

public interface IMemberRepository
{
    Task<ApplicationAuthUser> CreateAsync(ApplicationAuthUser user, UserComplements userComplements);
    Task<ApplicationAuthUser?> RetrieveEntityByIdAsync(Guid id);
    Task UpdateEntityAsync(ApplicationAuthUser entity);
};
