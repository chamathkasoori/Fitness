namespace Fitness.Core.Common;
public class StockDetail
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int? Threshold { get; set; }
    public string UOM { get; set; } = string.Empty;
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public int WarehouseId { get; set; }
    public int? CategoryId { get; set; }
}
