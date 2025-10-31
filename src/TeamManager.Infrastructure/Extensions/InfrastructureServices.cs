using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Domain.Members.Abstractions;
using TeamManager.Domain.Settings;
using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Email;

namespace TeamManager.Infrastructure.Extensions;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSettingsServices(configuration);

        services.AddPersistenceServices(configuration);

        services.AddCacheServices(configuration);

        services.AddMessageBrokerServices();

        services.AddHttpClientsServices(configuration);

        services.AddAuthenticationServices(configuration);

        return services;
    }

    private static IServiceCollection AddSettingsServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionStringsSettings>(
            configuration.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>() ??
            throw new InvalidOperationException("ConnectionStrings configuration is missing."));

        services.AddSingleton<IRedisSettings>(
            configuration.GetSection("Redis").Get<RedisSettings>() ??
            throw new InvalidOperationException("Redis configuration is missing."));

        services.AddSingleton<IRabbitMqSettings>(
            configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>() ??
            throw new InvalidOperationException("RabbitMqSettings configuration is missing."));
        
        services.AddSingleton<IEmailTemplateFactory, EmailTemplateFactory>();
        
        return services;
    }
}