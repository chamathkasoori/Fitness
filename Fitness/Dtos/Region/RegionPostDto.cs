namespace Fitness.Dtos;
public class RegionPostDto
{
    public string Name { get; set; } = null!;

    public string? NameAR { get; set; }

    public bool IsActive { get; set; }

    public int CountryId { get; set; }
}