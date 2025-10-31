using FluentValidation;
using TeamManager.Application.Contracts.Teams;

namespace TeamManager.Application.Validators.Teams;

public class RegisterTeamRequestValidator : AbstractValidator<RegisterTeam>
{
    private const string AvoidSpecialCharactersInTeamName = "^(?!'+$)[a-zA-Z\\s']+$";

    public RegisterTeamRequestValidator()
    {
        RuleFor(x => x.TeamName).NotEmpty()
            .WithMessage("Team name field cannot be empty")
            .NotNull()
            .WithMessage("Team name field cannot be null")
            .Matches(AvoidSpecialCharactersInTeamName)
            .WithMessage("Fullname is invalid");
    }
};