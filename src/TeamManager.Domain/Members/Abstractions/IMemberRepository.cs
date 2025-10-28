using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Entities;

namespace TeamManager.Domain.Members.Abstractions;

public interface IMemberRepository
{
    Task<ApplicationAuthUser?> RetrieveEntityByIdAsync(Guid id);
    Task<UserComplements> CreateUserComplements(UserComplements userComplements);
    Task<IList<UserTeamRoleDto>> RetrieveTeamMemberRolesByEntity(ApplicationAuthUser user);
    Task UpdateEntityAsync(ApplicationAuthUser entity);
}