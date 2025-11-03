using TeamManager.Application.Contracts.Teams;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Teams.Errors;

namespace TeamManager.Application.Features.Teams;

public class RegisterTeamUseCase : IUseCase<RegisterTeamRequest, Result<Team>>
{
    private readonly ITeamRepository _teamRepository;

    public RegisterTeamUseCase(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<Team>> ExecuteAsync(RegisterTeamRequest request)
    {
        var team = await _teamRepository.RetrieveTeamByNameAsync(request.TeamName);

        if (team is not null)
        {
            return Result<Team>.Failure(TeamErrors.TeamAlreadyExists);
        }
        
        var buildedTeam = Team.Build(request.TeamName, request.Description);

        var createdTeam = await _teamRepository.Create(buildedTeam);

        return Result<Team>.Success(createdTeam);
    }
}