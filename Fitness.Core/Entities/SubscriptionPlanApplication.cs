using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanApplication : BaseEntity
{
    public int ApplicationId { get; set; }
    public virtual Application Application { get; set; } = null!;

    public int SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}