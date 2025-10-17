using FluentValidation;
using TeamManager.Application.Abstractions.Requests.Auth;
using TeamManager.Application.Abstractions.Requests.Teams;

namespace TeamManager.Application.Abstractions.Requests.Validators.Teams;

public class RegisterTeamRequestValidator : AbstractValidator<RegisterTeam>
{
    private const string AvoidSpecialCharactersInTeamName = "@\"^(?!'+$)[a-zA-Z']+(?:\\s+[a-zA-Z']+)*$\"";

    public RegisterTeamRequestValidator()
    {
        RuleFor(x => x.TeamName).NotEmpty()
            .WithMessage("Team name field cannot be empty")
            .NotNull()
            .WithMessage("Team name field cannot be null");
    }
};