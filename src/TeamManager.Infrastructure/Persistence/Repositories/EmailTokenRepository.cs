using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;

namespace TeamManager.Infrastructure.Persistence.Repositories;

public class EmailTokenRepository : IEmailTokenRepository
{
    private readonly ApplicationDbContext _context;

    public EmailTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmailVerificationToken?> RetrieveByIdAsync(Guid id)
    {
        return await _context.EmailVerificationTokens
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.Id == id);
    }

    public async Task<EmailVerificationToken> AddAsync(EmailVerificationToken emailVerificationToken)
    {
        var result = await _context.EmailVerificationTokens.AddAsync(emailVerificationToken);
        return result.Entity;
    }

    public void Remove(EmailVerificationToken emailVerificationToken)
    {
        _context.EmailVerificationTokens.Remove(emailVerificationToken);
    }
}