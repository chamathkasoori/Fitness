using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Region : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? NameAR { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Address> Address { get; set; } = new List<Address>();
}