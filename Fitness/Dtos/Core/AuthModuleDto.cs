namespace Fitness.Dtos;
public class AuthModuleDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public List<AuthModuleDto> Operations { get; set; } = new List<AuthModuleDto>();
    public List<AuthModuleDto> Items { get; set; } = new List<AuthModuleDto>();
}
