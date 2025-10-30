using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;
using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Persistence;

namespace TeamManager.Infrastructure.Extensions;

public static class PersistenceServices
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionStringSettings = configuration.GetSection("ConnectionStrings")
            .Get<ConnectionStringsSettings>() ?? throw new InvalidOperationException(
            "ConnectionStrings configuration is missing.");

        services.AddDbContext<ApplicationDbContext>(x =>
            x.UseNpgsql(connectionStringSettings.DatabaseConnectionString));
        services.Scan(x => x.FromAssemblies(
                Infrastructure.AssemblyReference.Assembly
            )
            .AddClasses(
                classes => classes.Where(type => !type.IsAssignableTo(typeof(IHostedService)
                    )
                ),
                false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}