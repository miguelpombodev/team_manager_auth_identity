namespace TeamManager.Domain.Entities;

public class UserTeam
{
    public Guid UserId { get; set; } 
    public Guid TeamId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public ApplicationAuthUser User { get; set; } = null!;
    public Team Team { get; set; } = null!;
}