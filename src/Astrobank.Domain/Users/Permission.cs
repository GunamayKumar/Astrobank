using Astrobank.Domain.Common;

namespace Astrobank.Domain.Users;

/// <summary>
/// Represents a system permission.
/// </summary>
public class Permission : BaseEntity
{
    /// <summary>
    /// Gets or sets the primary key for the permission.
    /// </summary>
    public int PermissionID { get; init; }

    /// <summary>
    /// Gets or sets the unique name of the permission.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets a description of what the permission allows.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets or sets the category this permission belongs to (e.g., "User Management", "Chart Management").
    /// </summary>
    public required string Category { get; init; }

    /// <summary>
    /// Navigation property for the roles that have this permission.
    /// </summary>
    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
}
