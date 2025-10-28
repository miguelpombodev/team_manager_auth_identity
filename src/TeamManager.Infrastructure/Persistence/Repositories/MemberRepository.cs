using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;

namespace TeamManager.Infrastructure.Persistence.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;

    public MemberRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationAuthUser?> RetrieveEntityByIdAsync(Guid id)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));

        return entity;
    }

    public async Task<UserComplements> CreateUserComplements(UserComplements userComplements)
    {
        var result = await _context.UserComplements.AddAsync(userComplements);
        
        return result.Entity;
    }

    public async Task<IList<UserTeamRoleDto>> RetrieveTeamMemberRolesByEntity(ApplicationAuthUser user)
    {
        
        var roles = await _context.UserTeams.Where(ut => ut.UserId == user.Id)
            .Select(ut => new UserTeamRoleDto(ut.TeamId, ut.RoleName)).ToListAsync();
        return roles;
    }

    public async Task UpdateEntityAsync(ApplicationAuthUser entity)
    {
        _context.Users.Update(entity);

        await _context.SaveChangesAsync();
    }
    
}