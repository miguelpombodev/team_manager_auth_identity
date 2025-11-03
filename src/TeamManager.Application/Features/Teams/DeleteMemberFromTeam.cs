using Microsoft.Extensions.Logging;
using TeamManager.Application.Contracts.Teams;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Members.Errors;
using TeamManager.Domain.Providers.Persistence;
using TeamManager.Domain.Teams.Errors;

namespace TeamManager.Application.Features.Teams;

public class DeleteMemberFromTeam : IUseCase<RemoveMemberFromTeamRequest, Result>
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<DeleteMemberFromTeam> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteMemberFromTeam(ITeamRepository teamRepository, IMemberRepository memberRepository, ILogger<DeleteMemberFromTeam> logger, IUnitOfWork unitOfWork)
    {
        _teamRepository = teamRepository;
        _memberRepository = memberRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(RemoveMemberFromTeamRequest request)
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
            return Result.Failure(AuthenticationErrors.UserAccountNotFound);
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
            return Result.Failure(TeamErrors.TeamNotFound);
        }

        var deleteResult = await _teamRepository.RemoveMemberFromTeamAsync(request.MemberId, request.TeamId);

        return deleteResult == 0 ? Result.Failure(TeamErrors.TeamNotAbleToBeDelete) : Result.Success();
    }
}