namespace TeamManager.Domain.Teams.DTOs;

public record TeamMemberDto(Guid UserId, string? Email, string FullName, string RoleInTeam);

public record TeamDetailsDto(string TeamName, string TeamDescription, List<TeamMemberDto> TeamMember);