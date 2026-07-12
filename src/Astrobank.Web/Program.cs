using Astrobank.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AstrobankDbContext>(options =>
    options.UseSqlServer(connectionString));

// NOTE: This is solution-foundation scaffolding only (Sprint 1, Commit 1).
// Service registration for Identity, Serilog, FluentValidation,
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
