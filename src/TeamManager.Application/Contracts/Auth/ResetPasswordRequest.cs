namespace TeamManager.Application.Contracts.Auth;

public record ResetPasswordRequest(string UserEmail, string NewPassword);