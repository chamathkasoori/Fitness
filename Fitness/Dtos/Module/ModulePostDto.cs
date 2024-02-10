namespace Fitness.Dtos;
public class ModulePostDto
{
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int? ParentModuleId { get; set; }
    public List<ModuleOperationPost> ModuleOperations { get; set; } = new List<ModuleOperationPost>();
}