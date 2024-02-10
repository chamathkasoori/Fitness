namespace Fitness.Dtos;
public class MemberDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ClubId { get; set; }
    public string ClubName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public string? PreferredLanguage { get; set; }
    public string PersonalIdentificationNumber { get; set; } = string.Empty;
    public bool IsGuest { get; set; }
    public int CountryId { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public int? RegionId { get; set; }
    public string? RegionName { get; set; }
    public int? StateId { get; set; }
    public string? ZipCode { get; set; }
    public string? FullAddress { get; set; }
    public string MemberNo { get; set; } = string.Empty;
}
