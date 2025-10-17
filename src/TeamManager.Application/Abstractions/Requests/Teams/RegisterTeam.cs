using TeamManager.Application.Abstractions.Features;

namespace TeamManager.Application.Abstractions.Requests.Teams;

public record RegisterTeam(string TeamName) : IRequest;