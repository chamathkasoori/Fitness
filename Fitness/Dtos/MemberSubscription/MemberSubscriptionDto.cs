namespace Fitness.Dtos;
public class MemberSubscriptionDto
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public int MemberId { get; set; }
    public int SubscriptionPlanId { get; set; }
    public string SubscriptionPlanName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AccessRuleName { get; set; } = string.Empty;
    public string PaymentIntervalName { get; set; } = string.Empty;
    public string CommitmentPeriodName { get; set; } = string.Empty;
    public DateTime CommitmentEndDate { get; set; }
    public int PaymentInteralMonths { get; set; }
    public decimal SubscriptionPlanAmount { get; set; }
    public decimal Amount { get; set; }
    public int FreezeDays { get; set; }
    public int AvailableFreezeDays { get; set; }
    public int? MemberSubscriptionFreezeId { get; set; }
    public DateTime? FreezeFrom { get; set; }
    public DateTime? FreezeTo { get; set; }
    public List<MemberSubscriptionAddonDto> MemberSubscriptionAddonList { get; set; } = new List<MemberSubscriptionAddonDto>();
    public virtual SubscriptionPlanDto SubscriptionPlan { get; set; } = null!;
}
