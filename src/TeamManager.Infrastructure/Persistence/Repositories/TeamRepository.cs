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

    public async Task<List<UserTeam>?> RetrieveTeamsByMemberIdAsync(Guid userId)
    {
        var teams = await _context.UserTeams.AsNoTracking().Where(x => x.UserId.Equals(userId)).ToListAsync();

        return teams;
    }
}