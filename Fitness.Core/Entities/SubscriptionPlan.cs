using System.ComponentModel.DataAnnotations;
using Fitness.Core.Entities.Base;
using Fitness.Core.Enums;

namespace Fitness.Core.Entities;
public class SubscriptionPlan : BaseEntity
{
    #region General

    public string Name { get; set; } = string.Empty;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? MinimumAge { get; set; }

    public int? MaximumAge { get; set; }

    public string? FiscalName { get; set; }

    public PlanType PlanType { get; set; }

    public bool AdditionalPaymentPlan { get; set; }

    public bool AddToNewClubs { get; set; }

    public int? AccessRuleId { get; set; }
    public virtual AccessRule AccessRule { get; set; } = null!;

    #endregion

    #region Payment Plan

    public FeeTypes FeeType { get; set; }

    public decimal? MembershipFee { get; set; }

    public int? MembershipFeeVatId { get; set; }
    public virtual Vat MembershipFeeVat { get; set; } = null!;

    public decimal? AdministrationFee { get; set; }

    public int? AdministrationFeeVatId { get; set; }
    public virtual Vat AdministrationFeeVat { get; set; } = null!;

    public decimal? PenaltyforUnPaidInstallment { get; set; }

    public int? PenaltyforUnPaidInstallmentVatId { get; set; }
    public virtual Vat PenaltyforUnPaidInstallmentVat { get; set; } = null!;

    public decimal? JoiningFee { get; set; }

    public int? JoiningFeeVatId { get; set; }
    public virtual Vat JoiningFeeVat { get; set; } = null!;

    public int? PenaltyChargedAfterDays { get; set; }

    public bool AssignToAllClubs { get; set; }

    #endregion

    #region Period

    public int PaymentIntervalId { get; set; }
    public virtual PaymentInterval PaymentInterval { get; set; } = null!;

    public int CommitmentPeriodId { get; set; }
    public virtual PaymentInterval CommitmentPeriod { get; set; } = null!;

    [Range(0, 12, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MinimumCancellationMonths { get; set; }
    
    [Range(0, 31, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MinimumCancellationDays { get; set; }
    
    [Range(0, 12, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MaximumCancellationMonths { get; set; }
    
    [Range(0, 31, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MaximumCancellationDays { get; set; }

    #endregion

    public virtual ICollection<SubscriptionPlanPaymentMethod> SubscriptionPlanPaymentMethods { get; set; } = new List<SubscriptionPlanPaymentMethod>();

    public virtual ICollection<SubscriptionPlanAssignedClub> SubscriptionPlanAssignedClubs { get; set; } = new List<SubscriptionPlanAssignedClub>();

    public virtual ICollection<SubscriptionPlanAvailableClub> SubscriptionPlanAvailableClubs { get; set; } = new List<SubscriptionPlanAvailableClub>();

    public virtual ICollection<SubscriptionPlanTag> SubscriptionPlanTags { get; set; } = new List<SubscriptionPlanTag>();

    public virtual ICollection<SubscriptionPlanRole> SubscriptionPlanRoles { get; set; } = new List<SubscriptionPlanRole>();

    public virtual ICollection<SubscriptionPlanApplication> SubscriptionPlanApplications { get; set; } = new List<SubscriptionPlanApplication>();
    
    public virtual ICollection<SubscriptionPlanSubscriptionPlanSetting> SubscriptionPlanAssignedSettings { get; set; } = new List<SubscriptionPlanSubscriptionPlanSetting>();

    public virtual ICollection<SubscriptionPlanSubscriptionPlanAddon> SubscriptionPlanSubscriptionPlanAddons { get; set; } = new List<SubscriptionPlanSubscriptionPlanAddon>();
}