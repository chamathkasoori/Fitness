namespace Fitness.Dtos;
public class MemberSubscriptionFreezeDto
{
    public int Id { get; set; }
    public int MemberSubscriptionId { get; set; }
    public int MemberSubscriptionAddonId { get; set; }
    public string SubscriptionPlanName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
