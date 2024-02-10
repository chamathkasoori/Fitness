namespace Fitness.Dtos;
public class MemberSubscriptionTransactionDto
{
    public int Id { get; set; }
    public int MemberSubscriptionId { get; set; }
    public int OperationType { get; set; }
    public int TransactionType { get; set; }
    public int TransactionCategory { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public int VatPercentage { get; set; }
    public string Description { get; set; } = string.Empty;
}
