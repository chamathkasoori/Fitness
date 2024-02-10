namespace Fitness.Dtos;
public class ProductPostDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string UOM { get; set; } = string.Empty;
    public decimal? GrossPrice { get; set; }
    public decimal? NetPrice { get; set; }
    public int VatId { get; set; }
    public string Type { get; set; } = string.Empty;
    public int? Threshold { get; set; }
    public string? BatchNo { get; set; }
    public List<ProductImagePostDto> ProductImages { get; set; } = new List<ProductImagePostDto>();
    public List<ProductAvailableClubPostDto> ProductAvailableClubs { get; set; } = new List<ProductAvailableClubPostDto>();
    public List<ProductAvailableApplicationPostDto> ProductAvailableApplications { get; set; } = new List<ProductAvailableApplicationPostDto>();
}
