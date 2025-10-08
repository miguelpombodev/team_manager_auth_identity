using Microsoft.AspNetCore.Identity;

namespace TeamManager.Domain.Entities;

public class ApplicationAuthUser : IdentityUser<Guid>
{
    public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
}