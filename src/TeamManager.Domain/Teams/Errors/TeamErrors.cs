using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Teams.Errors;

public static class TeamErrors
{
    public static readonly Error TeamNotFound = new Error(
        "Team.TeamNotFound",
        404,
        "Team not found");
    
    public static readonly Error TeamNotAbleToBeDelete = new Error(
        "Team.TeamNotAbleToBeDelete",
        409,
        "Team could not be deleted");
}