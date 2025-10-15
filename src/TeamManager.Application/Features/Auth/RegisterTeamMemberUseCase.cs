using Microsoft.Extensions.Logging;
using TeamManager.Application.Abstractions.Features;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Common.Abstraction.Communication;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Entities;
using TeamManager.Domain.Members.Errors;

namespace TeamManager.Application.Features.Auth;

public class RegisterTeamMemberUseCase : IUseCase<RegisterTeamMember, Result<ApplicationAuthUser>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<RegisterTeamMemberUseCase> _logger;
    private readonly IServiceBusProvider _serviceBusProvider;

    public RegisterTeamMemberUseCase(
        IMemberRepository memberRepository, 
        ILogger<RegisterTeamMemberUseCase> logger,
        IServiceBusProvider serviceBusProvider
        )
    {
        _memberRepository = memberRepository;
        _logger = logger;
        _serviceBusProvider = serviceBusProvider;
    }

    public async Task<Result<ApplicationAuthUser>> ExecuteAsync(RegisterTeamMember request)
    {
        var tryToGetUser = await _memberRepository.RetrieveEntityByEmailAsync(request.Email);

        if (tryToGetUser is not null)
        {
            _logger.LogCritical(
                "Code: {Code} - Description: {Description}",
                MembersErrors.MemberAlreadyRegistered.Code,
                MembersErrors.MemberAlreadyRegistered.Description
            );

            return Result<ApplicationAuthUser>.Failure(MembersErrors.MemberAlreadyRegistered);
        }

        var user = ApplicationAuthUser.Build(request.Email, request.FullName);

        var userComplements = UserComplements.Build(request.FullName);

        var registerUserResult = await _memberRepository.CreateAsync(user, userComplements, request.Password);

        if (registerUserResult.IsFailure)
        {
            _logger.LogCritical(
                "Code: Member.RegistrationError - Description: {Description}",
                registerUserResult.Error.Description
            );

            return Result<ApplicationAuthUser>.Failure(
                new Error("Member.RegistrationError", 400, registerUserResult.Error.Description));
        }

        _logger.LogInformation("New user has been registered. User email: {UserEmail}", request.Email);
        
        // Create factory for member notification
        var memberRegisteredEmailNotification = new MemberCreatedEmailNotification(request.Email);

        await _serviceBusProvider.SendMessage(memberRegisteredEmailNotification);

        return Result<ApplicationAuthUser>.Success(registerUserResult.Data!);
    }
}