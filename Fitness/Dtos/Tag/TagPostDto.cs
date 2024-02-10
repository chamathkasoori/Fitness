using Fitness.Core.Enums;

namespace Fitness.Dtos;
public class TagPostDto
{
    public string Name { get; set; } = string.Empty;
    public TagType Type { get; set; }
}