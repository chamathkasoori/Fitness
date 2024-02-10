namespace Fitness.Dtos;
public class ClubDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public int ClubNumber { get; set; }
    public int CompanyId { get; set; }
    public DateTime OpenDate { get; set; }
    public string? AccountingNumber { get; set; }
    public string? AccountingAccount { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string Gender { get; set; } = string.Empty;
    public bool Visible { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string CityName { get; set; } = string.Empty;
    public int CountryId { get; set; }

    public int CityId { get; set; }
    public int? StateId { get; set; }
    public int? RegionId { get; set; }
    public string? TimeZone { get; set; }
    public string? AreaSalesManager { get; set; }
    public string? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? Email { get; set; }
    public string? BuildingName { get; set; }
}