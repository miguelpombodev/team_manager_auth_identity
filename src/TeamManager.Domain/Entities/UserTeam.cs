namespace TeamManager.Domain.Entities;

public class UserTeam
{
    protected UserTeam(Guid userId, Guid teamId, string roleName)
    {
        UserId = userId;
        TeamId = teamId;
        RoleName = roleName;
    }

    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public string RoleName { get; set; }
    public ApplicationAuthUser User { get; set; } = null!;
    public Team Team { get; set; } = null!;

    public static UserTeam Build(Guid userId, Guid teamId, string roleName)
    {
        return new UserTeam(userId, teamId, roleName);
    }
}