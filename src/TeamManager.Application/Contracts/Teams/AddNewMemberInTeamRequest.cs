using System.Text.Json.Serialization;

namespace TeamManager.Application.Contracts.Teams;

public record AddNewMemberInTeamRequest: IRequest
{
    public Guid MemberId { get; init; }
    public string RoleName { get; init; } = string.Empty;

    [JsonIgnore] 
    public Guid TeamId { get; init; }
    
    public AddNewMemberInTeamRequest() { }
    public AddNewMemberInTeamRequest(Guid memberId, Guid teamId, string roleName)
    {
        MemberId = memberId;
        TeamId = teamId;
        RoleName = roleName;
    }
}
