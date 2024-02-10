namespace Fitness.Dtos;
public class RegionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameAR { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; } = string.Empty;
}