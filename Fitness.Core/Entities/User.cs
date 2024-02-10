using Fitness.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Fitness.Core.Entities;
public class User : IdentityUser<int>
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public int? DeletedBy { get; set; }
    public bool IsDelete { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int? AddressId { get; set; }

    public string MobileNo { get; set; } = string.Empty;

    public string? MobileNo1 { get; set; }

    public DateTime? Dob { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string? LastContact { get; set; }

    public string? Status { get; set; }

    public bool? HasAcceptedTerms { get; set; }

    public UserType Type { get; set; }

    public virtual Address? Address { get; set; }
}