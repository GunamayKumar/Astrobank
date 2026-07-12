namespace Astrobank.Domain.Users.Enums;

/// <summary>
/// Represents the current status of a user account.
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// The account is pending activation or approval.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The account is active and in good standing.
    /// </summary>
    Active = 1,

    /// <summary>
    /// The account is inactive (e.g., has not been used recently).
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// The account has been suspended for violations or other issues.
    /// </summary>
    Suspended = 3,

    /// <summary>
    /// The account is locked (e.g., due to too many failed login attempts).
    /// </summary>
    Locked = 4,

    /// <summary>
    /// The account has been deleted.
    /// </summary>
    Deleted = 5
}
