using TeamManager.Application.Abstractions.Features;

namespace TeamManager.Application.Abstractions.Requests.Auth;

public record RegisterTeamMember(string FullName, string Email, string Password) : IRequest;