using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace TeamManager.Infrastructure.Configurations;

public sealed class AppSettings
{
    private static AppSettings? _instance;
    private static readonly object Lock = new();

    public JwtSettings Jwt { get; init; } = new();
    public ConnectionStringsSettings ConnectionStrings { get; init; } = new();
    public RedisSettings RedisSettings { get; init; } = new();

    private AppSettings() { }

    public static void Initialize(IConfiguration configuration)
    {
        if (_instance is not null)
            return;

        lock (Lock)
        {
            if (_instance is not null)
                return;

            var jwt = configuration.GetSection("Jwt").Get<JwtSettings>() ?? throw new InvalidOperationException("Missing Jwt configuration.");
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>() ?? throw new InvalidOperationException("Missing ConnectionStrings configuration.");
            var redisOptions = configuration.GetSection("Redis").Get<RedisSettings>() ?? throw new InvalidOperationException("Missing Redis configuration.");
            IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production";

            Validate(jwt);
            Validate(connectionStrings);
            Validate(redisOptions);

            _instance = new AppSettings
            {
                Jwt = jwt,
                ConnectionStrings = connectionStrings,
                RedisSettings = redisOptions
            };
        }
    }

    public static AppSettings Current =>
        _instance ?? throw new InvalidOperationException("AppSettings not initialized. Call AppSettings.Initialize(configuration) during startup.");

    private static void Validate(object instance)
    {
        var context = new ValidationContext(instance);
        Validator.ValidateObject(instance, context, validateAllProperties: true);
    }

    public static bool IsDevelopment { get; set; }
    public static string DatabaseConnectionString => Current.ConnectionStrings.DatabaseConnectionString;
    public static string RedisConnectionString => Current.ConnectionStrings.RedisConnectionString;
    public static string JwtSecret => Current.Jwt.Secret;
    public static string JwtIssuer => Current.Jwt.Issuer;
    public static string JwtAudience => Current.Jwt.Audience;
    public static int JwtExpirationInMinutes => Current.Jwt.ExpirationInMinutes;
    public static string RedisInstanceName => Current.RedisSettings.RedisInstanceName;
}

public class JwtSettings
{
    [Required] public string Issuer { get; set; } = string.Empty;
    [Required] public string Audience { get; set; } = string.Empty;
    [Required] public string Secret { get; set; } = string.Empty;
    [Range(1, 1440)] public int ExpirationInMinutes { get; set; }
}

public class ConnectionStringsSettings
{
    [Required] public string DatabaseConnectionString { get; set; } = string.Empty;
    [Required] public string RedisConnectionString { get; set; } = string.Empty;
}

public class RedisSettings
{
    [Required] public string RedisInstanceName { get; set; } = string.Empty;
}