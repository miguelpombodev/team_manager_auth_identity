using Microsoft.AspNetCore.Identity;

namespace TeamManager.Domain.Members.Entities;

public record UserTeamRoleDto(Guid TeamId, string RoleName);