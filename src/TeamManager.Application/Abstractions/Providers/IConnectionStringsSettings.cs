namespace TeamManager.Application.Abstractions.Providers;

public interface IConnectionStringsSettings
{
    string DatabaseConnectionString { get; }
    string RedisConnectionString { get; }
}