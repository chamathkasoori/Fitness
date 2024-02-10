using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Company : BaseEntity
{
    public string Name { get; set; } = null!;

    public int? AddressId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? AccountNumber { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Icon> Icons { get; set; } = new List<Icon>();
}