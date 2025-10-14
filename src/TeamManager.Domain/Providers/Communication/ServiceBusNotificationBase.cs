using System.Text;

namespace RoyalRent.Domain.Common.Entities;

public abstract class ServiceBusNotification
{
    protected ServiceBusNotification(string queueName, string exchangeName, string routingKeyName, string body)
    {
        QueueName = queueName;
        ExchangeName = exchangeName;
        RoutingKeyName = routingKeyName;
        Body = Encoding.UTF8.GetBytes(body);
    }

    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
    public string RoutingKeyName { get; set; }
    public ReadOnlyMemory<byte> Body { get; set; }
}
