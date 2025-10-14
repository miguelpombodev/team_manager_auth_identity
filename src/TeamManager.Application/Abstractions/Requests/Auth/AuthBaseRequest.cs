using TeamManager.Application.Abstractions.Features;

namespace TeamManager.Application.Abstractions.Requests.Auth;

public record AuthBaseRequest(string Email, string Password) : IRequest;
