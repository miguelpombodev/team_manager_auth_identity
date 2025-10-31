using TeamManager.Domain.Settings;

namespace TeamManager.Infrastructure.Configurations;

public class RedisSettings : IRedisSettings
{
    public string RedisInstanceName { get; set; } = string.Empty;
}