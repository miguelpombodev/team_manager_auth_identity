namespace TeamManager.Domain.Entities;

public class Team
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();

    public static Team Build(string name, string? description)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Team name must have a value", nameof(name));
        

        var entity = new Team
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };

        return entity;
    }
}