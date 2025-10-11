using Microsoft.AspNetCore.Identity;
using TeamManager.Domain.Common.Abstraction;
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