namespace Fitness.Dtos;
public class TreeModuleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TreeOperationDto> Operations { get; set; } = new List<TreeOperationDto>();
    public int? ParentModuleId { get; set; }
}
