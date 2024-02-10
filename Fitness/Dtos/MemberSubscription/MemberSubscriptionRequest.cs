namespace Fitness.Dtos;
public class MemberSubscriptionRequest
{
    public int MemberId { get; set; }
    public int SubscriptionPlanId { get; set; }
    public int SubscriptionPlanDiscountId { get; set; } // WE NEED TO REMOVE AFTER CLIENT IS FIXED
    public List<int> SubscriptionPlanDiscountIds { get; set; } = new List<int>();
    public string PaymentMethod { get; set; } = string.Empty; // CASH or CARD
}
