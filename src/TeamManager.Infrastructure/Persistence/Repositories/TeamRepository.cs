using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;

namespace TeamManager.Infrastructure.Persistence.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _context;

    public TeamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Team?> RetrieveTeamByIdAsync(Guid teamId)
    {
        var team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(x => x.Id == teamId);

        return team;
    }

    public async Task<List<Team>?> RetrieveTeamsByMemberIdAsync(Guid userId)
    {
        var teams = await _context.UserTeams.AsNoTracking().Where(x => x.UserId.Equals(userId)).Select(x => x.Team)
            .ToListAsync();

        return teams;
    }

    public async Task<Team> Create(Team team)
    {
        var entity = await _context.Teams.AddAsync(team);

        await _context.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<UserTeam> CreateUserTeamAsync(UserTeam userTeam)
    {
        var result = await _context.UserTeams.AddAsync(userTeam);

        return result.Entity;
    }

    public Task<int> RemoveMemberFromTeamAsync(Guid memberId, Guid teamId)
    {
        var result = _context.UserTeams.Where(x => x.UserId == memberId && x.TeamId == teamId)
            .ExecuteDeleteAsync();

        return result;
    }
}