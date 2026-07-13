namespace Astrobank.Domain.Common;

/// <summary>
/// Represents the base entity for all domain entities, providing common audit properties.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified, if applicable.
    /// </summary>
    public DateTime? ModifiedOn { get; protected set; }

    /// <summary>
    /// Updates the ModifiedOn timestamp.
    /// </summary>
    public void MarkModified()
    {
        ModifiedOn = DateTime.UtcNow;
    }

    /// <summary>
    /// Sets the creation timestamp directly (e.g. during seeding).
    /// </summary>
    public void SetCreatedOn(DateTime createdOn)
    {
        CreatedOn = createdOn;
    }
}
