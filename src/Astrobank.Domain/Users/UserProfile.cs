using Astrobank.Domain.Common;

namespace Astrobank.Domain.Users;

/// <summary>
/// Represents the public profile information for a user.
/// </summary>
public class UserProfile : BaseEntity
{
    /// <summary>
    /// Gets or sets the primary key for the user profile. This should typically map to the UserID in a 1:1 relationship.
    /// </summary>
    public int UserProfileID { get; init; }

    /// <summary>
    /// Gets or sets the foreign key to the associated user.
    /// </summary>
    public int UserID { get; init; }

    /// <summary>
    /// Gets or sets the display name shown on the user's profile.
    /// </summary>
    public string? DisplayName { get; private set; }

    /// <summary>
    /// Gets or sets the user's biography.
    /// </summary>
    public string? Biography { get; private set; }

    /// <summary>
    /// Gets or sets the user's qualification or degree.
    /// </summary>
    public string? Qualification { get; private set; }

    /// <summary>
    /// Gets or sets the number of years of experience the user has.
    /// </summary>
    public int? ExperienceYears { get; private set; }

    /// <summary>
    /// Gets or sets the user's personal or professional website URL.
    /// </summary>
    public string? Website { get; private set; }

    /// <summary>
    /// Gets or sets the user's consultation charges.
    /// </summary>
    public decimal? ConsultationCharges { get; private set; }

    /// <summary>
    /// Gets or sets the URL or path to the user's profile photo.
    /// </summary>
    public string? ProfilePhoto { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user's profile is public.
    /// </summary>
    public bool? IsPublicProfile { get; private set; }

    /// <summary>
    /// Navigation property to the associated user.
    /// </summary>
    public User? User { get; private set; }
}
