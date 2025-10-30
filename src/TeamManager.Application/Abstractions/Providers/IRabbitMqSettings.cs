namespace TeamManager.Application.Abstractions.Providers;

public interface IRabbitMqSettings
{
   string RabbitMqHostName { get; set; }
   int RabbitMqPort { get; set; }
   string RabbitMqUserName { get; set; }
   string RabbitMqPassword { get; set; }
}