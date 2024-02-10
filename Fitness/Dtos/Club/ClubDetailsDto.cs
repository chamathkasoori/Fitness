namespace Fitness.Dtos;
public class ClubDetailsDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ShortName { get; set; } = string.Empty;

    public DateTime OpeningDate { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public int? ClubNumber { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public int CountryId { get; set; }

    public int RegionId { get; set; }

    public int CityId { get; set; }

    public string ZipCode { get; set; } = string.Empty;

    public string FullAddress { get; set; } = string.Empty;

    public int? TimeZoneId { get; set; }

    public string Gender { get; set; } = string.Empty;

    public CountryDto Country { get; set; } = new CountryDto();

    public CityDto City { get; set; } = new CityDto();
}