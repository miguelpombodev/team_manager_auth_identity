using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Persistence.Constants;

namespace TeamManager.Infrastructure.Persistence.Configurations;

public class UserComplementsMappingConfiguration : IEntityTypeConfiguration<UserComplements>
{
    public void Configure(EntityTypeBuilder<UserComplements> builder)
    {
        builder.ToTable(TableNames.UserComplements);
        builder.HasKey(uc => new { uc.Id, uc.PublicId });

        builder.Property(uc => uc.Id).HasColumnName("id").UseIdentityByDefaultColumn();
        builder.Property(uc => uc.PublicId).HasColumnName("public_id").HasColumnType("UUID").IsRequired();
        builder.Property(uc => uc.FullName).HasColumnName("fullname").HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(uc => uc.Initials).HasColumnName("initials").HasColumnType("CHAR(2)").IsRequired();
    }
}