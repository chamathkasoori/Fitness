using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class Member : BaseEntity
{
    public int UserId { get; set; }

    public int ClubId { get; set; }

    public int? ApplicationId { get; set; }

    public string? PreferredLanguage { get; set; }

    public string? Campaign { get; set; }

    public string? MethodOfContact { get; set; }

    public string? Rating { get; set; }

    public string? LeadStatus { get; set; }

    public string? UserPayPreference { get; set; }

    public string? MemberGoals { get; set; }

    public decimal? HeightInCm { get; set; }

    public decimal? WeightInKg { get; set; }

    public string? PaymentSources { get; set; }

    public string? FitnessGoals { get; set; }

    public string? MedicalHistory { get; set; }

    public string? PersonalIdentificationNumber { get; set; }

    public bool? HasAcceptedTerms { get; set; }

    public string? MemberNo { get; set; }

    public bool IsGuest { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Club Club { get; set; } = null!;

    public virtual Application Application { get; set; } = null!;

    public virtual MemberOtp MemberOtp { get; set; } = null!;

    public ICollection<MemberTag> MemberTags { get; set; } = new List<MemberTag>();
}