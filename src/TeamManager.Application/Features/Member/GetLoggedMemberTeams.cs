using TeamManager.Domain.Common.Abstraction;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Members.Abstractions;

namespace TeamManager.Application.Features.Member;

public class GetLoggedMemberTeams : IUseCase<Guid, Result<List<Team>>>
{
    private readonly ITeamRepository _teamRepository;

    public GetLoggedMemberTeams(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Result<List<Team>>> ExecuteAsync(Guid request)
    {
        var userTeams = await _teamRepository.RetrieveTeamsByMemberIdAsync(request);

        if (userTeams is null)
        {
            return Result<List<Team>>.Success([]);
        }

        return Result<List<Team>>.Success(userTeams);
    }
}