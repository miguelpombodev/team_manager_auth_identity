using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Domain.Entities;
using TeamManager.Infrastructure.Persistence.Constants;

namespace TeamManager.Infrastructure.Persistence.Configurations;

public class ApplicationUserMappingConfiguration : IEntityTypeConfiguration<ApplicationAuthUser>
{
    public void Configure(EntityTypeBuilder<ApplicationAuthUser> builder)
    {
        builder.ToTable(TableNames.ApplicationUser);
        builder.HasKey(au => au.Id);


        builder.Property(au => au.Id)
            .HasColumnName("Id")
            .HasColumnType("UUID")
            .IsRequired();

        builder.Property(au => au.UserName)
            .HasColumnName("UserName")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.NormalizedUserName)
            .HasColumnName("NormalizedUserName")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.Email)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.NormalizedEmail)
            .HasColumnName("NormalizedEmail")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("TEXT")
            .IsRequired(false);

        builder.Property(au => au.SecurityStamp)
            .HasColumnName("SecurityStamp")
            .HasColumnType("TEXT")
            .IsRequired(false);

        builder.Property(au => au.ConcurrencyStamp)
            .HasColumnName("ConcurrencyStamp")
            .HasColumnType("CHAR(36)")
            .IsRequired(false);

        builder.Property(au => au.TwoFactorEnabled)
            .HasColumnName("TwoFactorEnabled")
            .HasColumnType("BOOL")
            .IsRequired();
        
        builder.Property(au => au.EmailConfirmed)
            .HasColumnName("EmailConfirmed")
            .HasColumnType("BOOL")
            .IsRequired();
        
        builder.Property(au => au.PhoneNumber)
            .HasColumnName("PhoneNumber")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
        
        builder.Property(au => au.PhoneNumberConfirmed)
            .HasColumnName("PhoneNumberConfirmed")
            .HasColumnType("BOOL")
            .IsRequired();

        builder.Property(au => au.LockoutEnd)
            .HasColumnName("LockoutEnd")
            .HasColumnType("TIMESTAMPTZ");

        builder.Property(au => au.LockoutEnabled)
            .HasColumnName("LockoutEnabled")
            .HasColumnType("BOOL")
            .IsRequired();

        builder.Property(au => au.AccessFailedCount)
            .HasColumnName("AccessFailedCount")
            .HasColumnType("INT4")
            .IsRequired();
        
        builder.HasIndex(au => new { au.Id, au.Email, au.UserName, au.NormalizedUserName, au.NormalizedEmail })
            .IsUnique();
    }
}