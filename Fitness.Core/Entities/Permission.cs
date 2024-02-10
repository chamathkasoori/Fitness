namespace Fitness.Core.Entities;

public class Permission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public int? ParentPermissionId { get; set; }

    public virtual ICollection<Permission> InverseParentPermission { get; set; } = new List<Permission>();

    public virtual Permission? ParentPermission { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}