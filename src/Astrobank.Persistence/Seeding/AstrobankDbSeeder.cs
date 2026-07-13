using Astrobank.Domain.Users;
using Astrobank.Domain.Users.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Astrobank.Persistence.Seeding;

public static class AstrobankDbSeeder
{
    public static async Task SeedAsync(AstrobankDbContext context, UserManager<User> userManager, IConfiguration configuration)
    {
        // 1. Seed Roles
        var rolesToSeed = new[]
        {
            new { Name = "SystemAdmin", IsSystemRole = true },
            new { Name = "Administrator", IsSystemRole = true },
            new { Name = "VerifiedAstrologer", IsSystemRole = false },
            new { Name = "StandardUser", IsSystemRole = false }
        };

        foreach (var roleData in rolesToSeed)
        {
            if (!await context.Roles.AnyAsync(r => r.Name == roleData.Name))
            {
                var role = new Role
                {
                    Name = roleData.Name,
                    IsSystemRole = roleData.IsSystemRole
                };
                role.SetCreatedOn(DateTime.UtcNow);

                context.Roles.Add(role);
            }
        }
        await context.SaveChangesAsync();

        // 2. Seed Default Admin User
        var adminUsername = "admin";
        var adminEmail = "admin@astrobank.local";

        if (!await context.Users.AnyAsync(u => u.Username == adminUsername))
        {
            var systemAdminRole = await context.Roles.FirstAsync(r => r.Name == "SystemAdmin");

            var adminUser = new User
            {
                Name = "System Administrator",
                Email = adminEmail,
                Username = adminUsername
            };

            // Use domain methods to configure the user cleanly
            adminUser.Activate();
            adminUser.VerifyAstrologer();
            adminUser.AssignRole(systemAdminRole.RoleID);
            adminUser.SetCreatedOn(DateTime.UtcNow);

            var adminPassword = configuration["SeedData:AdminPassword"];
            if (string.IsNullOrEmpty(adminPassword))
            {
                throw new InvalidOperationException("SeedData:AdminPassword configuration is missing.");
            }

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to seed admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
