namespace Fitness.Dtos;
public class AuthRoleDto
{
    public string Name { get; set; } = string.Empty;
    public List<RoleModuleDto> RoleModules { get; set; } = new List<RoleModuleDto>();
}