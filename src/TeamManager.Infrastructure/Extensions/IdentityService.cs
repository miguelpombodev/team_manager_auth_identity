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
        services.AddIdentityCore<ApplicationAuthUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = RequiredPasswordLength;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}