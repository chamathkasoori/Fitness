using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanSubscriptionPlanAddon : BaseEntity
{
    public int SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public int SubscriptionPlanAddonId { get; set; }
    public virtual SubscriptionPlanAddon SubscriptionPlanAddon { get; set; } = null!;
}
