using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Persistence;

namespace TeamManager.API.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecksServices(
        this IServiceCollection services
    )
    {
        services.AddHealthChecks()
            .AddNpgSql(
                connectionString: AppSettings.DatabaseConnectionString,
                name: "Team Manager PostgreSQL DB instance",
                tags: ["db", "data", "sql"]
            )
            .AddRedis(
                AppSettings.RedisConnectionString,
                name: "Team Manager Redis instance",
                tags: ["db", "data", "nosql"]
            );

        services.AddHealthChecksUI(x =>
            {
                // Defines the interval to trigger the services verification
                x.SetEvaluationTimeInSeconds(5);

                // Defines a max registries value that will be allowed in history 
                x.MaximumHistoryEntriesPerEndpoint(10);

                // Defines the HealthCheck endpoint
                x.AddHealthCheckEndpoint("HealthCheck Endpoint", "/api/health");
            })
            .AddInMemoryStorage();

        return services;
    }
}