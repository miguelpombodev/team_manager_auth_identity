namespace TeamManager.Infrastructure.Providers.Communication;

public class RabbitMqOptions
{
    public string? HostName { get; set; }
    public int Port { get; set; } = 5672;
    public string? Username { get; set; }
    public string? Password { get; set; }
}
