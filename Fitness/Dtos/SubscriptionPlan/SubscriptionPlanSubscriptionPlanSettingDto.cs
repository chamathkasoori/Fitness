namespace Fitness.Dtos;
public class SubscriptionPlanSubscriptionPlanSettingDto
{
    public int SubscriptionPlanSettingId { get; set; }

    public bool IsChecked { get; set; }

    public string Value { get; set; } = string.Empty;

    public string DropdownValue { get; set; } = string.Empty;
}
