using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astrobank.Persistence.Configurations.Users;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.HasKey(c => c.CountryID);

        builder.Property(c => c.CountryID)
            .UseIdentityColumn();

        builder.Property(c => c.CountryName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.ISOCode2)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(c => c.ISOCode3)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(c => c.PhoneCode)
            .HasMaxLength(10);

        // Navigation Properties
        builder.HasMany(c => c.Users)
            .WithOne(u => u.Country)
            .HasForeignKey(u => u.CountryID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
