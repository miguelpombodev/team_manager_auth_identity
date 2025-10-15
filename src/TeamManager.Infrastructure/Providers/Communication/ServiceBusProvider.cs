using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RoyalRent.Domain.Common.Entities;
using TeamManager.Domain.Common.Abstraction.Communication;
using TeamManager.Domain.Providers.Communication;
using TeamManager.Infrastructure.Providers.Communication.Interfaces;

namespace TeamManager.Infrastructure.Providers.Communication;

public class ServiceBusProvider : IServiceBusProvider
{
    private readonly ILogger<ServiceBusProvider> _logger;

    private readonly IRabbitMqConnection _connection;

    private readonly BasicProperties _props = new BasicProperties
    {
        Persistent = true,
        ContentType = "application/json",
        MessageId = Guid.NewGuid().ToString(),
        Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
    };

    public ServiceBusProvider(ILogger<ServiceBusProvider> logger, IRabbitMqConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public async Task<ServiceBusSendMessageResult> SendMessage(ServiceBusNotification notification)
    {
        try
        {
            await using var channelAsync = await _connection.CreateChannelAsync();
            
            await channelAsync.BasicPublishAsync(
                exchange: notification.ExchangeName,
                routingKey: notification.RoutingKeyName,
                true,
                basicProperties: _props,
                body: notification.Body);

            _logger.LogInformation(
                "Email Notification was send at {SendDateTime} to queue {QueueName}",
                DateTime.UtcNow,
                notification.QueueName);

            return new ServiceBusSendMessageResult { Success = true, Message = "Message sent successfully" };
        }
        catch (BrokerUnreachableException exc)
        {
            _logger.LogError(
                exc,
                "Host name {ConnectionFactoryHostName} could be reach, please check connection",
                _connection);

            return new ServiceBusSendMessageResult { Message = $"Broker unreachable: {exc.Message}" };
        }
        catch (OperationInterruptedException exc)
        {
            _logger.LogError(exc, "Operation interrupted: {Message}", exc.Message
            );

            return new ServiceBusSendMessageResult { Message = $"Operation interrupted: {exc.Message}" };
        }

        catch (Exception exc)
        {
            _logger.LogError(exc, "Unexpected error while sending message");
            return new ServiceBusSendMessageResult
            {
                Message = $"Unexpected error while sending message: {exc.Message}"
            };
        }
    }
}