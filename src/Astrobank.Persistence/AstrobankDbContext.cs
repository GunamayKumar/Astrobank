using Astrobank.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Astrobank.Persistence;

public class AstrobankDbContext : DbContext
{
    public AstrobankDbContext(DbContextOptions<AstrobankDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Country> Countries => Set<Country>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AstrobankDbContext).Assembly);
    }
}
