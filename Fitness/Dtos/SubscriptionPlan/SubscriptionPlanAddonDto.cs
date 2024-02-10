namespace Fitness.Dtos;
public class SubscriptionPlanAddonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int UsageLimitPerPlan { get; set; }
    public bool IsAvailableForAllPlans { get; set; }
    public bool AddToAllClubs { get; set; }
    public List<int> ClubIds { get; set; } = new List<int>();
}
