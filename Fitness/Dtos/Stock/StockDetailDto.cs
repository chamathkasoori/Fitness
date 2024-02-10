namespace Fitness.Dtos;
public class StockDetailDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int? WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int? Threshold { get; set; }
    public string UOM { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; } = 0;
    public decimal Amount { get; set; } = 0;
}
