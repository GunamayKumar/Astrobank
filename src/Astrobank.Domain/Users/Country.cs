using Astrobank.Domain.Common;

namespace Astrobank.Domain.Users;

/// <summary>
/// Represents a lookup entity for countries.
/// </summary>
public class Country : BaseEntity
{
    /// <summary>
    /// Gets or sets the primary key for the country.
    /// </summary>
    public int CountryID { get; init; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public required string CountryName { get; init; }

    /// <summary>
    /// Gets or sets the 2-letter ISO code for the country.
    /// </summary>
    public required string ISOCode2 { get; init; }

    /// <summary>
    /// Gets or sets the 3-letter ISO code for the country.
    /// </summary>
    public required string ISOCode3 { get; init; }

    /// <summary>
    /// Gets or sets the phone dial code for the country, if applicable.
    /// </summary>
    public string? PhoneCode { get; init; }

    /// <summary>
    /// Gets or sets the order in which the country should be displayed in lists.
    /// </summary>
    public int DisplayOrder { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the country is active and selectable.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Navigation property for the users associated with this country.
    /// </summary>
    public ICollection<User> Users { get; private set; } = new List<User>();
}
