using FluentValidation;
using TeamManager.Application.Contracts.Teams;

namespace TeamManager.Application.Validators.Teams;

public class AddNewMemberInTeamRequestValidator : AbstractValidator<AddNewMemberInTeamRequest>
{
    public AddNewMemberInTeamRequestValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member Id cannot be empty").NotNull()
            .WithMessage("Member Id cannot be null");

        RuleFor(x => x.TeamId).NotEmpty().WithMessage("Team Id cannot be empty").NotNull()
            .WithMessage("Team Id cannot be null");
    }
}