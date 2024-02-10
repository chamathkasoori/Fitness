using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanDiscountSubscriptionPlan : BaseEntity
{
    public int SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public int SubscriptionPlanDiscountId { get; set; }
    public virtual SubscriptionPlanDiscount SubscriptionPlanDiscount { get; set; } = null!;
}
