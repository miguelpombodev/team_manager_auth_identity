using System.Text;
using System.Text.Json;

namespace TeamManager.Domain.Providers.Communication;

public abstract class ServiceBusNotification
{
    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
    public string RoutingKeyName { get; set; }
    public ReadOnlyMemory<byte> Body { get; set; }
    
    protected ServiceBusNotification(string queueName, string exchangeName, string routingKeyName, string body)
    {
        QueueName = queueName;
        ExchangeName = exchangeName;
        RoutingKeyName = routingKeyName;
        Body = Encoding.UTF8.GetBytes(body);
    }
}
