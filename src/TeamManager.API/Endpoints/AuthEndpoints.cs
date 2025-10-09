using Microsoft.AspNetCore.Mvc;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Application.Features.Auth;

namespace TeamManager.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auth").WithTags("Auth");
        
        group.MapPost("register", async ([FromBody] RegisterTeamMember request, RegisterTeamMemberUseCase useCase) =>
        {
            var user = await useCase.ExecuteAsync(request);

            return Results.Ok(user);
        });
    }
}