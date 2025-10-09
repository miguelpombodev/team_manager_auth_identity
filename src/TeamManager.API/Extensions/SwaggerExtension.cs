using Microsoft.OpenApi.Models;

namespace TeamManager.API.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });

            var filePath = Path.Combine(System.AppContext.BaseDirectory, "TeamManager.Presentation.xml");

            config.IncludeXmlComments(filePath);
            config.UseInlineDefinitionsForEnums();
        });
        return services;
    }

    public static WebApplication AddSwagger(this WebApplication app)
    {
        app.MapOpenApi();

        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}