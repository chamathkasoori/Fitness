namespace Fitness.Core.Common.Xero;

public class MakePaymentDto
{
    public PaymentInvoice Invoice { get; set; } = null!;
    public Account Account { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentInvoice
{
    public Guid InvoiceID { get; set; }
}

public class Account
{
    public string Code { get; set; } = string.Empty;
}
