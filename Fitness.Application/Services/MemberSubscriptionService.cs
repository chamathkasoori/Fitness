using AutoMapper;
using Fitness.Core.Common.Xero;
using Fitness.Core.Common.Xero.Contact;
using Fitness.Core.Common.Xero.TrackingCategory;
using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Fitness.Application.Services;
public class MemberSubscriptionService : IMemberSubscriptionService
{
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMemberSubscriptionRepository memberSubscriptionRepository;
    private readonly IXeroService xeroService;
    private readonly IInvoiceService invoiceService;
    private readonly IMemberSubscriptionTransactionService memberSubscriptionTransactionService;
    private readonly ISubscriptionPlanAddonService subscriptionPlanAddonService;
    public MemberSubscriptionService(
        IMapper mapper,
        IConfiguration configuration,
        IMemberSubscriptionRepository memberSubscriptionRepository,
        IXeroService xeroService,
        IInvoiceService invoiceService,
        IMemberSubscriptionTransactionService memberSubscriptionTransactionService,
        ISubscriptionPlanAddonService subscriptionPlanAddonService
    )
    {
        this.mapper = mapper;
        this.configuration = configuration;
        this.memberSubscriptionRepository = memberSubscriptionRepository;
        this.xeroService = xeroService;
        this.invoiceService = invoiceService;
        this.memberSubscriptionTransactionService = memberSubscriptionTransactionService;
        this.subscriptionPlanAddonService = subscriptionPlanAddonService;
    }

    async Task<IReadOnlyList<MemberSubscription>> IGenericService<MemberSubscription>.GetAllAsync()
    {
        return await memberSubscriptionRepository.GetAllAsync();
    }

    public async Task<List<MemberSubscription>> GetActiveMemberSubscriptionsAsync(int memberId)
    {
        return await memberSubscriptionRepository.GetActiveMemberSubscriptionsAsync(memberId);
    }

    public async Task<List<MemberSubscription>> GetArchivedMemberSubscriptionsAsync(int memberId)
    {
        return await memberSubscriptionRepository.GetArchivedMemberSubscriptionsAsync(memberId);
    }

    public async Task<List<MemberSubscription>> GetTransfferedMemberSubscriptionsAsync(int memberId)
    {
        return await memberSubscriptionRepository.GetTransfferedMemberSubscriptionsAsync(memberId);
    }

    public async Task<List<MemberSubscription>> GetAllFreezedExpiredAsync()
    {
        return await memberSubscriptionRepository.GetAllFreezedExpiredAsync();
    }

    public async Task<List<MemberSubscription>> GetAllCurrentExpiredAsync()
    {
        return await memberSubscriptionRepository.GetAllCurrentExpiredAsync();
    }

    public async Task<List<MemberSubscription>> GetAllNotStartedActiveAsync()
    {
        return await memberSubscriptionRepository.GetAllNotStartedActiveAsync();
    }

    public async Task<List<MemberSubscription>> GetAllForFreezeAsync()
    {
        return await memberSubscriptionRepository.GetAllForFreezeAsync();
    }

    async Task<MemberSubscription?> IGenericService<MemberSubscription>.GetByIdAsync(int id)
    {
        return await memberSubscriptionRepository.GetByIdAsync(id);
    }

    public async Task<MemberSubscription> GetCurrentByMemberAsync(int memberId)
    {
        return await memberSubscriptionRepository.GetCurrentByMemberAsync(memberId);
    }

    public async Task<MemberSubscription> GetBySubscriptionPlanAndMember(int subscriptionPlanId, int memberId)
    {
        return await memberSubscriptionRepository.GetBySubscriptionPlanAndMember(subscriptionPlanId, memberId);
    }

    public async Task<MemberSubscription?> GetLatestByMemberAsync(int memberId)
    {
        return await memberSubscriptionRepository.GetLatestByMemberAsync(memberId);
    }

    async Task IGenericService<MemberSubscription>.AddAsync(MemberSubscription entity)
    {
        await memberSubscriptionRepository.AddAsync(entity);
    }

