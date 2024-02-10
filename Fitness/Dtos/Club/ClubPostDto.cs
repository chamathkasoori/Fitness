namespace Fitness.Dtos;
public class ClubPostDto
{
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public int ClubNumber { get; set; }
    public DateTime OpenDate { get; set; }
    public string? AccountingNumber { get; set; }
    public string? AccountingAccount { get; set; }  
    public string Gender { get; set; } = string.Empty;
    public bool Visible { get; set; }

    //Location Information
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public int? RegionId { get; set; }
    public int? StateId { get; set; }
    public string? TimeZone { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? AreaSalesManager { get; set; }

    //Contact Information
    public string? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
    public string? BuildingName { get; set; }
}