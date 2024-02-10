namespace Fitness.Dtos;
public class MemberPutDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public DateTime Dob { get; set; }

    public int CountryId { get; set; }

    public int? CityId { get; set; }

    public int? RegionId { get; set; }

    public string? ZipCode { get; set; }

    public string? FullAddress { get; set; }
}