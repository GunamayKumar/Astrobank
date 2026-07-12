namespace Astrobank.Domain.Common;

/// <summary>
/// Defines an entity that supports soft deletion.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity has been logically deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}
