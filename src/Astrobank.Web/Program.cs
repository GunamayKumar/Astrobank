using Astrobank.Application.Interfaces.Identity;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Users;
using Astrobank.Infrastructure.Identity;
using Astrobank.Persistence;
using Astrobank.Persistence.Repositories;
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

// NOTE: This is solution-foundation scaffolding only (Sprint 1, Commit 1).
// Service registration for Serilog, FluentValidation,
// AutoMapper, IAstrologyEngine implementations, Research, and AI modules will each be
// added in their own dedicated commit, as those modules are assigned.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
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
