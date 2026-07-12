namespace Astrobank.Application.Interfaces.Identity;

/// <summary>
/// Abstraction for identity and authentication operations.
/// </summary>
public interface IIdentityService
{
    Task<AuthenticationResult> RegisterUserAsync(string email, string username, string password, int roleId, string name, CancellationToken cancellationToken = default);
    Task<AuthenticationResult> LoginAsync(string username, string password, bool rememberMe, CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
}
