using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class City : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? NameAR { get; set; }
    public int CountryId { get; set; }
    public virtual Country Country { get; set; } = null!;
}