using FluentValidation;
using TeamManager.Application.Contracts.Auth;

namespace TeamManager.Application.Validators.Auth;

public class LoginTeamMemberRequestValidator : AbstractValidator<AuthBaseRequest>
{
    public LoginTeamMemberRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email field cannot be empty").EmailAddress()
            .WithMessage("Email field must be a valid email");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password field cannot be empty").NotNull()
            .WithMessage("Password field cannot be null")
            .Length(8, 100)
            .WithMessage("Password field must have between 8 to 100 characters");
    }
}