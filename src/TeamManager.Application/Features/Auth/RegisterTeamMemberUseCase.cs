using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Errors;

namespace TeamManager.Application.Features.Auth;

public class RegisterTeamMemberUseCase : IUseCase<RegisterTeamMember, Result<ApplicationAuthUser>>
{
    private readonly IMemberRepository _memberRepository;

    public RegisterTeamMemberUseCase(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }


    public async Task<Result<ApplicationAuthUser>> ExecuteAsync(RegisterTeamMember request)
    {
        var tryToGetUser = await _memberRepository.RetrieveEntityByEmailAsync(request.Email);

        if (tryToGetUser is not null)
        {
            return Result<ApplicationAuthUser>.Failure(MembersErrors.MemberAlreadyRegistered);
        }

        var user = ApplicationAuthUser.Build(request.Email, request.FullName);

        var userComplements = UserComplements.Build(request.FullName);

        var registerUserResult = await _memberRepository.CreateAsync(user, userComplements, request.Password);

        if (registerUserResult.IsFailure)
        {
            return Result<ApplicationAuthUser>.Failure(
                new Error(
                    "Member.RegistrationError",
                    400,
                    registerUserResult.Error.Description
                ));
        }

        return Result<ApplicationAuthUser>.Success(registerUserResult.Data!);
    }
}