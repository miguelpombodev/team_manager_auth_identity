using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManager.Domain.Members.Entities;
using TeamManager.Infrastructure.Persistence.Constants;

namespace TeamManager.Infrastructure.Persistence.Configurations;

internal sealed class EmailVerificationMappingConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.ToTable(TableNames.UserEmailVerificationToken);
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("UUID")
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("UUID")
            .IsRequired();
        
        builder.Property(x => x.CreatedOnUtc)
            .HasColumnName("created_on_utc")
            .HasColumnType("TIMESTAMP WITH TIME ZONE")
            .IsRequired();
        
        builder.Property(x => x.ExpiresOnUtc)
            .HasColumnName("expires_on_utc")
            .HasColumnType("TIMESTAMP WITH TIME ZONE")
            .IsRequired();

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}