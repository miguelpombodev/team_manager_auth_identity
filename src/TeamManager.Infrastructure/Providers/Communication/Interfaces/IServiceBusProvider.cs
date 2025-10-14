using RoyalRent.Domain.Common.Entities;

namespace TeamManager.Infrastructure.Providers.Communication.Interfaces;

public interface IServiceBusProvider
{
    Task<ServiceBusSendMessageResult> SendMessage(ServiceBusNotification notification);
}
