using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Application.Features.Auth;
using TeamManager.Domain.Entities;

namespace TeamManager.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auth").WithTags("Auth");

        group.MapPost(
            "register",
            async (
                [FromBody] RegisterTeamMember request,
                RegisterTeamMemberUseCase useCase) =>
            {
                var user = await useCase.ExecuteAsync(
                    request
                );

                return Results.Ok(user);
            });

        group.MapPost(
            "login",
            async (
                [FromBody] AuthBaseRequest request,
                LoginTeamMemberUseCase useCase,
                SignInManager<ApplicationAuthUser> signInManager,
                UserManager<ApplicationAuthUser> userManager) =>
            {
                var result = await useCase.ExecuteAsync(request);

                if (result.IsFailure)
                {
                    return Results.Problem(
                        title: result.Error.Code,
                        detail: result.Error.Description,
                        statusCode: result.Error.StatusCode);
                }

                await signInManager.SignInAsync(result.Data.Item2, isPersistent: true);
                await userManager.SetAuthenticationTokenAsync(
                    result.Data.Item2,
                    "TeamManager",
                    "refresh_token",
                    result.Data.Item1.RefreshToken);

                return Results.Ok(result.Data.Item1);
            });
    }
}