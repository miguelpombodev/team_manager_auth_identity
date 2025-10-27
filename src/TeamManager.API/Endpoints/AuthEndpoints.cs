using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamManager.API.Generator;
using TeamManager.Application.Abstractions.Requests;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Application.Features.Auth;
using TeamManager.Application.Features.Member;
using TeamManager.Domain.Entities;
using TeamManager.Domain.Providers.Authentication.Entities;

namespace TeamManager.API.Endpoints;

public static class AuthEndpoints
{
    public const string VerifyEmailEndpointName = "VerifyEmail";

    private const string LoginProvider = "TeamManager";
    private const string TokenName = "refresh_token";

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("auth").WithTags("Auth");

        group.MapPost(
                "register",
                async (
                    [FromBody] RegisterTeamMember request,
                    RegisterTeamMemberUseCase useCase,
                    SendEmailVerificationUseCase sendEmailVerificationUseCase,
                    EmailVerificationLinkFactory emailVerificationLinkFactory) =>
                {
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

                    var linkVerification = emailVerificationLinkFactory.Create(result.Data!);

                    var memberValidationLinkAndToken = new MemberValidationLinkAndToken(linkVerification, result.Data);
                    await sendEmailVerificationUseCase.ExecuteAsync(memberValidationLinkAndToken);

                    return Results.Created();
                })
            .AddEndpointFilter<ValidationFilter<RegisterTeamMember>>()
            .WithSummary("Register a new member")
            .WithDescription(
                "Tries to create a new member, if its email is not already taken")
            .Produces<AuthResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost(
                "login",
                async (
                    [FromBody] AuthBaseRequest request,
                    IValidator<AuthBaseRequest> validator,
                    LoginTeamMemberUseCase useCase,
                    SignInManager<ApplicationAuthUser> signInManager,
                    UserManager<ApplicationAuthUser> userManager) =>
                {
                    var validationResult = await validator.ValidateAsync(request);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => new
                        {
                            Field = e.PropertyName,
                            Error = e.ErrorMessage
                        });

                        return Results.BadRequest(new { Errors = errors });
                    }

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
                        LoginProvider,
                        TokenName,
                        result.Data.Item1.RefreshToken);

                    return Results.Ok(result.Data.Item1);
                })
            .AddEndpointFilter<ValidationFilter<AuthBaseRequest>>()
            .WithSummary("Login Member and return a cookie")
            .WithDescription(
                "Tries to login a possible member, if successful return a token access and a refresh token, and also set a cookie")
            .Produces<AuthResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("confirm-email", async (Guid token, ConfirmMemberEmailUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(token);

                if (result.IsFailure)
                {
                    return Results.Problem(
                        title: result.Error.Code,
                        detail: result.Error.Description,
                        statusCode: result.Error.StatusCode);
                }

                return Results.Ok();
            }).WithName(VerifyEmailEndpointName)
            .WithSummary("Confirm user's email")
            .WithDescription(
                "This endpoint confirms user's email by token in email sent after user is registered.")
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}