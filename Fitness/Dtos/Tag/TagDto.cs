using Fitness.Core.Enums;

namespace Fitness.Dtos;
public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TagType Type { get; set; }
}
