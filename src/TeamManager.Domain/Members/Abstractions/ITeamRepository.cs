using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Members.Abstractions;

public interface ITeamRepository
{
    Task<List<Team>?> RetrieveTeamsByMemberIdAsync(Guid userId);
    Task<Team> Create(Team team);

}