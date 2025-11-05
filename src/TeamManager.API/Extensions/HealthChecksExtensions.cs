using TeamManager.Infrastructure.Configurations;
using HealthChecks.UI.Configuration;
using System;

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
        
        return services;
    }
}