namespace Fitness.Dtos;
public class MemberPostDto
{
    public int ClubId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public DateTime Dob { get; set; }

    public int CountryId { get; set; }

    public int? CityId { get; set; }

    public int? StateId { get; set; }

    public string? ZipCode { get; set; }

    public string? FullAddress { get; set; }

    public string? PreferredLanguage { get; set; }

    public string PersonalIdentificationNumber { get; set; } = string.Empty;

    public List<int> TagIds { get; set; } = new();

    public bool IsGuest { get; set; }

    public int SubscriptionPlanId { get; set; }
    public int SubscriptionPlanDiscountId { get; set; } // WE NEED TO REMOVE AFTER CLIENT IS FIXED
    public List<int> SubscriptionPlanDiscountIds { get; set; } = new List<int>();
    public string PaymentMethod { get; set; } = string.Empty; // CASH or CARD
}