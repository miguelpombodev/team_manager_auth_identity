using TeamManager.Domain.Common.Abstraction;

namespace TeamManager.Domain.Teams.Errors;

public static class TeamErrors
{
    public static readonly Error TeamNotFound = new Error(
        "Team.TeamNotFound",
        404,
        "Team not found");
}