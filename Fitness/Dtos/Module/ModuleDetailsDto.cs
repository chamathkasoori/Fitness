namespace Fitness.Dtos;
public class ModuleDetailsDto: ModuleDto
{
    public ModuleDetailsDto? ParentModule { get; set; }
    public List<ModuleOperationDto>? ModuleOperations { get; set; }
    public List<OperationDto> Operations { get; set; } = new List<OperationDto>();
}