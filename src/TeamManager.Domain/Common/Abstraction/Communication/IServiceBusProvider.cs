using RoyalRent.Domain.Common.Entities;
using TeamManager.Domain.Providers.Communication;

namespace TeamManager.Domain.Common.Abstraction.Communication;

public interface IServiceBusProvider
{
    Task<ServiceBusSendMessageResult> SendMessage(ServiceBusNotification notification);
}
