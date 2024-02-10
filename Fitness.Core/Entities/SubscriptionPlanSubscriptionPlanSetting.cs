using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public  class SubscriptionPlanSubscriptionPlanSetting : BaseEntity
{
    public int SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    public int SubscriptionPlanSettingId { get; set; }
    public virtual SubscriptionPlanSetting SubscriptionPlanSetting { get; set; } = null!;

    public bool IsChecked { get; set; }

    public string Value { get; set; } = string.Empty;

    public string DropdownValue { get; set; } = string.Empty;
}
