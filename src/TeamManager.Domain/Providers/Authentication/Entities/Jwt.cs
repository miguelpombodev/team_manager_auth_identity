namespace TeamManager.Domain.Providers.Authentication.Entities;

public class Jwt
{
    public string Secret { get; }
    public string Issuer { get; }
    public string Audience { get; }
    public int ExpirationInMinutes { get; }
    
    protected Jwt(string secret, string issuer, string audience, int expirationInMinutes)
    {
        Secret = secret;
        Issuer = issuer;
        Audience = audience;
        ExpirationInMinutes = expirationInMinutes;
    }
    
    public static Jwt Create(Dictionary<string, string?> jwtConfiguration)
    {
        return new Jwt(
            jwtConfiguration["Secret"]!,
            jwtConfiguration["Issuer"]!,
            jwtConfiguration["Audience"]!,
            Convert.ToInt32(jwtConfiguration["ExpirationInMinutes"])
        );
    }
}