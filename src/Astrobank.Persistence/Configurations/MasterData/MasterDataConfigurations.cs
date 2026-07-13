using Astrobank.Domain.MasterData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astrobank.Persistence.Configurations.MasterData;

public class ChartTypeConfiguration : IEntityTypeConfiguration<ChartType> {
    public void Configure(EntityTypeBuilder<ChartType> builder) {
        builder.ToTable("ChartTypes"); builder.HasKey(e => e.ChartTypeID); builder.Property(e => e.ChartTypeID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200); builder.Property(e => e.Description).HasMaxLength(500);
    }
}
public class ChartPermissionConfiguration : IEntityTypeConfiguration<ChartPermission> {
    public void Configure(EntityTypeBuilder<ChartPermission> builder) {
        builder.ToTable("ChartPermissions"); builder.HasKey(e => e.ChartPermissionID); builder.Property(e => e.ChartPermissionID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200); builder.Property(e => e.Description).HasMaxLength(500);
    }
}
public class EventTypeConfiguration : IEntityTypeConfiguration<EventType> {
    public void Configure(EntityTypeBuilder<EventType> builder) {
        builder.ToTable("EventTypes"); builder.HasKey(e => e.EventTypeID); builder.Property(e => e.EventTypeID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200); builder.Property(e => e.Description).HasMaxLength(500); builder.Property(e => e.Category).HasMaxLength(100);
    }
}
public class HelpCategoryConfiguration : IEntityTypeConfiguration<HelpCategory> {
    public void Configure(EntityTypeBuilder<HelpCategory> builder) {
        builder.ToTable("HelpCategories"); builder.HasKey(e => e.HelpCategoryID); builder.Property(e => e.HelpCategoryID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200); builder.Property(e => e.Description).HasMaxLength(500);
    }
}
public class TagCategoryConfiguration : IEntityTypeConfiguration<TagCategory> {
    public void Configure(EntityTypeBuilder<TagCategory> builder) {
        builder.ToTable("TagCategories"); builder.HasKey(e => e.TagCategoryID); builder.Property(e => e.TagCategoryID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200); builder.Property(e => e.Description).HasMaxLength(500);
    }
}
public class TagConfiguration : IEntityTypeConfiguration<Tag> {
    public void Configure(EntityTypeBuilder<Tag> builder) {
        builder.ToTable("Tags"); builder.HasKey(e => e.TagID); builder.Property(e => e.TagID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200); builder.Property(e => e.Description).HasMaxLength(500);
        builder.HasOne(t => t.TagCategory).WithMany().HasForeignKey(t => t.TagCategoryID).OnDelete(DeleteBehavior.Restrict);
    }
}
public class ChartImageTypeConfiguration : IEntityTypeConfiguration<ChartImageType> {
    public void Configure(EntityTypeBuilder<ChartImageType> builder) {
        builder.ToTable("ChartImageTypes"); builder.HasKey(e => e.ChartImageTypeID); builder.Property(e => e.ChartImageTypeID).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200); builder.Property(e => e.Description).HasMaxLength(500);
    }
}
