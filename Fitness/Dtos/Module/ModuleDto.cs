namespace Fitness.Dtos;
public class ModuleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Hierarchy { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int? ParentModuleId { get; set; }
}