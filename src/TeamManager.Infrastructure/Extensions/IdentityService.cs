using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Persistence;

namespace TeamManager.Infrastructure.Extensions;

public static class IdentityService
{
    private const int RequiredPasswordLength = 8;
    public static IServiceCollection AddIdentitySetup(this IServiceCollection services)
    {
        /* The method AddDefaultTokenProviders tried to inject the interface IDataProtectionProvider,
         however to implement this, we need to call the method AddDataProtection. Otherwise we'll have a
         InvalidOperationException error.
         */
        services.AddDataProtection();
        
        services.AddIdentityCore<ApplicationAuthUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = RequiredPasswordLength;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}