using Microsoft.AspNetCore.Identity;

namespace Fitness.Core.Entities;
public class Role : IdentityRole<int>
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public int? DeletedBy { get; set; }
    public bool IsDelete { get; set; }
    public string? NameAR { get; set; }

    public string? Description { get; set; }

    public string? DescriptionAR { get; set; }

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<RoleModule> RoleModules { get; set; } = new List<RoleModule>();
    
    public virtual ICollection<SubscriptionPlanRole> SubscriptionPlanRoles { get; set; } = new List<SubscriptionPlanRole>();
}