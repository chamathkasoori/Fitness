using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberSubscriptionFreeze : BaseEntity
{
    public int MemberSubscriptionId { get; set; }
    public int MemberSubscriptionAddonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;

    public virtual MemberSubscription MemberSubscription { get; set; } = null!;
}
