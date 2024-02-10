using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberSubscriptionTransfer : BaseEntity
{
    public int OldMemberSubscriptionId { get; set; }
    public virtual MemberSubscription OldMemberSubscription { get; set; } = null!;

    public int NewMemberSubscriptionId { get; set; }
    public virtual MemberSubscription NewMemberSubscription { get; set; } = null!;

    public int NoOfDays { get; set; }
}
