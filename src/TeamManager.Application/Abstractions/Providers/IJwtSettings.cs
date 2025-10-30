namespace TeamManager.Application.Abstractions.Providers;

public interface IJwtSettings
{
    string Issuer { get; }
    string Audience { get; }
    string Secret { get; }
    int ExpirationInMinutes { get; }
}