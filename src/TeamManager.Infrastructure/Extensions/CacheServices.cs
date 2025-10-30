using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TeamManager.Domain.Providers.Cache;
using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Providers.Cache;

namespace TeamManager.Infrastructure.Extensions;

public static class CacheServices
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringSettings = configuration.GetSection("ConnectionStrings")
            .Get<ConnectionStringsSettings>() ?? throw new InvalidOperationException(
            "ConnectionStrings configuration is missing.");

        var redisSettings = configuration.GetSection("Redis")
            .Get<RedisSettings>() ?? throw new InvalidOperationException("Redis configuration is missing.");
        
        var redisConnection = ConnectionMultiplexer.Connect(connectionStringSettings.RedisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(redisConnection);
        
        services.AddStackExchangeRedisCache(x =>
            {
                x.Configuration = connectionStringSettings.RedisConnectionString;
                x.InstanceName = redisSettings.RedisInstanceName;
            }
        );
        
        services.AddSingleton<IDistribuitedCacheProvider, DistribuitedCacheProvider>();
        
        return services;
    }
}