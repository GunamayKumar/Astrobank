namespace Astrobank.Domain.Common;

/// <summary>
/// Represents the base entity for all domain entities, providing common audit properties.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedOn { get; protected set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified, if applicable.
    /// </summary>
    public DateTime? ModifiedOn { get; protected set; }
}
