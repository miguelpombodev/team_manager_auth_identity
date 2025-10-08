namespace TeamManager.Domain.Entities;

public class UserTeam
{
    public Guid UserId { get; set; } 
    public Guid TeamId { get; set; }

    public ApplicationAuthUser User { get; set; } = null!;
    public Team Team { get; set; } = null!;
}