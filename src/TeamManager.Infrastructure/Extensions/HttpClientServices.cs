using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Application.Abstractions.Providers;
using TeamManager.Infrastructure.Configurations;

namespace TeamManager.Infrastructure.Extensions;

public static class HttpClientServices
{
    public static IServiceCollection AddHttpClientsServices(this IServiceCollection services, IConfiguration configuration)
    {
        var strapiSettings = new StrapiSettings();
        configuration.GetSection("HttpClients:StrapiSettings").Bind(strapiSettings);

        if (string.IsNullOrEmpty(strapiSettings.StrapiApiUrl) || string.IsNullOrEmpty(strapiSettings.StrapiClientName))
        {
            throw new InvalidOperationException(
                "Configurações do Strapi (StrapiApiUrl, StrapiClientName) não encontradas ou vazias.");
        }
        services.AddSingleton<IStrapiSettings>(strapiSettings);
        

        services.AddHttpClient(strapiSettings.StrapiClientName, client => { client.BaseAddress = new Uri(strapiSettings.StrapiApiUrl); });

        return services;
    }
}