namespace Fitness.Dtos;
public class AddressDto
{
    public string? FullAddress { get; set; }
    public int CountryId { get; set; }
    public int? CityId { get; set; }
    public int? RegionId { get; set; }
    public int? StateId { get; set; }
    public string? ZipCode { get; set; }
    public string? TimeZone { get; set; }
}
