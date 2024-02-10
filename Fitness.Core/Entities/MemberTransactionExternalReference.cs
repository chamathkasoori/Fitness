using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberTransactionExternalReference : BaseEntity
{
    public int MemberId { get; set; }
    public int? SubscriptionPlanId { get; set; }
    public Guid TransactionId { get; set; }
    public virtual Member Member { get; set; } = null!;
}
