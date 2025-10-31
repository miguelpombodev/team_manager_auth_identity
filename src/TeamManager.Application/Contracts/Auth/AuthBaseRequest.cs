namespace TeamManager.Application.Contracts.Auth;

public record AuthBaseRequest(string Email, string Password) : IRequest;
