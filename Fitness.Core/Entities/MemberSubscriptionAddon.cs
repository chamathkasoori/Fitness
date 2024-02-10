using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberSubscriptionAddon : BaseEntity
{
    public int MemberSubscriptionId { get; set; }
    public virtual MemberSubscription MemberSubscription { get; set; } = null!;

    public int SubscriptionPlanAddonId { get; set; }
    public virtual SubscriptionPlanAddon SubscriptionPlanAddon { get; set; } = null!;
}
