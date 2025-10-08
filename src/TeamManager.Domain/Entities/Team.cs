namespace TeamManager.Domain.Entities;

public class Team
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
}