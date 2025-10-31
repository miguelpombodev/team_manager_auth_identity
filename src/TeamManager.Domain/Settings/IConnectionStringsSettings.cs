namespace TeamManager.Domain.Settings;

public interface IConnectionStringsSettings
{
    string DatabaseConnectionString { get; }
    string RedisConnectionString { get; }
}