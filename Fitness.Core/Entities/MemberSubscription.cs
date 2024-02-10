using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class MemberSubscription : BaseEntity
{
    public int MemberId { get; set; }
    public int SubscriptionPlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ApplicationId { get; set; }
    public decimal SubscriptionPlanAmount { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public virtual Member Member { get; set; } = null!;
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;
    public virtual ICollection<MemberSubscriptionFreeze> MemberSubscriptionFreezes { get; set; } = new List<MemberSubscriptionFreeze>();
    public virtual ICollection<MemberSubscriptionTransaction> MemberSubscriptionTransactions { get; set; } = new List<MemberSubscriptionTransaction>();
    public virtual ICollection<MemberSubscriptionDiscount> MemberSubscriptionDiscounts { get; set; } = new List<MemberSubscriptionDiscount>();
    public virtual ICollection<MemberSubscriptionAddon> MemberSubscriptionAddons { get; set; } = new List<MemberSubscriptionAddon>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}