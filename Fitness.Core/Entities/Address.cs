using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Address : BaseEntity
{
    public string? FullAddress { get; set; }
    public int CountryId { get; set; }
    public int? CityId { get; set; }
    public int? RegionId { get; set; }
    public int? StateId { get; set; }
    public string? ZipCode { get; set; }
    public string? TimeZone { get; set; }
    public string? Street { get; set; }
    public virtual Country Country { get; set; } = null!;
    public virtual City City { get; set; } = null!;
    public virtual Region Region { get; set; } = null!;
}