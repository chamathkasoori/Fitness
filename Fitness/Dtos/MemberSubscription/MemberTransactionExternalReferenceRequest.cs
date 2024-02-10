namespace Fitness.Dtos;
public class MemberTransactionExternalReferenceRequest
{
    public int MemberId { get; set; }
    public int? SubscriptionPlanId { get; set; }
}