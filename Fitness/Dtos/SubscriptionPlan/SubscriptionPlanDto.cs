using Fitness.Core.Enums;

namespace Fitness.Dtos;
public class SubscriptionPlanDto
{
    #region General

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? MinimumAge { get; set; }

    public int? MaximumAge { get; set; }

    public string? FiscalName { get; set; }

    public PlanType PlanType { get; set; }

    public bool AdditionalPaymentPlan { get; set; }

    public bool IsActive { get; set; }

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

    public bool IsFreezeAvailable { get; set; }

    public int? PenaltyChargedAfterDays { get; set; }

    #endregion

    #region Period

    public int PaymentIntervalId { get; set; }

    public string PaymentIntervalName { get; set; } = string.Empty;

    public int CommitmentPeriodId { get; set; }

    public string CommitmentPeriodName { get; set; } = string.Empty;

    public int MinimumCancellationMonths { get; set; }

    public int MinimumCancellationDays { get; set; }

    public int MaximumCancellationMonths { get; set; }

    public int MaximumCancellationDays { get; set; }

    #endregion
      
    public bool AddToNewClubs { get; set; }
    
    public int? AccessRuleId { get; set; }
  
    public string AccessRuleName { get; set; } = string.Empty;
    
    public List<SubscriptionPlanSubscriptionPlanAddonDto> SubscriptionPlanSubscriptionPlanAddons { get; set; }
}