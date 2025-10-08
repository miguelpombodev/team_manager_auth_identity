using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Persistence.Constants;

namespace TeamManager.Infrastructure.Persistence.Configurations;

public class TeamMappingConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable(TableNames.Teams);

        builder.Property(t => t.Id).HasColumnName("id").HasColumnType("UUID").IsRequired();
        builder.Property(t => t.Name).HasColumnName("team_name").HasColumnType("VARCHAR(100)").IsRequired();
        
        builder.HasIndex(t => t.Name).IsUnique();
    }
}