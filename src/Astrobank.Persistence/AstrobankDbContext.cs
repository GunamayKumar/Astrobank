using Astrobank.Domain.Charts;
using Astrobank.Domain.MasterData;
using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Astrobank.Persistence;

public class AstrobankDbContext : DbContext
{
    public AstrobankDbContext(DbContextOptions<AstrobankDbContext> options)
        : base(options)
    {
    }

    // Users Module
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Country> Countries => Set<Country>();

    // Master Data Module
    public DbSet<ChartType> ChartTypes => Set<ChartType>();
    public DbSet<ChartPermission> ChartPermissions => Set<ChartPermission>();
    public DbSet<EventType> EventTypes => Set<EventType>();
    public DbSet<HelpCategory> HelpCategories => Set<HelpCategory>();
    public DbSet<TagCategory> TagCategories => Set<TagCategory>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ChartImageType> ChartImageTypes => Set<ChartImageType>();

    // Charts Module
    public DbSet<Chart> Charts => Set<Chart>();
    public DbSet<ChartImage> ChartImages => Set<ChartImage>();
    public DbSet<ChartFile> ChartFiles => Set<ChartFile>();
    public DbSet<ChartTag> ChartTags => Set<ChartTag>();
    public DbSet<ChartEvent> ChartEvents => Set<ChartEvent>();
    public DbSet<ChartAccess> ChartAccesses => Set<ChartAccess>();
    public DbSet<ChartAuditLog> ChartAuditLogs => Set<ChartAuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AstrobankDbContext).Assembly);
    }
}
