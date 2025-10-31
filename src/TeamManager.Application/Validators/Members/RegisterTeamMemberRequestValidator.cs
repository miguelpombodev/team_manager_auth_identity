using FluentValidation;
using TeamManager.Application.Contracts.Auth;

namespace TeamManager.Application.Validators.Members;

public class RegisterTeamMemberRequestValidator : AbstractValidator<RegisterTeamMember>
{
    private const string AvoidSpecialCharactersInFullName = "^(?!'+$)[a-zA-Z\\s']+$";
    
    public RegisterTeamMemberRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty()
            .WithMessage("Fullname field cannot be empty")
            .NotNull()
            .WithMessage("Fullname field cannot be null")
            .Matches(AvoidSpecialCharactersInFullName)
            .WithMessage("Fullname is invalid");

    }
}