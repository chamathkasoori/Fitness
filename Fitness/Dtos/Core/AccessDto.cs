namespace Fitness.Dtos;
public class AccessDto
{
    public int UserId { get; set; }
    public int ApplicationId { get; set; }
    public List<int> ClubIdList { get; set; } = new List<int>();
    public int EmployeeId { get; set; } // Web Only
    public int MemberId { get; set; } // Mobile Only
    public int RoleId { get; set; } // Web Only
    public int CompanyId { get; set; } // Web Only
    public int DepartmentId { get; set; } // Web Only
}
