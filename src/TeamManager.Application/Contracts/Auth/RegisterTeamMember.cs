namespace TeamManager.Application.Contracts.Auth;

public record RegisterTeamMember(string FullName, string Email, string Password) : AuthBaseRequest(Email, Password);