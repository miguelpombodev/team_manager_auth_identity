using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Persistence.Constants;

namespace TeamManager.Infrastructure.Persistence.Configurations;

public class UserTeamMappingConfiguration : IEntityTypeConfiguration<UserTeam>
{
    public void Configure(EntityTypeBuilder<UserTeam> builder)
    {
        builder.ToTable(TableNames.UserTeams);

        builder.HasKey(ut => new { ut.UserId, ut.TeamId });
        
        builder.Property(ut => ut.RoleName)
            .HasColumnName("role_name")
            .HasColumnType("VARCHAR(50)")
            .IsRequired();

        builder.HasOne(ut => ut.User).WithMany(user => user.UserTeams).HasForeignKey(ut => ut.UserId);
        builder.HasOne(ut => ut.Team).WithMany(team => team.UserTeams).HasForeignKey(ut => ut.TeamId);
    }
}