using Fitness.Core.Enums;

public class SubscriptionPlanMobileDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public string PlanNameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public PlanType PlanType { get; set; }
    public int Duration { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public int FreezeDays { get; set; }
    public decimal PlanPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public decimal RegistrationFee { get; set; }
    public decimal? ExpiryDuration { get; set; }
}