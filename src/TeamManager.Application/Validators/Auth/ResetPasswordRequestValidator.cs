using FluentValidation;
using TeamManager.Application.Contracts.Auth;

namespace TeamManager.Application.Validators.Auth;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.UserEmail)
            .NotEmpty()
            .WithMessage("Password field cannot be empty")
            .NotNull()
            .WithMessage("Password field cannot be null")
            .EmailAddress()
            .WithMessage("Invalid email address");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Password field cannot be empty")
            .NotNull()
            .WithMessage("Password field cannot be null")
            .Length(8, 100)
            .WithMessage("Password field must have between 8 to 100 characters");
    }
}