using TeamManager.Infrastructure.Persistence;

namespace TeamManager.API.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecksServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddHealthChecks()
            .AddNpgSql(
                connectionString: configuration.GetConnectionString("TeamManager")!,
                name: "Team Manager PostgreSQL DB instance",
                tags: ["db", "data", "sql"]
            )
            .AddRedis(
                configuration.GetConnectionString("RedisConnection")!,
                name: "Team Manager Redis instance",
                tags: ["db", "data", "nosql"]
            );

        return services;
    }
}