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
            Name = name
        };

        // Users are Pending by default per business rules. (This is already the default for the enum, but explicit is fine).
        // Assign the role cleanly using the new domain method.
        user.AssignRole(roleId);

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
