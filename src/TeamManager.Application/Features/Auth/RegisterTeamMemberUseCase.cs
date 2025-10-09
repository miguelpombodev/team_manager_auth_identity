using Microsoft.AspNetCore.Identity;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Domain.Entities;

namespace TeamManager.Application.Features.Auth;

public class RegisterTeamMemberUseCase : IUseCase<RegisterTeamMember,ApplicationAuthUser>
{
    private readonly UserManager<ApplicationAuthUser> _userManager;

    public RegisterTeamMemberUseCase(UserManager<ApplicationAuthUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationAuthUser> ExecuteAsync(RegisterTeamMember request)
    {
        var user = new ApplicationAuthUser
        {
            Id = Guid.NewGuid(),
            UserName = RenameUserNameByUserFullName(request.FullName),
            Email = request.Email,
            PhoneNumber = "0000000000"
        };

        IdentityResult identityResult =  await _userManager.CreateAsync(user);

        await _userManager.AddToRoleAsync(user, Roles.TeamMember);
        return user;
    }

    private string RenameUserNameByUserFullName(string fullName)
    {
        var random = new Random();
        var randomStringfiedNumber = random.Next(100, 100000).ToString();
        
        
        return $"{fullName.Replace(" ", "_").ToLower()}_{randomStringfiedNumber}";
    }
}