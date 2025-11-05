using Microsoft.Extensions.Logging;
using TeamManager.Application.Contracts.Teams;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Persistence;
using TeamManager.Domain.Teams.Errors;

namespace TeamManager.Application.Features.Teams;

public class AddNewMemberInTeamUseCase : IUseCase<AddNewMemberInTeamRequest, Result<UserTeam>>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<AddNewMemberInTeamUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AddNewMemberInTeamUseCase(
        ITeamRepository teamRepository,
        ILogger<AddNewMemberInTeamUseCase> logger,
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork
    )
    {
        _teamRepository = teamRepository;
        _logger = logger;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserTeam>> ExecuteAsync(AddNewMemberInTeamRequest request)
    {
        var user = await _memberRepository.RetrieveEntityByIdAsync(request.MemberId);

        if (user is null)
        {
            _logger.LogWarning(
                "[WARNING] The sent request UserId does not belongs to a registered member | UseCase - {UseCaseName}; Properties - {UseCaseProperties}",
                nameof(AddNewMemberInTeamUseCase),
                new
                {
                    request.MemberId, request.TeamId
                }
            );
            return Result<UserTeam>.Failure(AuthenticationErrors.UserAccountNotFound);
        }

        var team = await _teamRepository.RetrieveTeamByIdAsync(request.TeamId);

        if (team is null)
        {
            _logger.LogWarning(
                "[WARNING] The sent request TeamId does not belongs to a registered member | UseCase - {UseCaseName}; Properties - {UseCaseProperties}",
                nameof(AddNewMemberInTeamUseCase),
                new
                {
                    request.MemberId, request.TeamId
                }
            );
            return Result<UserTeam>.Failure(TeamErrors.TeamNotFound);
        }

        var userTeam = UserTeam.Build(request.MemberId, request.TeamId, request.RoleName);

        var entityResult = await _teamRepository.CreateUserTeamAsync(userTeam);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation(
            "[CREATED] A new UserTeam has been created successfully - UserTeam - {UserTeamProperties}",
            new
            {
                UserInsertedInTeam = userTeam.UserId,
                Team = userTeam.TeamId
            }
        );

        return Result<UserTeam>.Success(entityResult);
    }
}