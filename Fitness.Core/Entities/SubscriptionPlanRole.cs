using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanRole : BaseEntity
{
    public int SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public int RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
}