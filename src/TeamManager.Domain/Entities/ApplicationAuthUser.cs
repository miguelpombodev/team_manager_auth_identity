using Microsoft.AspNetCore.Identity;

namespace TeamManager.Domain.Entities;

public class ApplicationAuthUser : IdentityUser<Guid>
{
    public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
    public ICollection<IdentityRole<Guid>> UserRoles { get; set; } = new List<IdentityRole<Guid>>();

    public static ApplicationAuthUser Build(string email, string fullName)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("User email must have a value", nameof(email));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("User full name must have a value", nameof(fullName));

        var entity = new ApplicationAuthUser();
        entity.Id = Guid.NewGuid();
        entity.Email = email;
        entity.SetRenamedUserNameByUserFullName(fullName);
        entity.PhoneNumber = "0000000000";

        return entity;
    }

    private void SetRenamedUserNameByUserFullName(string fullName)
    {
        var random = new Random();
        var randomStringfiedNumber = random.Next(100, 100000).ToString();

        UserName = $"{fullName.Replace(" ", "_").ToLower()}_{randomStringfiedNumber}";
    }
}