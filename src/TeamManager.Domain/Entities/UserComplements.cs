namespace TeamManager.Domain.Entities;

public class UserComplements
{
    public int Id { get; protected set; }
    public Guid PublicId { get; protected set; }
    public string FullName { get; protected set; } = string.Empty;
    public string Initials { get; protected set; } = string.Empty;
    
    public Guid UserId { get; protected set; } = Guid.NewGuid();
    public ApplicationAuthUser User { get; protected set; }

    protected UserComplements() { }
    
    public static UserComplements Build(string fullName, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("User full name must have a value", nameof(fullName));

        var entity = new UserComplements();
        entity.PublicId = Guid.NewGuid();
        entity.SetFullName(fullName);
        entity.SetInitialByFullName(fullName);
        entity.UserId = userId;

        return entity;
    }

    private void SetFullName(string fullName)
    {
        FullName = fullName;
    }

    private void SetInitialByFullName(string fullName)
    {
        var nameParts = fullName.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

        Initials = string.Concat(nameParts.Take(2).Select(x => char.ToUpper(x[0])));
    }
}