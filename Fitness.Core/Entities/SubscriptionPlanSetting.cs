using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class SubscriptionPlanSetting : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public bool IsValueVisible { get; set; }
    public bool IsDropdownVisible { get; set; }
    public string DropdownValues { get; set; } = string.Empty;
    public virtual ICollection<SubscriptionPlanSubscriptionPlanSetting> AssignedSubscriptionPlanSettings { get; set; } = new List<SubscriptionPlanSubscriptionPlanSetting>();
}