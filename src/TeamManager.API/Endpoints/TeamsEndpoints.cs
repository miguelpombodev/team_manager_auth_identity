using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManager.Application.Contracts.Teams;
using TeamManager.Application.Features.Member;
using TeamManager.Application.Features.Teams;
using TeamManager.Domain.Common.Auth;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Providers.Authentication.Abstractions;

namespace TeamManager.API.Endpoints;

public static class TeamsEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("teams").WithTags("Teams").WithDescription("Teams Endpoints");

        group.MapPost("/", async (
                [FromBody] RegisterTeamRequest request,
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
            .AddEndpointFilter<ValidationFilter<RegisterTeamRequest>>()
            .WithSummary("Register a new team")
            .WithDescription(
                "Tries to create a new team, if its name is not already taken")
            .Produces<Team>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/", async (
                ICurrentUserService currentUserService,
                GetLoggedMemberTeams useCase
            ) =>
            {
                var user = await currentUserService.GetCurrentUserOrThrow();

                if (user.IsFailure)
                {
                    return Results.Problem(
                        title: user.Error.Code,
                        detail: user.Error.Description,
                        statusCode: user.Error.StatusCode);
                }

                var result = await useCase.ExecuteAsync(user.Data.Id);

                if (result.IsFailure)
                {
                    return Results.Problem(
                        title: result.Error.Code,
                        detail: result.Error.Description,
                        statusCode: result.Error.StatusCode);
                }

                return Results.Ok(result.Data);
            })
            .RequireAuthorization()
            .WithName("GetLoggedMemberTeams")
            .WithSummary("List Teams of a specific member")
            .WithDescription(
                "List Teams of a specific member")
            .Produces<Team>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/{id:guid}/members", async (
                [FromBody] AddNewMemberInTeamRequest request,
                IAuthorizationService authService,
                ClaimsPrincipal user,
                AddNewMemberInTeamUseCase useCase
            ) =>
            {
                await authService.AuthorizeAsync(
                    user,
                    AuthPolicies.CanManageTeam
                );

                var result = await useCase.ExecuteAsync(request);

                if (result.IsFailure)
                {
                    return Results.Problem(
                        title: result.Error.Code,
                        detail: result.Error.Description,
                        statusCode: result.Error.StatusCode);
                }

                return Results.Created();
            })
            .AddEndpointFilter<ValidationFilter<AddNewMemberInTeamRequest>>()
            .RequireAuthorization()
            .WithName("AddNewMemberInTeam")
            .WithSummary("Add a specific member into Team")
            .WithDescription(
                "Add a specific member into Team")
            .Produces<Team>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{id:guid}/members/{memberId:guid}", async (
                Guid teamId,
                Guid memberId,
                IAuthorizationService authService,
                ClaimsPrincipal user,
                DeleteMemberFromTeam useCase
            ) =>
            {
                await authService.AuthorizeAsync(
                    user,
                    AuthPolicies.CanManageTeam
                );

                var request = new RemoveMemberFromTeamRequest(teamId, memberId);
                var result = await useCase.ExecuteAsync(request);

                if (result.IsFailure)
                {
                    return Results.Problem(
                        title: result.Error.Code,
                        detail: result.Error.Description,
                        statusCode: result.Error.StatusCode);
                }

                return Results.NoContent();
            })
            .RequireAuthorization()
            .WithName("DeleteMemberFromTeam")
            .WithSummary("Remove a specific member from Team")
            .WithDescription(
                "Remove a specific member from Team")
            .Produces<Team>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}