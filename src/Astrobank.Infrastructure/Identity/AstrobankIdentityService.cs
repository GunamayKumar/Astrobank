using Astrobank.Application.Interfaces.Identity;
using Astrobank.Domain.Users;
using Astrobank.Domain.Users.Enums;
using Microsoft.AspNetCore.Identity;

namespace Astrobank.Infrastructure.Identity;

public class AstrobankIdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AstrobankIdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthenticationResult> RegisterUserAsync(string email, string username, string password, int roleId, string name, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = email,
            Username = username,
            Name = name,
            PasswordHash = "" // Will be set by UserManager via PasswordHasher
        };

        // Reflection used for properties with private setters (following strict DDD rules in this repository)
        typeof(User).GetProperty(nameof(User.Status))!.SetValue(user, UserStatus.Active);
        typeof(User).GetProperty(nameof(User.CreatedOn))!.SetValue(user, DateTime.UtcNow);

        // Reflection to set RoleID as it's private set (or handled via a domain method in future)
        typeof(User).GetProperty(nameof(User.RoleID))!.SetValue(user, roleId);

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return AuthenticationResult.Success();
        }

        return AuthenticationResult.Failure(result.Errors.Select(e => e.Description));
    }

    public async Task<AuthenticationResult> LoginAsync(string username, string password, bool rememberMe, CancellationToken cancellationToken = default)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return AuthenticationResult.Success();
        }

        return AuthenticationResult.Failure(new[] { "Invalid login attempt." });
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        await _signInManager.SignOutAsync();
    }
}
