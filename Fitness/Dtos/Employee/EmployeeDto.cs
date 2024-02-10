namespace Fitness.Dtos;
public class EmployeeDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string PersonalIdentificationNumber { get; set; } = string.Empty;
    public DateTime? Dob { get; set; }
    public int UserId { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int PositionId { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public DateTime? EmploymentDate { get; set; }
    public string? Citizenship { get; set; }
    public string? Picture { get; set; }
    public bool OnShift { get; set; }
    public bool NoLogin { get; set; }
    public bool ChangePassword { get; set; }
    public int CountryId { get; set; }
    public int RegionId { get; set; }
    public int CityId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string PostCode { get; set; } = string.Empty;
    public string HomePhone { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool AddToAllClubs { get; set; }
    public bool AssignToNewClubs { get; set; }
    public bool AvailableToNewClubs { get; set; }
    public List<int> AssignedClubIds { get; set; } = new List<int>();
    public List<int> AvailableClubIds { get; set; } = new List<int>();
}
