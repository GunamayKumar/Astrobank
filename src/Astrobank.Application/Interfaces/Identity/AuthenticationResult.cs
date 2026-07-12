namespace Astrobank.Application.Interfaces.Identity;

/// <summary>
/// A persistence-ignorant result object for authentication operations.
/// </summary>
public class AuthenticationResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();

    public static AuthenticationResult Success() => new AuthenticationResult { Succeeded = true };
    public static AuthenticationResult Failure(IEnumerable<string> errors) => new AuthenticationResult { Succeeded = false, Errors = errors };
}
