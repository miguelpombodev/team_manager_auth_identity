using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Members.Abstractions;

public interface ITeamRepository
{
    Task<Team?> RetrieveTeamByIdAsync(Guid teamId);
    Task<List<Team>?> RetrieveTeamsByMemberIdAsync(Guid userId);
    Task<Team> Create(Team team);
    Task<UserTeam> CreateUserTeamAsync(UserTeam userTeam);
}