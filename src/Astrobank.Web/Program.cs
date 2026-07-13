using Astrobank.Application.Interfaces.Identity;
using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Application.Users.Commands.LoginUser;
using Astrobank.Application.Users.Commands.RegisterUser;
using Astrobank.Application.Users.DTOs;
using Astrobank.Domain.Users;
using Astrobank.Infrastructure.Identity;
using Astrobank.Persistence;
using Astrobank.Persistence.Repositories;
using Astrobank.Persistence.Seeding;
using Astrobank.Web.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AstrobankDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Configure ASP.NET Core Identity to use Custom Stores
builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<Role>()
.AddSignInManager<SignInManager<User>>()
.AddDefaultTokenProviders();

// Register Custom Stores explicitly
builder.Services.AddScoped<IUserStore<User>, AstrobankUserStore>();
builder.Services.AddScoped<IRoleStore<Role>, AstrobankRoleStore>();

// Register IIdentityService
builder.Services.AddScoped<IIdentityService, AstrobankIdentityService>();

// Need authentication middleware for SignInManager
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

// Register Application CQRS Handlers
builder.Services.AddScoped<ICommandHandler<RegisterUserCommand, AuthenticationResultDto>, RegisterUserCommandHandler>();
builder.Services.AddScoped<ICommandHandler<LoginUserCommand, AuthenticationResultDto>, LoginUserCommandHandler>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register FluentValidation explicitly with ASP.NET Core UI integration
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// NOTE: Service registration for Serilog, IAstrologyEngine implementations,
// Research, and AI modules will each be added in their own dedicated commit.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidationExceptionFilterAttribute>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Apply migrations and seed data automatically in development
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AstrobankDbContext>();
            context.Database.Migrate();

            var userManager = services.GetRequiredService<UserManager<User>>();
            var configuration = services.GetRequiredService<IConfiguration>();

            await AstrobankDbSeeder.SeedAsync(context, userManager, configuration);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
