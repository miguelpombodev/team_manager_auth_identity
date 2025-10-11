using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;

namespace TeamManager.Application.Features.Auth;

public class RegisterTeamMemberUseCase : IUseCase<RegisterTeamMember, ApplicationAuthUser>
{
    private readonly IMemberRepository _memberRepository;

    public RegisterTeamMemberUseCase(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }


    public async Task<ApplicationAuthUser> ExecuteAsync(RegisterTeamMember request)
    {
        var user = ApplicationAuthUser.Build(request.Email, request.FullName);

        var userComplements = UserComplements.Build(request.FullName);

        var registerUserResult = await _memberRepository.CreateAsync(user, userComplements, request.Password);

        return registerUserResult;
    }
}