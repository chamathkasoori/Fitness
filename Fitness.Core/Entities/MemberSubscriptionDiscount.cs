using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberSubscriptionDiscount : BaseEntity
{
    public int MemberSubscriptionId { get; set; }
    public int SubscriptionPlanDiscountId { get; set; }
    public virtual MemberSubscription MemberSubscription { get; set; } = null!;
    public virtual SubscriptionPlanDiscount SubscriptionPlanDiscount { get; set; } = null!;
}
