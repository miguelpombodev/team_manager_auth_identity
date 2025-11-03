namespace TeamManager.Application.Contracts.Teams;

public record RegisterTeamRequest(string TeamName, string? Description) : IRequest;