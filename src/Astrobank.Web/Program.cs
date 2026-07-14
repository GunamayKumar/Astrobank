using Astrobank.Application.Common.Models;
using Astrobank.Application.Charts.DTOs;
using Astrobank.Application.Charts.Queries.ListCharts;
using Astrobank.Application.Charts.Commands.UpdateChart;
using Astrobank.Application.Charts.Commands.CreateChart;
using Astrobank.Persistence.Repositories;
using Astrobank.Application.MasterData.Countries.DTOs;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.MasterData.Countries.Commands.UpdateCountry;
using Astrobank.Application.MasterData.Countries.Commands.CreateCountry;
using Astrobank.Application.MasterData.TagCategorys.DTOs;
using Astrobank.Application.MasterData.TagCategorys.Queries.ListTagCategorys;
using Astrobank.Application.MasterData.TagCategorys.Commands.UpdateTagCategory;
using Astrobank.Application.MasterData.TagCategorys.Commands.CreateTagCategory;
using Astrobank.Application.MasterData.HelpCategorys.DTOs;
using Astrobank.Application.MasterData.HelpCategorys.Queries.ListHelpCategorys;
using Astrobank.Application.MasterData.HelpCategorys.Commands.UpdateHelpCategory;
using Astrobank.Application.MasterData.HelpCategorys.Commands.CreateHelpCategory;
using Astrobank.Application.MasterData.EventTypes.DTOs;
using Astrobank.Application.MasterData.EventTypes.Queries.ListEventTypes;
using Astrobank.Application.MasterData.EventTypes.Commands.UpdateEventType;
using Astrobank.Application.MasterData.EventTypes.Commands.CreateEventType;
using Astrobank.Application.MasterData.ChartPermissions.DTOs;
using Astrobank.Application.MasterData.ChartPermissions.Queries.ListChartPermissions;
using Astrobank.Application.MasterData.ChartPermissions.Commands.UpdateChartPermission;
using Astrobank.Application.MasterData.ChartPermissions.Commands.CreateChartPermission;
using Astrobank.Application.MasterData.ChartTypes.DTOs;
using Astrobank.Application.MasterData.ChartTypes.Queries.ListChartTypes;
using Astrobank.Application.MasterData.ChartTypes.Commands.UpdateChartType;
using Astrobank.Application.MasterData.ChartTypes.Commands.CreateChartType;
using Astrobank.Persistence.Repositories;
using Astrobank.Application.MasterData.Countries.DTOs;
using Astrobank.Application.MasterData.Countries.Queries.ListCountries;
using Astrobank.Application.MasterData.Countries.Commands.UpdateCountry;
using Astrobank.Application.MasterData.Countries.Commands.CreateCountry;
using Astrobank.Application.MasterData.TagCategorys.DTOs;
using Astrobank.Application.MasterData.TagCategorys.Queries.ListTagCategorys;
using Astrobank.Application.MasterData.TagCategorys.Commands.UpdateTagCategory;
using Astrobank.Application.MasterData.TagCategorys.Commands.CreateTagCategory;
using Astrobank.Application.MasterData.HelpCategorys.DTOs;
using Astrobank.Application.MasterData.HelpCategorys.Queries.ListHelpCategorys;
using Astrobank.Application.MasterData.HelpCategorys.Commands.UpdateHelpCategory;
using Astrobank.Application.MasterData.HelpCategorys.Commands.CreateHelpCategory;
using Astrobank.Application.MasterData.EventTypes.DTOs;
using Astrobank.Application.MasterData.EventTypes.Queries.ListEventTypes;
using Astrobank.Application.MasterData.EventTypes.Commands.UpdateEventType;
using Astrobank.Application.MasterData.EventTypes.Commands.CreateEventType;
using Astrobank.Application.MasterData.ChartPermissions.DTOs;
using Astrobank.Application.MasterData.ChartPermissions.Queries.ListChartPermissions;
using Astrobank.Application.MasterData.ChartPermissions.Commands.UpdateChartPermission;
using Astrobank.Application.MasterData.ChartPermissions.Commands.CreateChartPermission;
using Astrobank.Application.MasterData.ChartTypes.DTOs;
using Astrobank.Application.MasterData.ChartTypes.Queries.ListChartTypes;
using Astrobank.Application.MasterData.ChartTypes.Commands.UpdateChartType;
using Astrobank.Application.MasterData.ChartTypes.Commands.CreateChartType;
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

// Master Data Repositories
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IChartTypeRepository, ChartTypeRepository>();
builder.Services.AddScoped<IChartPermissionRepository, ChartPermissionRepository>();
builder.Services.AddScoped<IEventTypeRepository, EventTypeRepository>();
builder.Services.AddScoped<IHelpCategoryRepository, HelpCategoryRepository>();
builder.Services.AddScoped<ITagCategoryRepository, TagCategoryRepository>();

// Master Data CQRS
builder.Services.AddScoped<IQueryHandler<ListCountriesQuery, PaginatedList<CountryDto>>, ListCountriesQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateCountryCommand>, CreateCountryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCountryCommand>, UpdateCountryCommandHandler>();

builder.Services.AddScoped<IQueryHandler<ListChartTypesQuery, PaginatedList<ChartTypeDto>>, ListChartTypesQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateChartTypeCommand>, CreateChartTypeCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateChartTypeCommand>, UpdateChartTypeCommandHandler>();

builder.Services.AddScoped<IQueryHandler<ListChartPermissionsQuery, PaginatedList<ChartPermissionDto>>, ListChartPermissionsQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateChartPermissionCommand>, CreateChartPermissionCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateChartPermissionCommand>, UpdateChartPermissionCommandHandler>();

builder.Services.AddScoped<IQueryHandler<ListEventTypesQuery, PaginatedList<EventTypeDto>>, ListEventTypesQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateEventTypeCommand>, CreateEventTypeCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateEventTypeCommand>, UpdateEventTypeCommandHandler>();

builder.Services.AddScoped<IQueryHandler<ListHelpCategorysQuery, PaginatedList<HelpCategoryDto>>, ListHelpCategorysQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateHelpCategoryCommand>, CreateHelpCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateHelpCategoryCommand>, UpdateHelpCategoryCommandHandler>();

builder.Services.AddScoped<IQueryHandler<ListTagCategorysQuery, PaginatedList<TagCategoryDto>>, ListTagCategorysQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateTagCategoryCommand>, CreateTagCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateTagCategoryCommand>, UpdateTagCategoryCommandHandler>();

// Charts Module Repositories
builder.Services.AddScoped<IChartRepository, ChartRepository>();

// Charts Module CQRS
builder.Services.AddScoped<IQueryHandler<ListChartsQuery, PaginatedList<ChartDto>>, ListChartsQueryHandler>();
builder.Services.AddScoped<ICommandHandler<CreateChartCommand>, CreateChartCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateChartCommand>, UpdateChartCommandHandler>();
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
