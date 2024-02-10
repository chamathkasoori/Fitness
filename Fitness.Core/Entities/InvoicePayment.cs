using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class InvoicePayment : BaseEntity
{
    public int InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public int VatPercentage { get; set; }
    public string OperationType { get; set; } = string.Empty; // Charge, Refund etc..
    public string PaymentMethod { get; set; } = string.Empty; // Credit Card, Debit Card, Cash
    public string Notes { get; set; } = string.Empty;
    public virtual Invoice Invoice { get; set; } = null!;
}
