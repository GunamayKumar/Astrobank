namespace Astrobank.Domain.Users.Enums;

/// <summary>
/// Represents the gender of a user.
/// </summary>
public enum Gender
{
    /// <summary>
    /// The gender is unknown or not specified.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Male gender.
    /// </summary>
    Male = 1,

    /// <summary>
    /// Female gender.
    /// </summary>
    Female = 2,

    /// <summary>
    /// Other gender.
    /// </summary>
    Other = 3,

    /// <summary>
    /// The user prefers not to say their gender.
    /// </summary>
    PreferNotToSay = 4
}
