using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Club : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public int ClubNumber { get; set; }
    public int CompanyId { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public string? BuildingName { get; set; }
    public string? AreaSalesManager {  get; set; }
    public DateTime OpenDate { get; set; }
    public string? AccountingNumber { get; set; }
    public string? AccountingAccount { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string Gender { get; set; } = string.Empty;
    public bool Visible { get; set; }
    public int AddressId { get; set; }    
    public virtual Company Company { get; set; } = null!;
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<ClubOpeningHour> ClubOpeningHours { get; set; } = new List<ClubOpeningHour>();
    public virtual ICollection<EmployeeAssignedClub> EmployeeAssignedClubs { get; set; } = new List<EmployeeAssignedClub>();
    public virtual ICollection<EmployeeAvailableClub> EmployeeAvailableClubs { get; set; } = new List<EmployeeAvailableClub>();
    public virtual ICollection<SubscriptionPlanAssignedClub> SubscriptionPlanAssignedClubs { get; set; } = new List<SubscriptionPlanAssignedClub>();
    public virtual ICollection<SubscriptionPlanAvailableClub> SubscriptionPlanAvailableClubs { get; set; } = new List<SubscriptionPlanAvailableClub>();
}