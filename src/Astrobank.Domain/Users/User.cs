using Astrobank.Domain.Common;
using Astrobank.Domain.Users.Enums;

namespace Astrobank.Domain.Users;

/// <summary>
/// Represents a user within the Astrobank system.
/// </summary>
public class User : BaseEntity, ISoftDelete
{
    /// <summary>
    /// Gets or sets the primary key for the user.
    /// </summary>
    public int UserID { get; init; }

    /// <summary>
    /// Gets or sets the foreign key to the user's role.
    /// </summary>
    public int RoleID { get; private set; }

    /// <summary>
    /// Gets or sets the foreign key to the user's country, if provided.
    /// </summary>
    public int? CountryID { get; private set; }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the unique email address of the user.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Gets or sets the unique username of the user.
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Gets or sets the securely hashed password for the user.
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional phone number of the user.
    /// </summary>
    public string? PhoneNo { get; private set; }

    /// <summary>
    /// Gets or sets the gender of the user.
    /// </summary>
    public Gender Gender { get; private set; }

    /// <summary>
    /// Gets or sets an optional referral code used during registration.
    /// </summary>
    public string? ReferralCode { get; init; }

    /// <summary>
    /// Gets or sets the current status of the user account.
    /// </summary>
    public UserStatus Status { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is a verified astrologer.
    /// </summary>
    public bool IsVerifiedAstrologer { get; private set; }

    /// <summary>
    /// Gets or sets the IP address recorded for the user, if available.
    /// </summary>
    public string? IPAddress { get; private set; }

    /// <summary>
    /// Sets the password hash for the user.
    /// </summary>
    public void SetPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    /// <summary>
    /// Activates the user account.
    /// </summary>
    public void Activate()
    {
        Status = UserStatus.Active;
    }

    /// <summary>
    /// Marks the user as a verified astrologer.
    /// </summary>
    public void VerifyAstrologer()
    {
        IsVerifiedAstrologer = true;
    }

    /// <summary>
    /// Sets the role ID for the user.
    /// </summary>
    public void AssignRole(int roleId)
    {
        RoleID = roleId;
    }

    /// <summary>
    /// Sets the registration details (Phone, Gender, Country).
    /// </summary>
    public void SetRegistrationDetails(string? phoneNo, Gender gender, int countryId)
    {
        PhoneNo = phoneNo;
        Gender = gender;
        CountryID = countryId;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user account has been logically deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Navigation property to the user's role.
    /// </summary>
    public Role? Role { get; private set; }

    /// <summary>
    /// Navigation property to the user's country.
    /// </summary>
    public Country? Country { get; private set; }

    /// <summary>
    /// Navigation property to the user's public profile.
    /// </summary>
    public UserProfile? UserProfile { get; private set; }
}
