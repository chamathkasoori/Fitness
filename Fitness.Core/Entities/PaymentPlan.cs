using Fitness.Core.Entities.Base;
using Fitness.Core.Enums;

namespace Fitness.Core.Entities;
public class PaymentPlan : BaseEntity
{
    public FeeTypes? Fees { get; set; }

    public decimal? MembershipFee { get; set; }

    public int? MembershipFeeVatId { get; set; }
    public Vat? MembershipFeeVat { get; set; }

    public decimal? JoiningFee { get; set; }

    public int? JoiningFeeVatId { get; set; }
    public Vat? JoiningFeeVat { get; set; }

    public decimal? AdministrationFee { get; set; }

    public int? AdminFeeVatId { get; set; }
    public Vat? AdminFeeVat { get; set; }

    public decimal? DepositValue { get; set; }

    public decimal? FreezeAvailable { get; set; }

    public int? FreezeAvailableVatId { get; set; }
    public Vat? FreezeAvailableVat { get; set; }

    public decimal? AllowableDebit { get; set; }

    public decimal? PenaltyforUnPaidInstallment { get; set; }

    public int? PenaltyChargedAfterDays { get; set; }

    public int? PenaltyforUnPaidInstallmentVatId { get; set; }
    public Vat? PenaltyforUnPaidInstallmentVat { get; set; }
}