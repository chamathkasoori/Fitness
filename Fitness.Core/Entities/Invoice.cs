using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Invoice : BaseEntity
{
    public string Type { get; set; } = string.Empty;
    public Guid InvoiceID { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public decimal AmountDue { get; set; }
    public decimal AmountPaid { get; set; }
    public bool SentToContact { get; set; }
    public decimal CurrencyRate { get; set; }
    public bool IsDiscounted { get; set; }
    public bool HasErrors { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public Guid BrandingThemeID { get; set; }
    public string Status { get; set; } = string.Empty;
    public string LineAmountTypes { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
    public decimal TotalTax { get; set; }
    public decimal Total { get; set; }
    public DateTime UpdatedDateUTC { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public bool InvoiceSent { get; set; }
    public int MemberSubscriptionId { get; set; }
    public MemberSubscription MemberSubscription { get; set; } = null!;
}