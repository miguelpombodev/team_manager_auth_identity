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
            .HasColumnName("id")
            .HasColumnType("UUID")
            .IsRequired();

        builder.Property(au => au.UserName)
            .HasColumnName("username")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.NormalizedUserName)
            .HasColumnName("normalized_username")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.Email)
            .HasColumnName("email")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.NormalizedEmail)
            .HasColumnName("normalized_email")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder.Property(au => au.PasswordHash)
            .HasColumnName("password_hash")
            .HasColumnType("TEXT")
            .IsRequired(false);

        builder.Property(au => au.SecurityStamp)
            .HasColumnName("security_stamp")
            .HasColumnType("TEXT")
            .IsRequired(false);

        builder.Property(au => au.ConcurrencyStamp)
            .HasColumnName("concurrency_stamp")
            .HasColumnType("CHAR(36)")
            .IsRequired(false);

        builder.Property(au => au.TwoFactorEnabled)
            .HasColumnName("f2a_enabled")
            .HasColumnType("BOOL")
            .IsRequired();
        
        builder.Property(au => au.EmailConfirmed)
            .HasColumnName("email_confirmed")
            .HasColumnType("BOOL")
            .IsRequired();
        
        builder.Property(au => au.PhoneNumber)
            .HasColumnName("phone_number")
            .HasColumnType("VARCHAR(100)")
            .IsRequired();
        
        builder.Property(au => au.PhoneNumberConfirmed)
            .HasColumnName("phone_number_confirmed")
            .HasColumnType("BOOL")
            .IsRequired();

        builder.Property(au => au.LockoutEnd)
            .HasColumnName("lockout_end")
            .HasColumnType("TIMESTAMPTZ");

        builder.Property(au => au.LockoutEnabled)
            .HasColumnName("lockout_enabled")
            .HasColumnType("BOOL")
            .IsRequired();

        builder.Property(au => au.AccessFailedCount)
            .HasColumnName("access_failed_count")
            .HasColumnType("INT4")
            .IsRequired();

        builder.HasIndex(au => new { au.Id, au.Email, au.UserName, au.NormalizedUserName, au.NormalizedEmail })
            .IsUnique();
    }
}