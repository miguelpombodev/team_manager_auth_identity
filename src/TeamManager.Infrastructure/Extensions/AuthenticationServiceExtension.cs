using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Providers.Authentication.Abstractions;
using TeamManager.Domain.Settings;
using TeamManager.Infrastructure.Configurations;
using TeamManager.Infrastructure.Providers.Security;

namespace TeamManager.Infrastructure.Extensions;

public static class AuthenticationServiceExtension
{
    public const string IgnoreExpirationScheme = "BearerIgnoreExpiration";

    public static IServiceCollection AddAuthenticationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetSection("Jwt")
                              .Get<JwtSettings>() ??
                          throw new InvalidOperationException("Jwt configuration is missing.");

        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };

        var jwtTokenEvents = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var tokenFromCookie = context.HttpContext.Request.Cookies["access_token"];
                if (!string.IsNullOrEmpty(tokenFromCookie))
                {
                    context.Token = tokenFromCookie;
                }

                return Task.CompletedTask;
            }
        };

        services.AddSingleton<IJwtSettings>(jwtSettings);
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<Jwt>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie.IsEssential = true;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParams;

                options.Events = jwtTokenEvents;
            })
            .AddJwtBearer(IgnoreExpirationScheme, options =>
            {
                options.TokenValidationParameters = tokenValidationParams.Clone();
                options.TokenValidationParameters.ValidateLifetime = false;
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicies.CanRefresh, policy =>
            {
                policy.AddAuthenticationSchemes(IgnoreExpirationScheme);
                policy.RequireAuthenticatedUser();
            });
        });

        return services;
    }
}