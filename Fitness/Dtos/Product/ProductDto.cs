namespace Fitness.Dtos;
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string UOM { get; set; } = string.Empty;
    public decimal? GrossPrice { get; set; }
    public decimal? NetPrice { get; set; }
    public int VatId { get; set; }
    public string VatName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int? Threshold { get; set; }
    public string? BatchNo { get; set; }
}
