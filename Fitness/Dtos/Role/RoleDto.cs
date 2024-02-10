namespace Fitness.Dtos;
public class RoleDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public string Company { get; set; } = string.Empty;
    public virtual ICollection<RoleModuleDetailsDto> RoleModules { get; set; } = new List<RoleModuleDetailsDto>();
    public virtual Dictionary<string, RoleModuleNodeDto> RoleModuleNodes { get; set; } = new();
}