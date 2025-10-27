using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;

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

    public async Task<Result<ApplicationAuthUser>> CreateAsync(ApplicationAuthUser user,
        UserComplements userComplements, string userPassword)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var identityResult = await _userManager.CreateAsync(user, userPassword);

            if (!identityResult.Succeeded)
            {
                var errors = string.Join(",", identityResult.Errors.Select(x => x.Description));
                return Result<ApplicationAuthUser>.Failure(new Error(
                    "MemberRepositoryError",
                    Description: $"Failed to create a new member: {errors}"));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.TeamMember);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(x => x.Description));
                await transaction.RollbackAsync();
                return Result<ApplicationAuthUser>.Failure(new Error(
                    "MemberRepositoryError",
                    Description: $"Failed to associate user to role: {errors}"));
            }

            await _context.UserComplements.AddAsync(userComplements);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return Result<ApplicationAuthUser>.Success(user);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result<ApplicationAuthUser>.Failure(new Error(
                "MemberRepositoryError",
                Description: $"Erro inesperado ao criar usuário: {ex.Message}"));
        }
    }

    public async Task<ApplicationAuthUser?> RetrieveEntityByIdAsync(Guid id)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));

        return entity;
    }

    public async Task<EmailVerificationToken?> RetrieveEmailVerificationByIdAsync(Guid id)
    {
        var tokenEntity = await _context.EmailVerificationTokens.Include(token => token.User).FirstOrDefaultAsync(token => token.Id == id);

        return tokenEntity;
    }

    public async Task<EmailVerificationToken> CreateEmailVerificationToken(EmailVerificationToken emailVerificationToken)
    {
        var tokenEntity = await _context.EmailVerificationTokens.AddAsync(emailVerificationToken);

        await _context.SaveChangesAsync();

        return tokenEntity.Entity;
    }

    public async Task<ApplicationAuthUser?> RetrieveEntityByEmailAsync(string email)
    {
        var entity = await _userManager.FindByEmailAsync(email);

        return entity;
    }

    public async Task<IList<UserTeamRoleDto>> RetrieveTeamMemberRolesByEntity(ApplicationAuthUser user)
    {
        var roles = await _context.Users.Where(u => u.Id.Equals(user.Id)).SelectMany(u => u.UserTeams.Select(ut => new
        {
            TeamId = ut.Team.Id,
            TeamName = ut.Team.Name,
            Roles = u.UserRoles.Select(ur => ur.Name)
        })).Select(x => new UserTeamRoleDto(x.TeamId, x.Roles)).ToListAsync();

        return roles;
    }

    public async Task UpdateEntityAsync(ApplicationAuthUser entity)
    {
        _context.Users.Update(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoveTokenVerification(EmailVerificationToken emailVerificationToken)
    {
        _context.EmailVerificationTokens.Remove(emailVerificationToken);
        
        await _context.SaveChangesAsync();

        return true;
    }
}