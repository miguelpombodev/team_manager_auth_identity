using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamManager.Domain.Common.Abstraction.Communication;
using TeamManager.Infrastructure.Providers.Communication;
using TeamManager.Infrastructure.Providers.Communication.Interfaces;

namespace TeamManager.Infrastructure.Extensions;

public static class MessageBrokerServices
{
    public static IServiceCollection AddMessageBrokerServices(
        this IServiceCollection services
    )
    {
        services.AddSingleton<IRabbitMqConnection, RabbitMqPersistentConnection>();
        services.AddSingleton<IHostedService, RabbitMqConnectionHostedService>();

        services.AddScoped<IServiceBusProvider, ServiceBusProvider>();

        return services;
    }
}