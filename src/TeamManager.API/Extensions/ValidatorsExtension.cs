using FluentValidation;
using FluentValidation.AspNetCore;
using TeamManager.Application.Abstractions.Requests.Validators.Auth;

namespace TeamManager.API.Extensions;

public static class ValidatorsExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        
        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

        
        return services;
    }
}