namespace Fitness.Dtos;
public class SubscriptionPlanSettingDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsValueVisible { get; set; }
    public bool IsDropdownVisible { get; set; }
    public List<string> DropdownItems { get; set; } = new List<string>();
}
