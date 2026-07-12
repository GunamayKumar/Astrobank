using Astrobank.Domain.Common;

namespace Astrobank.Domain.Users;

/// <summary>
/// Represents a role that can be assigned to a user, granting specific permissions.
/// </summary>
public class Role : BaseEntity, ISoftDelete
{
    /// <summary>
    /// Gets or sets the primary key for the role.
    /// </summary>
    public int RoleID { get; init; }

    /// <summary>
    /// Gets or sets the unique name of the role.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets an optional description of the role.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a built-in system role that cannot be deleted.
    /// </summary>
    public bool IsSystemRole { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the role has been logically deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Navigation property for the users assigned to this role.
    /// </summary>
    public ICollection<User> Users { get; private set; } = new List<User>();

    /// <summary>
    /// Navigation property for the permissions granted to this role.
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
}
