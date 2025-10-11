using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TeamManager.Domain.Providers.Authentication.Abstractions;
using TeamManager.Infrastructure.Persistence;
using TeamManager.Infrastructure.Providers;

namespace TeamManager.Infrastructure.Extensions;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("TeamManager")));
        
        services.Scan(x => x.FromAssemblies(
                Infrastructure.AssemblyReference.Assembly
            )
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}