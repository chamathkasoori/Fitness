using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanDiscount : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool MembershipFeeDiscountApplied { get; set; }
    // FMA - Free month after
    // LBA - Lower by amount
    // LBP - Lower by percent
    public string? MembershipFeeDiscountType { get; set; }
    public decimal? MembershipFeeDiscountValue { get; set; }

    public bool ApplyToAllSubscriptionPlans { get; set; }
    public bool ApplyToAllClubs { get; set; }
    public bool OnlyForChoosenRoles { get; set; }
    public bool OnlyForChoosenApplications { get; set; }
    public bool AddAutomaticallyToAutomaticallyRenewedPlans { get; set; }
    public bool AvailableOnlyWithPromoCode { get; set; }
    public string PromoCode { get; set; } = string.Empty;

    public virtual ICollection<SubscriptionPlanDiscountSubscriptionPlan> SubscriptionPlanDiscountSubscriptionPlans { get; set; } = new List<SubscriptionPlanDiscountSubscriptionPlan>();
    public virtual ICollection<SubscriptionPlanDiscountClub> SubscriptionPlanDiscountClubs { get; set; } = new List<SubscriptionPlanDiscountClub>();
    public virtual ICollection<SubscriptionPlanDiscountRole> SubscriptionPlanDiscountRoles { get; set; } = new List<SubscriptionPlanDiscountRole>();
    public virtual ICollection<SubscriptionPlanDiscountApplication> SubscriptionPlanDiscountApplications { get; set; } = new List<SubscriptionPlanDiscountApplication>();
}
