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

    public async Task<List<Team>?> RetrieveTeamsByMemberIdAsync(Guid userId)
    {
        var teams = await _context.UserTeams.AsNoTracking().Where(x => x.UserId.Equals(userId)).Select(x => x.Team).ToListAsync();

        return teams;
    }

    public async Task<Team> Create(Team team)
    {
        var entity = await _context.Teams.AddAsync(team);

        await _context.SaveChangesAsync();
        
        return entity.Entity;
    }
}