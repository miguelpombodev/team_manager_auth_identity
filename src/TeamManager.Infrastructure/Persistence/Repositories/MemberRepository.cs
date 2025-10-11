using Microsoft.AspNetCore.Identity;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;

namespace TeamManager.Infrastructure.Persistence.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly UserManager<ApplicationAuthUser> _userManager;
    private readonly ApplicationDbContext _context;

    public MemberRepository(UserManager<ApplicationAuthUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    public async Task<ApplicationAuthUser> CreateAsync(ApplicationAuthUser user, UserComplements userComplements, string userPassword)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(); 
        
        IdentityResult identityResult =  await _userManager.CreateAsync(user, userPassword);
        
        await _userManager.AddToRoleAsync(user, Roles.TeamMember);

        await _context.UserComplements.AddAsync(userComplements);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
        
        return user;
    }

    public Task<ApplicationAuthUser?> RetrieveEntityByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationAuthUser?> RetrieveEntityByEmailAsync(string email)
    {
        var entity = await _userManager.FindByEmailAsync(email);

        return entity;
    }

    public async Task<IList<string>> RetrieveMemberRolesByEntity(ApplicationAuthUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        return roles;
    }

    public Task UpdateEntityAsync(ApplicationAuthUser entity)
    {
        throw new NotImplementedException();
    }
}