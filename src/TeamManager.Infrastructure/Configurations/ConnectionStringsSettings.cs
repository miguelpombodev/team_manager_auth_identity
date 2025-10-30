using TeamManager.Application.Abstractions.Providers;

namespace TeamManager.Infrastructure.Configurations;

public class ConnectionStringsSettings : IConnectionStringsSettings
{
    public string DatabaseConnectionString { get; set; }
    public string RedisConnectionString { get; set; }
}