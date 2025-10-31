using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManager.Application.Contracts.Teams;
using TeamManager.Application.Features.Teams;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Entities;

namespace TeamManager.API.Endpoints;

public static class TeamsEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("teams").WithTags("Teams").WithDescription("Teams Endpoints");

        group.MapPost("/create", async (
                [FromBody] RegisterTeam request,
                RegisterTeamUseCase useCase,
                IAuthorizationService authService,
                ClaimsPrincipal user
            ) =>
            {
                await authService.AuthorizeAsync(user, AuthPolicies.CanCreateTeam);
                
                var result = await useCase.ExecuteAsync(
                    request
                );

                if (result.IsFailure)
                {
                    return Results.Problem(
                        title: result.Error.Code,
                        detail: result.Error.Description,
                        statusCode: result.Error.StatusCode);
                }

                return Results.Created();
            })
            .AddEndpointFilter<ValidationFilter<RegisterTeam>>()
            .WithSummary("Register a new team")
            .WithDescription(
                "Tries to create a new team, if its name is not already taken")
            .Produces<Team>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}