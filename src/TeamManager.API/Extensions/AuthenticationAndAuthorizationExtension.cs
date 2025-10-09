namespace TeamManager.API.Extensions;

public static class AuthenticationAndAuthorizationExtension
{
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services)
    {
        services.AddAuthentication();
        services.AddAuthorization();
        
        return services;
    }
}