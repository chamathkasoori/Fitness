using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Department : BaseEntity
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;
}