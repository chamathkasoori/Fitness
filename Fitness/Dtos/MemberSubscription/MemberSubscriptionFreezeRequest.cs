namespace Fitness.Dtos;
public class MemberSubscriptionFreezeRequest
{
    public int MemberSubscriptionId { get; set; }
    public int MemberSubscriptionAddonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}
