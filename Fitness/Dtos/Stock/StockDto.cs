namespace Fitness.Dtos;
public class StockDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime ModifiedOn { get; set; }
}
