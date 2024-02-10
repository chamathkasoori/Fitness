using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanDiscountCombination : BaseEntity
{
    public int SubscriptionPlanDiscountId { get; set; }
    public virtual SubscriptionPlanDiscount SubscriptionPlanDiscount { get; set; } = null!;

    public int CombinedSubscriptionPlanDiscountId { get; set; }
    public virtual SubscriptionPlanDiscount CombinedSubscriptionPlanDiscount { get; set; } = null!;
}
