using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class ProductCategory : BaseEntity
{
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public int IconId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }    
    public bool? PersonalTrainingCategory { get; set; }
    public virtual Company Company { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
    public virtual Icon Icon { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}