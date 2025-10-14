using RabbitMQ.Client;

namespace TeamManager.Infrastructure.Providers.Communication.Interfaces;

public interface IRabbitMqConnection
{
    Task<IChannel> CreateChannelAsync();

}
