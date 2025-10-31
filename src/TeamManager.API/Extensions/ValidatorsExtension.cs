using FluentValidation;
using FluentValidation.AspNetCore;

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