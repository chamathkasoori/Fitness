namespace Fitness.Core.Common.Xero;

public class CreateInvoicesDto
{
    public List<CreateInvoice> Invoices { get; set; } = new List<CreateInvoice>();
}

public class InvoiceContact
{
    public Guid ContactID { get; set; }
}

public class CreateInvoice
{
    public string Type { get; set; } = string.Empty;
    public InvoiceContact Contact { get; set; } = null!;
    public List<LineItem> LineItems { get; set; } = new List<LineItem>();
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class LineItem
{
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitAmount { get; set; }
    public string AccountCode { get; set; } = string.Empty;
    public string TaxType { get; set; } = string.Empty;

    public List<PostTracking> Tracking { get; set; } = new List<PostTracking> { new PostTracking() };
}

public class PostTracking
{
    public string Name { get; set; } = string.Empty;
    public string Option { get; set; } = string.Empty;
}
