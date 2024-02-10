namespace Fitness.Dtos;
public class RoleModuleDetailsDto
{
    public int Id { get; set; }
    public int ModuleId { get; set; }
    public ModuleDetailsDto Module { get; set; } = null!;
    public ICollection<RoleModuleOperationDto> RoleModuleOperations { get; set; } = new List<RoleModuleOperationDto>();
}