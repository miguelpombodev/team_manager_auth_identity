using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Members.Abstractions;

public interface IMemberRepository
{
    Task<Result<ApplicationAuthUser>> CreateAsync(ApplicationAuthUser user, UserComplements userComplements, string userPassword);
    Task<ApplicationAuthUser?> RetrieveEntityByIdAsync(Guid id);
    Task<ApplicationAuthUser?> RetrieveEntityByEmailAsync(string email);
    Task<IList<string>> RetrieveMemberRolesByEntity(ApplicationAuthUser user);
    Task UpdateEntityAsync(ApplicationAuthUser entity);
};
