using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;

public class Position : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? NameAR { get; set; }

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;
}