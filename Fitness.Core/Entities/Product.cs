using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Product : BaseEntity
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
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public virtual ICollection<ProductAvailableClub> ProductAvailableClubs { get; set; } = new List<ProductAvailableClub>();
    public virtual ICollection<ProductAvailableApplication> ProductAvailableApplications { get; set; } = new List<ProductAvailableApplication>();
    public virtual ProductCategory Category { get; set; } = null!;
    public virtual Vat Vat { get; set; } = null!;
}
