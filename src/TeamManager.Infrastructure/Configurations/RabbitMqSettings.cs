using TeamManager.Domain.Settings;

namespace TeamManager.Infrastructure.Configurations;

public class RabbitMqSettings : IRabbitMqSettings
{
    public string RabbitMqHostName { get; set; } = string.Empty;
    public int RabbitMqPort { get; set; }
    public string RabbitMqUserName { get; set; } = string.Empty;
    public string RabbitMqPassword { get; set; } = string.Empty;
}