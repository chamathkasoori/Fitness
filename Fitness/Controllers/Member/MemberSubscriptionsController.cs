using AutoMapper;
using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Core.Enums;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class MemberSubscriptionsController : CommonController<MemberSubscriptionsController>
{
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMemberService memberService;
    private readonly ISubscriptionPlanService subscriptionPlanService;
    private readonly ISubscriptionPlanDiscountService subscriptionPlanDiscountService;
    private readonly IMemberSubscriptionService memberSubscriptionService;
    private readonly IMemberSubscriptionFreezeService memberSubscriptionFreezeService;
    private readonly IMemberSubscriptionTransferService memberSubscriptionTransferService;
    private readonly IInvoiceService invoiceService;
    public MemberSubscriptionsController(
        IMapper mapper,
        IConfiguration configuration,
        IMemberService memberService,
        ISubscriptionPlanService subscriptionPlanService,
        ISubscriptionPlanDiscountService subscriptionPlanDiscountService,
        IMemberSubscriptionService memberSubscriptionService,
        IMemberSubscriptionFreezeService memberSubscriptionFreezeService,
        IMemberSubscriptionTransferService memberSubscriptionTransferService, IInvoiceService invoiceService)
    {
        this.mapper = mapper;
        this.configuration = configuration;
        this.memberService = memberService;
        this.subscriptionPlanService = subscriptionPlanService;
        this.subscriptionPlanDiscountService = subscriptionPlanDiscountService;
        this.memberSubscriptionService = memberSubscriptionService;
        this.memberSubscriptionFreezeService = memberSubscriptionFreezeService;
        this.memberSubscriptionTransferService = memberSubscriptionTransferService;
        this.invoiceService = invoiceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await memberSubscriptionService.GetAllAsync();
        var result = mapper.Map<IReadOnlyList<MemberSubscriptionDto>>(items);
        return Ok(result);
    }

    [HttpGet("active")]
    [HttpGet("active/member/{memberId:int}")]
    public async Task<IActionResult> GetActiveMemberSubscriptions(int memberId = 0)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        if (memberId == 0)
        {
            memberId = access!.MemberId;
        }
        if (memberId == 0)
        {
            return BadRequest("Member Not Found");
        }
        var result = await memberSubscriptionService.GetActiveMemberSubscriptionsAsync(memberId);
        var items = mapper.Map<IReadOnlyList<MemberSubscriptionDto>>(result);
        foreach (var item in items)
        {
            if (item.Status == MemberSubscriptionStatus.Freezed)
            {
                List<MemberSubscriptionFreeze> memberSubscriptionFreezeList = await memberSubscriptionFreezeService.GetAllByMemberSubscriptionAsync(item.Id);
                var today = DateTime.UtcNow.Date;
                var currentFreeze = memberSubscriptionFreezeList.Find(x => x.StartDate.Date <= today && x.EndDate.Date >= today);
                if (currentFreeze != null)
                {
                    item.MemberSubscriptionFreezeId = currentFreeze!.Id;
                    item.FreezeFrom = currentFreeze!.StartDate;
                    item.FreezeTo = currentFreeze!.EndDate;
                }
            }
        }

        return Ok(items);
    }

    [HttpGet("archived")]
    [HttpGet("archived/member/{memberId:int}")]
    public async Task<IActionResult> GetArchivedMemberSubscriptions(int memberId = 0)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        if (memberId == 0)
        {
            memberId = access!.MemberId;
        }
        if (memberId == 0)
        {
            return BadRequest("Member Not Found");
        }
        var items = await memberSubscriptionService.GetArchivedMemberSubscriptionsAsync(memberId);
        var result = mapper.Map<IReadOnlyList<MemberSubscriptionDto>>(items);
        return Ok(result);
    }

    [HttpGet("transffered")]
    [HttpGet("transffered/member/{memberId:int}")]
    public async Task<IActionResult> GetTransfferedMemberSubscriptions(int memberId = 0)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        if (memberId == 0)
        {
            memberId = access!.MemberId;
        }
        if (memberId == 0)
        {
            return BadRequest("Member Not Found");
        }
        var items = await memberSubscriptionService.GetTransfferedMemberSubscriptionsAsync(memberId);
        var result = mapper.Map<IReadOnlyList<MemberSubscriptionDto>>(items);
        return Ok(result);
    }

    [HttpGet("current")]
    [HttpGet("current/member/{memberId:int}")]
    public async Task<IActionResult> GetMemberCurrentPlan(int memberId = 0)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        if (memberId == 0)
        {
            memberId = access!.MemberId;
        }
        if (memberId == 0)
        {
            return BadRequest("Member Not Found");
        }

        var item = await memberSubscriptionService.GetCurrentByMemberAsync(memberId);
        if (item.Id == 0)
        {
            return BadRequest("Member Subscription Not Found");
        }

        // Check If Current Plan Is Free Trial and Expired
        int freeTrialSubscriptionPlanId = int.Parse(configuration["FreeTrialSubscriptionPlanId"] ?? "0");
        if (item.SubscriptionPlanId == freeTrialSubscriptionPlanId && item.Status == MemberSubscriptionStatus.Current && item.EndDate < DateTime.UtcNow.Date)
        {
            item.Status = MemberSubscriptionStatus.Ended;
            item.ModifiedBy = access!.UserId;
            item.ModifiedOn = DateTime.UtcNow;
            await memberSubscriptionService.UpdateAsync(item);
        }

        var result = mapper.Map<MemberSubscriptionDto>(item);
        result.AvailableFreezeDays = result.FreezeDays;
        List<MemberSubscriptionFreeze> memberSubscriptionFreezeList = await memberSubscriptionFreezeService.GetAllByMemberSubscriptionAsync(result.Id);
        foreach (var memberSubscriptionFreeze in memberSubscriptionFreezeList)
        {
            int noOfDays = (memberSubscriptionFreeze.EndDate - memberSubscriptionFreeze.StartDate).Days + 1;
            result.AvailableFreezeDays = result.AvailableFreezeDays - noOfDays;
        }
        result.AvailableFreezeDays = result.AvailableFreezeDays < 0 ? 0 : result.AvailableFreezeDays;
        if (result.Status == MemberSubscriptionStatus.Freezed && memberSubscriptionFreezeList.Any())
        {
            var today = DateTime.UtcNow.Date;
            var currentFreeze = memberSubscriptionFreezeList.Find(x => x.StartDate <= today && x.EndDate >= today);
            if (currentFreeze != null)
            {
                result.MemberSubscriptionFreezeId = currentFreeze!.Id;
                result.FreezeFrom = currentFreeze!.StartDate;
                result.FreezeTo = currentFreeze!.EndDate;
            }
        }

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await memberSubscriptionService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Member Subscription Not Found");
        }
        var result = mapper.Map<MemberSubscriptionDto>(item);
        result.AvailableFreezeDays = result.FreezeDays;
        List<MemberSubscriptionFreeze> memberSubscriptionFreezeList = await memberSubscriptionFreezeService.GetAllByMemberSubscriptionAsync(result.Id);
        foreach (var memberSubscriptionFreeze in memberSubscriptionFreezeList)
        {
            int noOfDays = (memberSubscriptionFreeze.EndDate - memberSubscriptionFreeze.StartDate).Days + 1;
            result.AvailableFreezeDays = result.AvailableFreezeDays - noOfDays;
        }
        result.AvailableFreezeDays = result.AvailableFreezeDays < 0 ? 0 : result.AvailableFreezeDays;
        if (result.Status == MemberSubscriptionStatus.Freezed && memberSubscriptionFreezeList.Any())
        {
            var today = DateTime.UtcNow.Date;
            var currentFreeze = memberSubscriptionFreezeList.Find(x => x.StartDate <= today && x.EndDate >= today);
            if (currentFreeze != null)
            {
                result.MemberSubscriptionFreezeId = currentFreeze!.Id;
                result.FreezeFrom = currentFreeze!.StartDate;
                result.FreezeTo = currentFreeze!.EndDate;
            }
        }
        return Ok(result);
    }

    [HttpGet("{id:int}/Freeze")]
    public async Task<IActionResult> GetFreezesByMemberSubscriptionId(int id)
    {
        var items = await memberSubscriptionFreezeService.GetAllByMemberSubscriptionAsync(id);
        var result = mapper.Map<IReadOnlyList<MemberSubscriptionFreezeDto>>(items);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription(MemberSubscriptionRequest model)
    {
        // WE NEED TO REMOVE AFTER CLIENT IS FIXED
        if (model.SubscriptionPlanDiscountId > 0)
        {
            model.SubscriptionPlanDiscountIds.Add(model.SubscriptionPlanDiscountId);
        }

        var member = await memberService.GetByIdAsync(model.MemberId);
        if (member == null)
        {
            return BadRequest("Member Not Found");
        }

        var subscriptionPlan = await subscriptionPlanService.GetByIdAsync(model.SubscriptionPlanId);
        if (subscriptionPlan == null)
        {
            return BadRequest("Subscription Plan Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        if (member.IsGuest)
        {
            member.IsGuest = false;
            member.ModifiedBy = access!.UserId;
            member.ModifiedOn = DateTime.UtcNow;
            await memberService.UpdateAsync(member);

            // Check Is Curren Plan Is Free Trial
            int freeTrialSubscriptionPlanId = int.Parse(configuration["FreeTrialSubscriptionPlanId"] ?? "0");
            var freeTrialSubscriptionPlan = await memberSubscriptionService.GetBySubscriptionPlanAndMember(freeTrialSubscriptionPlanId, model.MemberId);
            if (freeTrialSubscriptionPlan.Id > 0)
            {
                freeTrialSubscriptionPlan.Status = MemberSubscriptionStatus.Ended;
                freeTrialSubscriptionPlan.ModifiedBy = access!.UserId;
                freeTrialSubscriptionPlan.ModifiedOn = DateTime.UtcNow;
                await memberSubscriptionService.UpdateAsync(freeTrialSubscriptionPlan);
            }
        }

        var mobileApplicationId = int.Parse(configuration["MobileApplicationId"] ?? "0");
        MemberSubscription memberSubscription = new MemberSubscription();
        memberSubscription.CreatedBy = access!.UserId;
        memberSubscription.MemberId = model.MemberId;
        memberSubscription.ApplicationId = access!.ApplicationId;
        memberSubscription.Status = MemberSubscriptionStatus.Current;
        memberSubscription.SubscriptionPlanId = subscriptionPlan.Id;
        foreach (int subscriptionPlanDiscountId in model.SubscriptionPlanDiscountIds)
        {
            var subscriptionPlanDiscount = await subscriptionPlanDiscountService.GetByIdAsync(subscriptionPlanDiscountId);
            if (subscriptionPlanDiscount != null)
            {
                MemberSubscriptionDiscount memberSubscriptionDiscount = new MemberSubscriptionDiscount();
                memberSubscriptionDiscount.SubscriptionPlanDiscount = subscriptionPlanDiscount;
                memberSubscriptionDiscount.SubscriptionPlanDiscountId = subscriptionPlanDiscount.Id;
                memberSubscriptionDiscount.CreatedBy = access!.UserId;
                memberSubscriptionDiscount.CreatedOn = DateTime.UtcNow;
                memberSubscription.MemberSubscriptionDiscounts.Add(memberSubscriptionDiscount);
            }
        }

        if (memberSubscription.ApplicationId == mobileApplicationId)
        {
            var subscriptionPlanDiscounts = await subscriptionPlanDiscountService.GetAllByClubAndSubscriptionPlansAsync(member.ClubId, new List<int>(subscriptionPlan.Id));
            foreach (SubscriptionPlanDiscount subscriptionPlanDiscount in subscriptionPlanDiscounts)
            {
                MemberSubscriptionDiscount memberSubscriptionDiscount = new MemberSubscriptionDiscount();
                memberSubscriptionDiscount.SubscriptionPlanDiscount = subscriptionPlanDiscount;
                memberSubscriptionDiscount.SubscriptionPlanDiscountId = subscriptionPlanDiscount.Id;
                memberSubscriptionDiscount.CreatedBy = access!.UserId;
                memberSubscriptionDiscount.CreatedOn = DateTime.UtcNow;
                memberSubscription.MemberSubscriptionDiscounts.Add(memberSubscriptionDiscount);
            }
        }

        await memberSubscriptionService.SaveAsync(memberSubscription, member, subscriptionPlan, model.PaymentMethod);
        return Ok(mapper.Map<MemberSubscriptionDto>(memberSubscription));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await memberSubscriptionService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("MemberSubscription Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;
        item.Member = null!;
        item.SubscriptionPlan = null!;
        item.MemberSubscriptionFreezes = null!;
        await memberSubscriptionService.UpdateAsync(item);

        try
        {
            var invoice = await invoiceService.GetByMemberSubscriptionIdAsync(item.Id);
            if (invoice == null)
                return NotFound("Invoice No Found");

            item.Member = invoice.MemberSubscription.Member;
            item.SubscriptionPlan = invoice.MemberSubscription.SubscriptionPlan;
            await invoiceService.SendMail(invoice.Id, MailTypes.CancelInvoice, invoice, item);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        await AdjustDays(item.MemberId, access!.UserId);

        return NoContent();
    }

    [HttpPost("Freeze")]
    public async Task<IActionResult> FreezeSubscription(MemberSubscriptionFreezeRequest model)
    {
        model.StartDate = model.StartDate.Date;
        model.EndDate = model.EndDate.Date;
        if (model.StartDate > model.EndDate)
        {
            return BadRequest("Invalid Date Selection");
        }
        if (model.MemberSubscriptionId == 0)
        {
            return BadRequest("Member Subscription Not Found");
        }
        var memberSubscription = await memberSubscriptionService.GetByIdAsync(model.MemberSubscriptionId);
        if (memberSubscription == null)
        {
            return BadRequest("Member Subscription Not Found");
        }
        if (memberSubscription.Status != MemberSubscriptionStatus.Current && memberSubscription.Status != MemberSubscriptionStatus.Freezed)
        {
            return BadRequest("Freeze Failed, Member Subscription Is " + memberSubscription.Status);
        }
        if (model.StartDate < memberSubscription.StartDate || model.StartDate > memberSubscription.EndDate)
        {
            return BadRequest("Selected Start Date is not in Member Subscription Date Range");
        }

        // Check For Available FreezeDays
        int availableFreezeDays = 0;
        if (memberSubscription.MemberSubscriptionAddons.Any(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze))
        {
            var addOn = memberSubscription.MemberSubscriptionAddons.Where(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze).Select(x => x.SubscriptionPlanAddon).FirstOrDefault();
            availableFreezeDays = addOn!.Quantity;
        }
        List<MemberSubscriptionFreeze> memberSubscriptionFreezeList = await memberSubscriptionFreezeService.GetAllByMemberSubscriptionAsync(model.MemberSubscriptionId);
        foreach (var memberSubscriptionFreeze in memberSubscriptionFreezeList)
        {
            int days = (memberSubscriptionFreeze.EndDate - memberSubscriptionFreeze.StartDate).Days + 1;
            availableFreezeDays = availableFreezeDays - days;
        }
        availableFreezeDays = availableFreezeDays < 0 ? 0 : availableFreezeDays;
        int freezeDays = (model.EndDate - model.StartDate).Days + 1;
        if (availableFreezeDays < freezeDays)
        {
            return BadRequest("Requested for " + freezeDays.ToString() + ", where AvailableFreezeDays is " + availableFreezeDays.ToString());
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<MemberSubscriptionFreeze>(model);
        item.CreatedBy = access!.UserId;
        item.StartDate = item.StartDate.Date;
        item.EndDate = item.EndDate.Date;
        await memberSubscriptionFreezeService.AddAsync(item);

        if (model.StartDate.Date == DateTime.UtcNow.Date)
        {
            memberSubscription.Status = MemberSubscriptionStatus.Freezed;
        }
        memberSubscription.ModifiedBy = access!.UserId;
        memberSubscription.ModifiedOn = DateTime.UtcNow;
        memberSubscription.EndDate = memberSubscription.EndDate.AddDays(freezeDays);
        memberSubscription.SubscriptionPlan = null!;
        await memberSubscriptionService.UpdateAsync(memberSubscription);
        await AdjustDays(memberSubscription.MemberId, access!.UserId);

        return NoContent();
    }

    [HttpPost("UnFreeze")]
    public async Task<IActionResult> UnFreezeSubscription(MemberSubscriptionUnFreezeRequest model)
    {
        var item = await memberSubscriptionFreezeService.GetByIdAsync(model.MemberSubscriptionFreezeId);
        if (item == null)
        {
            return BadRequest("MemberSubscriptionFreeze Not Found");
        }
        else if (item.IsDelete)
        {
            return BadRequest("MemberSubscriptionFreeze Is Already Removed");
        }
        else if (item.EndDate < DateTime.UtcNow.Date)
        {
            return BadRequest("MemberSubscriptionFreeze Is Already Expired");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        int remainingFreezeDays = 0;
        if (item.StartDate > DateTime.UtcNow.Date)
        {
            remainingFreezeDays = (item.EndDate - item.StartDate).Days + 1;
        }
        else
        {
            remainingFreezeDays = (item.EndDate - DateTime.UtcNow.Date).Days + 1;
        }

        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        item.MemberSubscription.EndDate = item.MemberSubscription.EndDate.AddDays(-remainingFreezeDays);
        item.MemberSubscription.Status = MemberSubscriptionStatus.Current;
        item.MemberSubscription.ModifiedBy = access!.UserId;
        item.MemberSubscription.ModifiedOn = DateTime.UtcNow;
        item.MemberSubscription.SubscriptionPlan = null!;
        await memberSubscriptionService.UpdateAsync(item.MemberSubscription);
        await AdjustDays(item.MemberSubscription.MemberId, access!.UserId);

        return NoContent();
    }

    [HttpPost("Transfer")]
    public async Task<IActionResult> TransferSubscription(MemberSubscriptionTransferRequest model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;

        if (model.TransferDate < DateTime.UtcNow.Date)
        {
            return BadRequest("Transfer Date should be a Future Date");
        }

        var currentSubscription = await memberSubscriptionService.GetByIdAsync(model.MemberSubscriptionId);
        if (currentSubscription == null)
        {
            return BadRequest("Member Subscription Not Found");
        }

        if (currentSubscription.MemberId == model.TransferMemberId)
        {
            return BadRequest("Transfer Member Cannot be the Same Member");
        }

        var transferMember = await memberService.GetByIdAsync(model.TransferMemberId);
        if (transferMember == null)
        {
            return BadRequest("Transfer Member Not Found");
        }

        // Check For The Status
        if (new List<string> { MemberSubscriptionStatus.Current, MemberSubscriptionStatus.Freezed, MemberSubscriptionStatus.NotStarted }.IndexOf(currentSubscription.Status) == -1)
        {
            return BadRequest("Cannot Transfer, Member Subscription Is " + currentSubscription.Status);
        }

        // Check For Available Days
        int remaningDays;
        if (currentSubscription.Status == MemberSubscriptionStatus.NotStarted)
        {
            remaningDays = (currentSubscription.EndDate - currentSubscription.StartDate).Days;
        }
        else
        {
            remaningDays = (currentSubscription.EndDate - DateTime.UtcNow.Date).Days;
        }
        if (remaningDays <= 0)
        {
            return BadRequest("Member Subscription Remaning Days Is Zero");
        }

        // Update The Current Member Subscrition Status to TRANSFERRED
        currentSubscription.Status = MemberSubscriptionStatus.Transferred;
        currentSubscription.EndDate = DateTime.UtcNow;
        currentSubscription.ModifiedBy = access!.UserId;
        currentSubscription.ModifiedOn = DateTime.UtcNow;
        currentSubscription.SubscriptionPlan = null!;
        await memberSubscriptionService.UpdateAsync(currentSubscription);

        // Create New Subscription
        MemberSubscription memberSubscription = new MemberSubscription();
        memberSubscription.CreatedBy = access!.UserId;
        memberSubscription.MemberId = transferMember.Id;
        memberSubscription.ApplicationId = access!.ApplicationId;
        memberSubscription.Status = MemberSubscriptionStatus.Current;
        memberSubscription.SubscriptionPlanId = currentSubscription.SubscriptionPlanId;
        memberSubscription.MemberSubscriptionDiscounts = currentSubscription.MemberSubscriptionDiscounts;

        // Calculate The Start Date and End Date
        var lastestMemberSubscription = await memberSubscriptionService.GetLatestByMemberAsync(model.TransferMemberId);
        if (lastestMemberSubscription == null)
        {
            memberSubscription.StartDate = model.TransferDate;
        }
        else if (lastestMemberSubscription.Status == MemberSubscriptionStatus.Current || lastestMemberSubscription.Status == MemberSubscriptionStatus.Freezed || lastestMemberSubscription.Status == MemberSubscriptionStatus.NotStarted)
        {
            memberSubscription.StartDate = lastestMemberSubscription.EndDate.Date.AddDays(1);// Already Member Has Plans
            memberSubscription.Status = MemberSubscriptionStatus.NotStarted;
        }
        memberSubscription.EndDate = memberSubscription.StartDate.AddDays(remaningDays);
        await memberSubscriptionService.AddAsync(memberSubscription);

        // If Guest We need to change it
        if (transferMember.IsGuest)
        {
            transferMember.IsGuest = false;
            transferMember.ModifiedBy = access!.UserId;
            transferMember.ModifiedOn = DateTime.UtcNow;
            await memberService.UpdateAsync(transferMember);
        }

        // Create The History
        MemberSubscriptionTransfer memberSubscriptionTransfer = new MemberSubscriptionTransfer();
        memberSubscriptionTransfer.OldMemberSubscriptionId = currentSubscription.Id;
        memberSubscriptionTransfer.NewMemberSubscriptionId = memberSubscription.Id;
        memberSubscriptionTransfer.NoOfDays = remaningDays;
        memberSubscriptionTransfer.CreatedBy = access!.UserId;
        await memberSubscriptionTransferService.AddAsync(memberSubscriptionTransfer);

        return NoContent();
    }

    private async Task AdjustDays(int memberId, int currentUserId)
    {
        // If Only One Active Subscription, we dont need to anything
        // If more we need to get the first ones end date and corret others
        var activeSubscriptionList = await memberSubscriptionService.GetActiveMemberSubscriptionsAsync(memberId);
        if (activeSubscriptionList.Count > 1)
        {
            DateTime endDate = activeSubscriptionList.First().EndDate.Date;
            foreach (var activeSubscription in activeSubscriptionList.Where(x => x.Id != activeSubscriptionList.First().Id))
            {
                int noOfDays = (activeSubscription.EndDate - activeSubscription.StartDate).Days + 1;
                activeSubscription.StartDate = endDate.AddDays(1);
                activeSubscription.EndDate = activeSubscription.StartDate.AddDays(noOfDays).AddSeconds(-1);
                activeSubscription.ModifiedBy = currentUserId;
                activeSubscription.ModifiedOn = DateTime.UtcNow;
                activeSubscription.SubscriptionPlan = null!;
                await memberSubscriptionService.UpdateAsync(activeSubscription);
                endDate = activeSubscription.EndDate.Date;
            }
        }
    }
}
