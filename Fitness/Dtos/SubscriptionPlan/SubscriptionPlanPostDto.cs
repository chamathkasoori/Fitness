using System.ComponentModel.DataAnnotations;
using Fitness.Core.Enums;

namespace Fitness.Dtos;
public class SubscriptionPlanPostDto
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

    public bool AssignToAllClubs { get; set; }

    #endregion

    #region Payment Plan

    public FeeTypes FeeType { get; set; }

    public decimal? MembershipFee { get; set; }

    public int? MembershipFeeVatId { get; set; }

    public decimal? AdministrationFee { get; set; }

    public int? AdministrationFeeVatId { get; set; }

    public decimal? PenaltyforUnPaidInstallment { get; set; }

    public int? PenaltyforUnPaidInstallmentVatId { get; set; }

    public decimal? JoiningFee { get; set; }

    public int? JoiningFeeVatId { get; set; }

    public int? PenaltyChargedAfterDays { get; set; }

    #endregion

    #region Period

    public int PaymentIntervalId { get; set; }

    public int CommitmentPeriodId { get; set; }

    public bool AddToNewClubs { get; set; }

    public int? AccessRuleId { get; set; }

    public bool IsActive { get; set; } = true;


    [Range(0, 12, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MinimumCancellationMonths { get; set; }

    [Range(0, 31, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MinimumCancellationDays { get; set; }

    [Range(0, 12, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MaximumCancellationMonths { get; set; }

    [Range(0, 31, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int MaximumCancellationDays { get; set; }

    #endregion

    public List<int> PaymentMethodIds { get; set; } = new List<int>();
    public List<int> TagIds { get; set; } = new List<int>();
    public List<int> ApplicationIds { get; set; } = new List<int>();
    public List<int> RoleIds { get; set; } = new List<int>();
    public List<int> AssignedClubIds { get; set; } = new List<int>();
    public List<int> AvailableClubIds { get; set; } = new List<int>();
    public List<int> AddonIds { get; set; } = new List<int>();
    public List<SubscriptionPlanSubscriptionPlanSettingDto> SubscriptionPlanSubscriptionPlanSettings { get; set; } = new List<SubscriptionPlanSubscriptionPlanSettingDto>();
}