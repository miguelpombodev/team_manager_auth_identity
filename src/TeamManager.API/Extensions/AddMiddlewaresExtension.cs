using TeamManager.API.Middlewares;

namespace TeamManager.API.Extensions;

public static class AddMiddlewaresExtension
{
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ResponseMiddleware>();
        
        return app;
    }
}