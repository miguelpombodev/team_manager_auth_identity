using Microsoft.Extensions.Logging;
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
    private readonly ILogger<RegisterTeamMemberUseCase> _logger;

    public RegisterTeamMemberUseCase(IMemberRepository memberRepository, ILogger<RegisterTeamMemberUseCase> logger)
    {
        _memberRepository = memberRepository;
        _logger = logger;
    }

    public async Task<Result<ApplicationAuthUser>> ExecuteAsync(RegisterTeamMember request)
    {
        var tryToGetUser = await _memberRepository.RetrieveEntityByEmailAsync(request.Email);

        if (tryToGetUser is not null)
        {
            _logger.LogCritical(
                $"Code: {MembersErrors.MemberAlreadyRegistered.Code} - Description: {MembersErrors.MemberAlreadyRegistered.Description}"
            );
            
            return Result<ApplicationAuthUser>.Failure(MembersErrors.MemberAlreadyRegistered);
        }

        var user = ApplicationAuthUser.Build(request.Email, request.FullName);

        var userComplements = UserComplements.Build(request.FullName);

        var registerUserResult = await _memberRepository.CreateAsync(user, userComplements, request.Password);

        if (registerUserResult.IsFailure)
        {
            _logger.LogCritical(
                $"Code: Member.RegistrationError - Description: {registerUserResult.Error.Description}"
            );
            
            return Result<ApplicationAuthUser>.Failure(
                new Error(
                    "Member.RegistrationError",
                    400,
                    registerUserResult.Error.Description
                ));
        }

        _logger.LogInformation($"New user has been registered. User email: {request.Email}");
        
        return Result<ApplicationAuthUser>.Success(registerUserResult.Data!);
    }
}