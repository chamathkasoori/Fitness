namespace Fitness.Dtos;
public class MemberSubscriptionTransferRequest
{
    public int MemberSubscriptionId { get; set; }
    public int TransferMemberId { get; set; }
    public DateTime TransferDate { get; set; }
}
