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
        builder.HasKey(uc => uc.Id);

        builder.HasIndex(uc => uc.PublicId).IsUnique();

        builder.Property(uc => uc.Id).HasColumnName("id").UseIdentityByDefaultColumn();
        builder.Property(uc => uc.PublicId).HasColumnName("public_id").HasColumnType("UUID").IsRequired();
        builder.Property(uc => uc.FullName).HasColumnName("fullname").HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(uc => uc.Initials).HasColumnName("initials").HasColumnType("CHAR(2)").IsRequired();
        builder.Property(uc => uc.UserId).HasColumnName("user_id").HasColumnType("UUID").IsRequired();

        builder.HasOne(x => x.User).WithOne(x => x.UserComplements).HasForeignKey<UserComplements>(x => x.UserId);
    }
}