using TeamManager.Application.Contracts.Teams;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Teams.DTOs;
using TeamManager.Domain.Teams.Errors;

namespace TeamManager.Application.Features.Teams;

public class GetTeamsDetails : IUseCase<Guid, Result<TeamDetailsDto>>
{
    private readonly ITeamRepository _teamRepository;

    public GetTeamsDetails(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<TeamDetailsDto>> ExecuteAsync(Guid teamId)
    {
        var queryResult = await _teamRepository.RetrieveTeamDetails(teamId);

        if (queryResult is null)
        {
            return Result<TeamDetailsDto>.Failure(TeamErrors.TeamNotFound);
        }

        return Result<TeamDetailsDto>.Success(queryResult);
    }
}