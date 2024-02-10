namespace Fitness.Dtos.Invoice;

public class InvoiceDto
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public bool InvoiceSent { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Club { get; set; } = string.Empty;
}