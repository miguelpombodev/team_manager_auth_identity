using TeamManager.Application.Abstractions.Providers;

namespace TeamManager.Infrastructure.Configurations;

public class RedisSettings : IRedisSettings
{
    public string RedisInstanceName { get; set; } = string.Empty;
}