using TeamManager.Domain.Entities;
using TeamManager.Domain.Teams.DTOs;

namespace TeamManager.Domain.Members.Abstractions;

public interface ITeamRepository
{
    Task<Team?> RetrieveTeamByIdAsync(Guid teamId);
    Task<Team?> RetrieveTeamByNameAsync(string teamName);
    Task<List<Team>?> RetrieveTeamsByMemberIdAsync(Guid userId);
    Task<Team> Create(Team team);
    Task<UserTeam> CreateUserTeamAsync(UserTeam userTeam);
    Task<int> RemoveMemberFromTeamAsync(Guid memberId, Guid teamId);

    Task<TeamDetailsDto?> RetrieveTeamDetails(Guid teamId);
}