using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Vat : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Percentage { get; set; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
