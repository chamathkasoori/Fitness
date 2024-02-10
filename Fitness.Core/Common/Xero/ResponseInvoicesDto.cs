using Fitness.Core.Common.Xero.Core;

namespace Fitness.Core.Common.Xero;

public class ResponseInvoicesDto : XeroResponseDto
{
    public List<InvoiceResponseDto> Invoices { get; set; } = new List<InvoiceResponseDto>();
}

public class InvoiceResponseDto
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
}
