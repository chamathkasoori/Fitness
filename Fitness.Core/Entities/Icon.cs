using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Icon : BaseEntity
{
    public int CompanyId { get; set; }
    
    public string? Name { get; set; }

    public string? IconValue { get; set; }

    public virtual Company Company { get; set; } = null!;
}