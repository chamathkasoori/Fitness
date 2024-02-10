using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Employee : BaseEntity
{
    public int UserId { get; set; }
    public int DepartmentId { get; set; }
    public int PositionId { get; set; }
    public int RoleId { get; set; }
    public DateTime? EmploymentDate { get; set; }
    public string? Citizenship { get; set; }
    public string? Picture { get; set; }
    public string PersonalIdentificationNumber { get; set; } = string.Empty;
    public bool? OnShift { get; set; }
    public bool? NoLogin { get; set; }
    public bool? ChangePassword { get; set; }
    public bool AddToAllClubs { get; set; }
    public bool AssignToNewClubs { get; set; }
    public bool AvailableToNewClubs { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
    public virtual Position Position { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<EmployeeAssignedClub> EmployeeAssignedClubs { get; set; } = new List<EmployeeAssignedClub>();
    public virtual ICollection<EmployeeAvailableClub> EmployeeAvailableClubs { get; set; } = new List<EmployeeAvailableClub>();
}