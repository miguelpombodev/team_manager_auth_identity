using System.Text.RegularExpressions;
using FluentValidation;
using TeamManager.Application.Abstractions.Requests.Auth;

namespace TeamManager.Application.Abstractions.Requests.Validators.Members;

public class RegisterTeamMemberRequestValidator : AbstractValidator<RegisterTeamMember>
{
    private const string AvoidSpecialCharactersInFullName = "@\"^(?!'+$)[a-zA-Z']+(?:\\s+[a-zA-Z']+)*$\"";
    
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