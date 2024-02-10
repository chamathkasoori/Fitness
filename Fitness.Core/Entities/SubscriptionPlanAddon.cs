using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanAddon : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int UsageLimitPerPlan { get; set; }
    public bool IsAvailableForAllPlans { get; set; }
    public bool AddToAllClubs { get; set; }
    public virtual ICollection<SubscriptionPlanAddonClub> SubscriptionPlanAddonClubs { get; set; } = new List<SubscriptionPlanAddonClub>();
    public virtual ICollection<SubscriptionPlanSubscriptionPlanAddon> SubscriptionPlanSubscriptionPlanAddons { get; set; } = new List<SubscriptionPlanSubscriptionPlanAddon>();
}
