using TeamManager.Domain.Settings;

namespace TeamManager.Infrastructure.Configurations;

public class ConnectionStringsSettings : IConnectionStringsSettings
{
    public string DatabaseConnectionString { get; set; }
    public string RedisConnectionString { get; set; }
}