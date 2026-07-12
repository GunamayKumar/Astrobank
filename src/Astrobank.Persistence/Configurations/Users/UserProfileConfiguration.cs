using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astrobank.Persistence.Configurations.Users;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");

        builder.HasKey(up => up.UserProfileID);

        builder.Property(up => up.UserProfileID)
            .UseIdentityColumn();

        builder.Property(up => up.DisplayName)
            .HasMaxLength(256);

        builder.Property(up => up.Biography)
            .HasMaxLength(2000);

        builder.Property(up => up.Qualification)
            .HasMaxLength(500);

        builder.Property(up => up.Website)
            .HasMaxLength(256);

        builder.Property(up => up.ConsultationCharges)
            .HasColumnType("decimal(18,2)");
    }
}
