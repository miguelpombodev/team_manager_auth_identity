namespace TeamManager.Application.Contracts.Teams;

public record RemoveMemberFromTeamRequest(Guid TeamId, Guid MemberId);