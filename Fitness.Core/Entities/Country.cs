using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string NameAR {  get; set; } = null!;
    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
    public virtual ICollection<Address> Address { get; set; } = new List<Address>();
}