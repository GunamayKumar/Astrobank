using Astrobank.Domain.Charts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astrobank.Persistence.Configurations.Charts;

public class ChartConfiguration : IEntityTypeConfiguration<Chart> {
    public void Configure(EntityTypeBuilder<Chart> builder) {
        builder.ToTable("Charts"); builder.HasKey(e => e.ChartID); builder.Property(e => e.ChartID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(256); builder.Property(e => e.Alias).HasMaxLength(256);
        builder.Property(e => e.BirthPlace).IsRequired().HasMaxLength(500); builder.Property(e => e.Timezone).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(4000); builder.Property(e => e.Gender).HasConversion<int>();
        builder.Property(e => e.ChartStatus).HasConversion<int>().IsRequired();
        builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.ChartType).WithMany().HasForeignKey(e => e.ChartTypeID).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.ChartPermission).WithMany().HasForeignKey(e => e.ChartPermissionID).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Country).WithMany().HasForeignKey(e => e.CountryID).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.HelpCategory).WithMany().HasForeignKey(e => e.HelpCategoryID).OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(e => e.ChartImages).WithOne(e => e.Chart).HasForeignKey(e => e.ChartID).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.ChartFiles).WithOne(e => e.Chart).HasForeignKey(e => e.ChartID).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.ChartEvents).WithOne(e => e.Chart).HasForeignKey(e => e.ChartID).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.ChartTags).WithOne(e => e.Chart).HasForeignKey(e => e.ChartID).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.ChartAccesses).WithOne(e => e.Chart).HasForeignKey(e => e.ChartID).OnDelete(DeleteBehavior.Cascade);
    }
}
public class ChartImageConfiguration : IEntityTypeConfiguration<ChartImage> {
    public void Configure(EntityTypeBuilder<ChartImage> builder) {
        builder.ToTable("ChartImages"); builder.HasKey(e => e.ChartImageID); builder.Property(e => e.ChartImageID).UseIdentityColumn();
        builder.Property(e => e.OriginalFileName).IsRequired().HasMaxLength(500); builder.Property(e => e.StoredFileName).IsRequired().HasMaxLength(500);
        builder.Property(e => e.RelativePath).IsRequired().HasMaxLength(1000); builder.Property(e => e.MimeType).IsRequired().HasMaxLength(100);
        builder.HasOne(e => e.ChartImageType).WithMany().HasForeignKey(e => e.ChartImageTypeID).OnDelete(DeleteBehavior.Restrict);
    }
}
public class ChartFileConfiguration : IEntityTypeConfiguration<ChartFile> {
    public void Configure(EntityTypeBuilder<ChartFile> builder) {
        builder.ToTable("ChartFiles"); builder.HasKey(e => e.ChartFileID); builder.Property(e => e.ChartFileID).UseIdentityColumn();
        builder.Property(e => e.OriginalFileName).IsRequired().HasMaxLength(500); builder.Property(e => e.StoredFileName).IsRequired().HasMaxLength(500);
        builder.Property(e => e.RelativePath).IsRequired().HasMaxLength(1000); builder.Property(e => e.MimeType).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(1000);
    }
}
public class ChartTagConfiguration : IEntityTypeConfiguration<ChartTag> {
    public void Configure(EntityTypeBuilder<ChartTag> builder) {
        builder.ToTable("ChartTags"); builder.HasKey(e => e.ChartTagID); builder.Property(e => e.ChartTagID).UseIdentityColumn();
        builder.HasOne(e => e.Tag).WithMany().HasForeignKey(e => e.TagID).OnDelete(DeleteBehavior.Restrict);
    }
}
public class ChartEventConfiguration : IEntityTypeConfiguration<ChartEvent> {
    public void Configure(EntityTypeBuilder<ChartEvent> builder) {
        builder.ToTable("ChartEvents"); builder.HasKey(e => e.ChartEventID); builder.Property(e => e.ChartEventID).UseIdentityColumn();
        builder.Property(e => e.EventDate).IsRequired().HasMaxLength(100); builder.Property(e => e.Description).HasMaxLength(2000);
        builder.Property(e => e.ReferenceSource).HasMaxLength(500);
        builder.HasOne(e => e.EventType).WithMany().HasForeignKey(e => e.EventTypeID).OnDelete(DeleteBehavior.Restrict);
    }
}
public class ChartAccessConfiguration : IEntityTypeConfiguration<ChartAccess> {
    public void Configure(EntityTypeBuilder<ChartAccess> builder) {
        builder.ToTable("ChartAccess"); builder.HasKey(e => e.ChartAccessID); builder.Property(e => e.ChartAccessID).UseIdentityColumn();
        builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.Cascade);
    }
}
public class ChartAuditLogConfiguration : IEntityTypeConfiguration<ChartAuditLog> {
    public void Configure(EntityTypeBuilder<ChartAuditLog> builder) {
        builder.ToTable("ChartAuditLogs"); builder.HasKey(e => e.ChartAuditLogID); builder.Property(e => e.ChartAuditLogID).UseIdentityColumn();
        builder.Property(e => e.Action).IsRequired().HasMaxLength(100);
        builder.HasOne(e => e.Chart).WithMany().HasForeignKey(e => e.ChartID).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.ModifiedByUser).WithMany().HasForeignKey(e => e.ModifiedByUserID).OnDelete(DeleteBehavior.Restrict);
    }
}
