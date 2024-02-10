namespace Fitness.Dtos;
public class SubscriptionPlanDiscountPostDto
{
    public bool IsActive { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool MembershipFeeDiscountApplied { get; set; }
    public string? MembershipFeeDiscountType { get; set; }
    public decimal? MembershipFeeDiscountValue { get; set; }

    public bool ApplyToAllSubscriptionPlans { get; set; }
    public bool ApplyToAllClubs { get; set; }
    public bool OnlyForChoosenRoles { get; set; }
    public bool OnlyForChoosenApplications { get; set; }
    public bool AddAutomaticallyToAutomaticallyRenewedPlans { get; set; }
    public bool AvailableOnlyWithPromoCode { get; set; }
    public string PromoCode { get; set; } = string.Empty;

    public List<int> SubscriptionPlanIds { get; set; } = new List<int>();
    public List<int> ClubIds { get; set; } = new List<int>();
    public List<int> RoleIds { get; set; } = new List<int>();
    public List<int> ApplicationIds { get; set; } = new List<int>();
    public List<int> CombinedDiscountIds { get; set; } = new List<int>();
}