    public async Task SaveAsync(MemberSubscription memberSubscription, Member member, SubscriptionPlan subscriptionPlan, string paymentMethod)
    {
        #region Calculate Amount
        memberSubscription.SubscriptionPlanAmount = subscriptionPlan.MembershipFee!.Value;
        memberSubscription.Amount = subscriptionPlan.MembershipFee!.Value;
        foreach (MemberSubscriptionDiscount memberSubscriptionDiscount in memberSubscription.MemberSubscriptionDiscounts)
        {
            if (memberSubscriptionDiscount.SubscriptionPlanDiscount.MembershipFeeDiscountType == MembershipFeeDiscountType.LowerByPercent)
            {
                memberSubscription.Amount = memberSubscription.Amount * (100 - memberSubscriptionDiscount.SubscriptionPlanDiscount.MembershipFeeDiscountValue!.Value) / 100;
            }
            else if (memberSubscriptionDiscount.SubscriptionPlanDiscount.MembershipFeeDiscountType == MembershipFeeDiscountType.LowerByAmount)
            {
                memberSubscription.Amount = memberSubscription.Amount - memberSubscriptionDiscount.SubscriptionPlanDiscount.MembershipFeeDiscountValue!.Value;
            }
        }
        memberSubscription.Amount = memberSubscription.Amount > 0 ? memberSubscription.Amount : 0;
        #endregion Calculate Amount

        #region StartDate and EndDate
        var lastestMemberSubscription = await GetLatestByMemberAsync(memberSubscription.MemberId);
        if (lastestMemberSubscription == null)
        {
            memberSubscription.StartDate = DateTime.UtcNow.Date;
        }
        else if (lastestMemberSubscription.Status == MemberSubscriptionStatus.Current || lastestMemberSubscription.Status == MemberSubscriptionStatus.Freezed || lastestMemberSubscription.Status == MemberSubscriptionStatus.NotStarted)
        {
            memberSubscription.StartDate = lastestMemberSubscription.EndDate.Date.AddDays(1);// Already Member Has Plans
            if (memberSubscription.Status == MemberSubscriptionStatus.Current)
            {
                memberSubscription.Status = MemberSubscriptionStatus.NotStarted;
            }
        }
        memberSubscription.EndDate = Utility.GetEndDate(memberSubscription.StartDate, subscriptionPlan.PaymentInterval);
        foreach (MemberSubscriptionDiscount memberSubscriptionDiscount in memberSubscription.MemberSubscriptionDiscounts)
        {
            if (memberSubscriptionDiscount.SubscriptionPlanDiscount.MembershipFeeDiscountType == MembershipFeeDiscountType.FreeMonthAfter)
            {
                var noOfMonths = int.Parse(memberSubscriptionDiscount.SubscriptionPlanDiscount.MembershipFeeDiscountValue!.Value.ToString());
                memberSubscription.EndDate = memberSubscription.EndDate.AddMonths(noOfMonths);
            }
        }
        #endregion StartDate and EndDate

        #region Addons
        List<SubscriptionPlanAddon> addonList = await subscriptionPlanAddonService.GetBySubscriptionPlanAsync(subscriptionPlan.Id);
        foreach (SubscriptionPlanAddon subscriptionPlanAddon in addonList)
        {
            MemberSubscriptionAddon memberSubscriptionAddon = new MemberSubscriptionAddon();
            memberSubscriptionAddon.SubscriptionPlanAddonId = subscriptionPlanAddon.Id;
            memberSubscriptionAddon.CreatedBy = memberSubscription.CreatedBy;
            memberSubscription.MemberSubscriptionAddons.Add(memberSubscriptionAddon);
        }
        #endregion Addons

        foreach (MemberSubscriptionDiscount memberSubscriptionDiscount in memberSubscription.MemberSubscriptionDiscounts)
        {
            memberSubscriptionDiscount.SubscriptionPlanDiscount = null!;
        }
        await memberSubscriptionRepository.AddAsync(memberSubscription);

        if (!member.IsGuest)
        {
            MemberSubscriptionTransaction memberSubscriptionTransaction = new MemberSubscriptionTransaction();
            memberSubscriptionTransaction.MemberSubscriptionId = memberSubscription.Id;
            memberSubscriptionTransaction.Amount = memberSubscription.Amount;
            memberSubscriptionTransaction.OperationType = OperationType.Membership;
            memberSubscriptionTransaction.PaymentMethod = paymentMethod;
            memberSubscriptionTransaction.TransactionDate = DateTime.UtcNow;
            memberSubscriptionTransaction.VatPercentage = subscriptionPlan.MembershipFeeVat.Percentage;
            memberSubscriptionTransaction.Description = subscriptionPlan.Name;
            memberSubscriptionTransaction.CreatedBy = memberSubscription.CreatedBy;
            await memberSubscriptionTransactionService.AddAsync(memberSubscriptionTransaction);

            // Guest will have "Free Trial Subscription Plan" Initially, no need invoice for that
            var contact = await xeroService.CreateContactIfNotExistAsync(member);
            var tracking = await xeroService.CreateTrackingCategoryIfNotExistAsync(member);
            var invoiceResp = await CreateInvoice(memberSubscription, member, subscriptionPlan, contact, tracking);
            await MakePayment(invoiceResp.Invoices[0]);

            var invoice = mapper.Map<Invoice>(invoiceResp.Invoices[0]);
            invoice.MemberSubscriptionId = memberSubscription.Id;
            invoice.AmountPaid = invoice.AmountDue;
            await invoiceService.AddAsync(invoice);

            try
            {
                await invoiceService.SendMail(invoice.Id, MailTypes.Invoice, invoice, new MemberSubscription { Member = member, SubscriptionPlan = subscriptionPlan });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    async Task IGenericService<MemberSubscription>.UpdateAsync(MemberSubscription entity)
    {
        await memberSubscriptionRepository.UpdateAsync(entity);
    }

    private async Task<ResponseInvoicesDto> CreateInvoice(MemberSubscription memberSubscription, Member member, SubscriptionPlan plan, XeroContactDto contact, XeroTrackingCategoryDto tracking)
    {
        try
        {
            var invoiceNb = $"{memberSubscription.CreatedOn:hhmmss}/R/{memberSubscription.CreatedOn:MM}/{memberSubscription.CreatedOn:yy}";
            var price = memberSubscription.Amount;

            var vat = plan.MembershipFeeVat;
            var vatPercentage = vat?.Percentage ?? 15;
            var netPrice = price * (100 - vatPercentage) / 100;// 15% Calculation

            var invoice = new CreateInvoicesDto()
            {
                Invoices = new List<CreateInvoice>
                {
                    new()
                    {
                        Contact = new InvoiceContact { ContactID = contact.ContactID },
                        Date = memberSubscription.CreatedOn,
                        InvoiceNumber = invoiceNb,
                        Type = "ACCREC",
                        DueDate = memberSubscription.EndDate,
                        Status = "AUTHORISED",
                        LineItems = new List<LineItem>
                        {
                            new()
                            {
                                Description = plan.Name,
                                Quantity = 1,
                                UnitAmount = Math.Round(netPrice, 2),
                                TaxType = configuration["Xero:TaxType"] ?? string.Empty,
                                AccountCode = configuration["Xero:InvoiceAccount"] ?? string.Empty,
                                Tracking = new List<PostTracking>
                                {
                                    new()
                                    {
                                        Name = tracking.Name,
                                        Option = tracking.Options.Find(t => t.Name == member.Club.Name.Split("-")[0].Trim())?.Name ?? string.Empty
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response = await xeroService.MakeAccountingPutRequest("Invoice", invoice);
            return JsonConvert.DeserializeObject<ResponseInvoicesDto>(response) ?? new ResponseInvoicesDto();
        }
        catch (Exception e)
        {
            throw new Exception("Exception when calling apiInstance.CreateInvoices: " + e.Message);
        }
    }

    private async Task MakePayment(InvoiceResponseDto invoice)
    {
        try
        {
            var payment = new MakePaymentDto
            {
                Invoice = new PaymentInvoice { InvoiceID = invoice.InvoiceID },
                Amount = invoice.AmountDue,
                Account = new Account { Code = configuration["Xero:PaymentAccount"] ?? string.Empty },
                Date = DateTime.UtcNow
            };

            await xeroService.MakeAccountingPutRequest("Payments", payment);
        }
        catch (Exception e)
        {
            throw new Exception("Exception when calling apiInstance.MakePayments: " + e.Message);
        }
    }
}