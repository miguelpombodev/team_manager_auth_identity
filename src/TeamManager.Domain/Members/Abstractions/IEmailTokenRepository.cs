using TeamManager.Domain.Members.Entities;

namespace TeamManager.Domain.Members.Abstractions;

public interface IEmailTokenRepository
{
    Task<EmailVerificationToken?> RetrieveByIdAsync(Guid id);

    Task<EmailVerificationToken> AddAsync(EmailVerificationToken emailVerificationToken);

    void Remove(EmailVerificationToken emailVerificationToken);
}