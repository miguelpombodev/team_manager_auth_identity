using TeamManager.Domain.Entities;

namespace TeamManager.Domain.Members.Entities;

public sealed class EmailVerificationToken
{
    protected EmailVerificationToken()
    {
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ExpiresOnUtc { get; set; }

    public ApplicationAuthUser User { get; set; }

    public static EmailVerificationToken Build(Guid userId)
    {
        if (userId.Equals(Guid.Empty))
            throw new ArgumentException("User id must have a value", nameof(userId));

        var now = DateTime.UtcNow;
        var entity = new EmailVerificationToken();

        entity.UserId = userId;
        entity.Id = Guid.NewGuid();
        entity.CreatedOnUtc = now;
        entity.ExpiresOnUtc = now.AddHours(5);

        return entity;
    }
}