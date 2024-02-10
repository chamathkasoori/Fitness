namespace Fitness.Dtos;
public class RolePostDto
{
    public string Name { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionAr { get; set; } = string.Empty;
    public List<string> ModuleOperations { get; set; } = new List<string>();
}
