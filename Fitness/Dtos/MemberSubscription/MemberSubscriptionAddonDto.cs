namespace Fitness.Dtos;
public class MemberSubscriptionAddonDto
{
    public int MemberSubscriptionAddonId { get; set; }
    public int SubscriptionPlanAddonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int UsageLimitPerPlan { get; set; }
}
