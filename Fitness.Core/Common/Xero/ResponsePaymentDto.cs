using Fitness.Core.Common.Xero.Core;

namespace Fitness.Core.Common.Xero;
public class ResponsePaymentDto : XeroResponseDto
{
    public List<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
}

public class PaymentDto
{
    public Guid PaymentID { get; set; }
    public DateTime Date { get; set; }
    public decimal BankAmount { get; set; }
    public decimal Amount { get; set; }
    public decimal CurrencyRate { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime UpdatedDateUTC { get; set; }
    public bool HasAccount { get; set; }
    public bool IsReconciled { get; set; }
}
