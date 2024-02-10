namespace Fitness.Dtos;
public class TreeNodeDto
{
    public string Key { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public List<TreeNodeDto> Children { get; set; } = new List<TreeNodeDto>();
}