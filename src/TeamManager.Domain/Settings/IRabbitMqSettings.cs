namespace TeamManager.Domain.Settings;

public interface IRabbitMqSettings
{
   string RabbitMqHostName { get; set; }
   int RabbitMqPort { get; set; }
   string RabbitMqUserName { get; set; }
   string RabbitMqPassword { get; set; }
}