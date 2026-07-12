using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astrobank.Persistence.Configurations.Users;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(rp => rp.RolePermissionID);

        builder.Property(rp => rp.RolePermissionID)
            .UseIdentityColumn();

        // Unique constraint on (RoleID, PermissionID)
        builder.HasIndex(rp => new { rp.RoleID, rp.PermissionID })
            .IsUnique();
    }
}
