using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Infrastructure.Configurations;

namespace TeamManager.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        AppSettings.Initialize(configuration);

        return services;
    }
}