using FluentValidation;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.Arguments.OfType<T>().FirstOrDefault();

        if (model is null)
        {
            return Results.BadRequest(new { error = "Request body is required" });
        }

        var result = await _validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => new
            {
                Field = e.PropertyName,
                Error = e.ErrorMessage
            });

            return Results.BadRequest(new { Errors = errors });
        }

        return await next(context);
    }
}