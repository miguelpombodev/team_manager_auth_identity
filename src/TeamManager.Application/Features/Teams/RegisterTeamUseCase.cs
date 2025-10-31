using TeamManager.Application.Contracts.Teams;
using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;

namespace TeamManager.Application.Features.Teams;

public class RegisterTeamUseCase : IUseCase<RegisterTeam, Result<Team>>
{
    private readonly ITeamRepository _teamRepository;

    public RegisterTeamUseCase(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<Team>> ExecuteAsync(RegisterTeam request)
    {
        var team = Team.Build(request.TeamName);

        var createdTeam = await _teamRepository.Create(team);

        return Result<Team>.Success(createdTeam);
    }
}