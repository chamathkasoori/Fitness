using Fitness.Core.Entities.Base;
using Fitness.Core.Enums;

namespace Fitness.Core.Entities;
public class MemberSubscriptionTransaction : BaseEntity
{
    public int MemberSubscriptionId { get; set; }
    public OperationType OperationType { get; set; }
    public int TransactionType { get; set; }
    public int TransactionCategory { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public int VatPercentage { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public virtual MemberSubscription MemberSubscription { get; set; } = null!;
}
