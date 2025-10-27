using TeamManager.API.Generator;

namespace TeamManager.API.Extensions;

public static class AddApiExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<EmailVerificationLinkFactory>();
        services.AddHttpContextAccessor();
        
        
        return services;
        
    }
}