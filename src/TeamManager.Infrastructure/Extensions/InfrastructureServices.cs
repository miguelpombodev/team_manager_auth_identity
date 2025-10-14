using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;
using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Persistence;
using StackExchange.Redis;
using TeamManager.Domain.Providers.Cache;
using TeamManager.Infrastructure.Providers.Cache;
using TeamManager.Infrastructure.Providers.Communication;
using TeamManager.Infrastructure.Providers.Communication.Interfaces;

namespace TeamManager.Infrastructure.Extensions;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(AppSettings.DatabaseConnectionString));
        services.AddSingleton<IDistribuitedCacheProvider, DistribuitedCacheProvider>();

        var redisConnection = ConnectionMultiplexer.Connect(AppSettings.RedisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(redisConnection);

        services.AddStackExchangeRedisCache(x =>
            {
                x.Configuration = AppSettings.RedisConnectionString;
                x.InstanceName = AppSettings.RedisInstanceName;
            }
        );

        services.AddSingleton<IRabbitMqConnection, RabbitMqPersistentConnection>();

        services.AddSingleton<IHostedService, RabbitMqConnectionHostedService>();

        services.AddScoped<IServiceBusProvider, ServiceBusProvider>();


        services.Scan(x => x.FromAssemblies(
                Infrastructure.AssemblyReference.Assembly
            )
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}