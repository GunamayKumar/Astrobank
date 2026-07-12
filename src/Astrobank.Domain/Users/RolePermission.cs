using Astrobank.Domain.Common;

namespace Astrobank.Domain.Users;

/// <summary>
/// Represents a join entity granting a specific permission to a specific role.
/// </summary>
public class RolePermission : BaseEntity
{
    /// <summary>
    /// Gets or sets the primary key for the role permission mapping.
    /// </summary>
    public int RolePermissionID { get; init; }

    /// <summary>
    /// Gets or sets the foreign key to the associated role.
    /// </summary>
    public int RoleID { get; init; }

    /// <summary>
    /// Gets or sets the foreign key to the associated permission.
    /// </summary>
    public int PermissionID { get; init; }

    /// <summary>
    /// Navigation property to the associated role.
    /// </summary>
    public Role? Role { get; private set; }

    /// <summary>
    /// Navigation property to the associated permission.
    /// </summary>
    public Permission? Permission { get; private set; }
}
