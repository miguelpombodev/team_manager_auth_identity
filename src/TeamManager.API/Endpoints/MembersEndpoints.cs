namespace TeamManager.API.Endpoints;

public static class MembersEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("users").WithTags("Members");
    }
}