using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Persistence;

namespace TeamManager.API.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecksServices(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        var connectionStrings = configuration.GetSection("ConnectionStrings")
                                    .Get<ConnectionStringsSettings>() ??
                                throw new InvalidOperationException("Configuração 'ConnectionStrings' não encontrada.");

        if (string.IsNullOrEmpty(connectionStrings.DatabaseConnectionString))
        {
            throw new InvalidOperationException(
                "Configuração 'ConnectionStrings:DatabaseConnectionString' não encontrada ou vazia.");
        }

        if (string.IsNullOrEmpty(connectionStrings.RedisConnectionString))
        {
            throw new InvalidOperationException(
                "Configuração 'ConnectionStrings:RedisConnectionString' não encontrada ou vazia.");
        }

        services.AddHealthChecks()
            .AddNpgSql(
                connectionString: connectionStrings.DatabaseConnectionString,
                name: "Team Manager PostgreSQL DB instance",
                tags: ["db", "data", "sql"]
            )
            .AddRedis(
                connectionStrings.RedisConnectionString,
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