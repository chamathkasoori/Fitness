namespace Fitness.Dtos;

public class ModuleOperationDto
{
    public int ModuleId { get; set; }

    public int OperationId { get; set; }
    
    public OperationDto Operation { get; set; } = null!;
}