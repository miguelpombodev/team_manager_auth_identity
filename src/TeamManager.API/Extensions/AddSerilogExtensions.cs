using Serilog;

namespace TeamManager.API.Extensions;

public static class AddSerilogExtensions
{
    public static IApplicationBuilder AddSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }

    public static ConfigureHostBuilder ConfigureSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });

        return host;
    }
}