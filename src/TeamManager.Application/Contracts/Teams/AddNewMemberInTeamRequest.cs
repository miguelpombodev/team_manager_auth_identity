namespace TeamManager.Application.Contracts.Teams;

public record AddNewMemberInTeamRequest(Guid MemberId, Guid TeamId) : IRequest;
