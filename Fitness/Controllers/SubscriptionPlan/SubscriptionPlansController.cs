using AutoMapper;
using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class SubscriptionPlansController : CommonController<SubscriptionPlansController>
{
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly ISubscriptionPlanService subscriptionPlanService;
    private readonly ISubscriptionPlanDiscountService subscriptionPlanDiscountService;
    public SubscriptionPlansController(IMapper mapper, IConfiguration configuration, ISubscriptionPlanService subscriptionPlanService, ISubscriptionPlanDiscountService subscriptionPlanDiscountService)
    {
        this.mapper = mapper;
        this.configuration = configuration;
        this.subscriptionPlanService = subscriptionPlanService;
        this.subscriptionPlanDiscountService = subscriptionPlanDiscountService;
    }

    [HttpGet]
    [HttpGet("club/{clubId:int}/rule/{ruleId:int}")]
    public async Task<IActionResult> GetAll(int clubId = 0, int ruleId = 0)
    {
        var items = await subscriptionPlanService.GetAllByFilterAsync(clubId, ruleId);
        var result = mapper.Map<IReadOnlyList<SubscriptionPlanDto>>(items);
        return Ok(result);
    }

    [HttpGet("club/{clubId:int}")]
    public async Task<IActionResult> GetAllByClub(int clubId)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var items = await subscriptionPlanService.GetAllByClubAsync(clubId, access!.ApplicationId);
        var result = mapper.Map<IReadOnlyList<SubscriptionPlanDetailsDto>>(items);
        return Ok(result);
    }

    [HttpGet("mobile/club/{clubId:int}")]
    public async Task<IActionResult> GetAllForMobileByClub(int clubId)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var items = await subscriptionPlanService.GetAllByClubAsync(clubId, access!.ApplicationId);
        var discounts = await subscriptionPlanDiscountService.GetAllByClubAndSubscriptionPlansAsync(clubId, items.Select(x => x.Id).ToList());

        List<SubscriptionPlanMobileDto> plans = mapper.Map<List<SubscriptionPlanMobileDto>>(items);
        foreach (var plan in plans)
        {

            List<SubscriptionPlanDiscount> planDiscounts = discounts.Where(x =>
                (x.MembershipFeeDiscountType == MembershipFeeDiscountType.LowerByPercent || x.MembershipFeeDiscountType == MembershipFeeDiscountType.LowerByAmount)
                && (x.ApplyToAllSubscriptionPlans || x.SubscriptionPlanDiscountSubscriptionPlans.Any(y => y.SubscriptionPlanId == plan.Id))
            ).ToList();
            if (planDiscounts.Any())
            {
                decimal discountedPrice = plan.PlanPrice;
                foreach (SubscriptionPlanDiscount planDiscount in planDiscounts)
                {
                    if (planDiscount.MembershipFeeDiscountType == MembershipFeeDiscountType.LowerByPercent)
                    {
                        discountedPrice = discountedPrice * (100 - planDiscount.MembershipFeeDiscountValue!.Value) / 100;
                    }
                    else if (planDiscount.MembershipFeeDiscountType == MembershipFeeDiscountType.LowerByAmount)
                    {
                        discountedPrice = discountedPrice - planDiscount.MembershipFeeDiscountValue!.Value;
                    }
                }
                plan.DiscountedPrice = discountedPrice > 0 ? discountedPrice : 0.0m;
            }
        }
        return Ok(plans);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await subscriptionPlanService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("SubscriptionPlan Not Found");
        }

        item.SubscriptionPlanPaymentMethods = item.SubscriptionPlanPaymentMethods.Where(x => x.IsActive).ToList();
        item.SubscriptionPlanTags = item.SubscriptionPlanTags.Where(x => x.IsActive).ToList();
        item.SubscriptionPlanRoles = item.SubscriptionPlanRoles.Where(x => x.IsActive).ToList();
        item.SubscriptionPlanApplications = item.SubscriptionPlanApplications.Where(x => x.IsActive).ToList();
        item.SubscriptionPlanAssignedClubs = item.SubscriptionPlanAssignedClubs.Where(x => x.IsActive).ToList();
        item.SubscriptionPlanAvailableClubs = item.SubscriptionPlanAvailableClubs.Where(x => x.IsActive).ToList();
        item.SubscriptionPlanAssignedSettings = item.SubscriptionPlanAssignedSettings.Where(x => x.IsActive).ToList();
        var result = mapper.Map<SubscriptionPlanDetailsDto>(item);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(SubscriptionPlanPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<SubscriptionPlan>(model);
        item.CreatedBy = access!.UserId;
        item.SubscriptionPlanPaymentMethods = GetSubscriptionPlanPaymentMethods(access!.UserId, item.SubscriptionPlanPaymentMethods, model.PaymentMethodIds);
        item.SubscriptionPlanTags = GetSubscriptionPlanTags(access!.UserId, item.SubscriptionPlanTags, model.TagIds);
        item.SubscriptionPlanApplications = GetSubscriptionPlanApplications(access!.UserId, item.SubscriptionPlanApplications, model.ApplicationIds);
        item.SubscriptionPlanRoles = GetSubscriptionPlanRoles(access!.UserId, item.SubscriptionPlanRoles, model.RoleIds);
        item.SubscriptionPlanAssignedClubs = GetSubscriptionPlanAssignedClubs(access!.UserId, item.SubscriptionPlanAssignedClubs, model.AssignedClubIds);
        item.SubscriptionPlanAvailableClubs = GetSubscriptionPlanAvailableClubs(access!.UserId, item.SubscriptionPlanAvailableClubs, model.AvailableClubIds);
        item.SubscriptionPlanSubscriptionPlanAddons = GetSubscriptionPlanAddons(access!.UserId, item.SubscriptionPlanSubscriptionPlanAddons, model.AddonIds);

        await subscriptionPlanService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, mapper.Map<SubscriptionPlanDto>(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SubscriptionPlanPostDto model)
    {
        var existingItem = await subscriptionPlanService.GetByIdAsync(id);
        if (existingItem == null)
        {
            return BadRequest("SubscriptionPlan Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<SubscriptionPlan>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        item.CreatedBy = existingItem.CreatedBy;
        item.CreatedOn = existingItem.CreatedOn;
        item.SubscriptionPlanPaymentMethods = GetSubscriptionPlanPaymentMethods(access!.UserId, existingItem.SubscriptionPlanPaymentMethods, model.PaymentMethodIds);
        item.SubscriptionPlanTags = GetSubscriptionPlanTags(access!.UserId, existingItem.SubscriptionPlanTags, model.TagIds);
        item.SubscriptionPlanApplications = GetSubscriptionPlanApplications(access!.UserId, existingItem.SubscriptionPlanApplications, model.ApplicationIds);
        item.SubscriptionPlanRoles = GetSubscriptionPlanRoles(access!.UserId, existingItem.SubscriptionPlanRoles, model.RoleIds);
        item.SubscriptionPlanAssignedClubs = GetSubscriptionPlanAssignedClubs(access!.UserId, existingItem.SubscriptionPlanAssignedClubs, model.AssignedClubIds);
        item.SubscriptionPlanAvailableClubs = GetSubscriptionPlanAvailableClubs(access!.UserId, existingItem.SubscriptionPlanAvailableClubs, model.AvailableClubIds);
        item.SubscriptionPlanSubscriptionPlanAddons = GetSubscriptionPlanAddons(access!.UserId, item.SubscriptionPlanSubscriptionPlanAddons, model.AddonIds);

        await subscriptionPlanService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        int freeTrialSubscriptionPlanId = int.Parse(configuration["FreeTrialSubscriptionPlanId"] ?? "0");
        if (freeTrialSubscriptionPlanId == id)
        {
            return BadRequest("Access Denied, You Cannot Delete Free Trial Subscription");
        }

        var item = await subscriptionPlanService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("SubscriptionPlan Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await subscriptionPlanService.UpdateAsync(item);
        return NoContent();
    }

    private ICollection<SubscriptionPlanPaymentMethod> GetSubscriptionPlanPaymentMethods(int userId, ICollection<SubscriptionPlanPaymentMethod> items, List<int> selectedIds)
    {
        foreach (var item in items)
        {
            item.ModifiedBy = userId;
            item.ModifiedOn = DateTime.UtcNow;
            item.IsActive = selectedIds.Any(x => x == item.PaymentMethodId);
            if (item.IsActive)
            {
                selectedIds.Remove(item.PaymentMethodId);
            }
        }
        foreach (var id in selectedIds)
        {
            SubscriptionPlanPaymentMethod item = new SubscriptionPlanPaymentMethod();
            item.CreatedBy = userId;
            item.PaymentMethodId = id;
            items.Add(item);
        }
        return items;
    }

    private ICollection<SubscriptionPlanTag> GetSubscriptionPlanTags(int userId, ICollection<SubscriptionPlanTag> items, List<int> selectedIds)
    {
        foreach (var item in items)
        {
            item.ModifiedBy = userId;
            item.ModifiedOn = DateTime.UtcNow;
            item.IsActive = selectedIds.Any(x => x == item.TagId);
            if (item.IsActive)
            {
                selectedIds.Remove(item.TagId);
            }
        }
        foreach (var id in selectedIds)
        {
            SubscriptionPlanTag item = new SubscriptionPlanTag();
            item.CreatedBy = userId;
            item.TagId = id;
            items.Add(item);
        }
        return items;
    }

    private ICollection<SubscriptionPlanApplication> GetSubscriptionPlanApplications(int userId, ICollection<SubscriptionPlanApplication> items, List<int> selectedIds)
    {
        foreach (var item in items)
        {
            item.ModifiedBy = userId;
            item.ModifiedOn = DateTime.UtcNow;
            item.IsActive = selectedIds.Any(x => x == item.ApplicationId);
            if (item.IsActive)
            {
                selectedIds.Remove(item.ApplicationId);
            }
        }
        foreach (var id in selectedIds)
        {
            SubscriptionPlanApplication item = new SubscriptionPlanApplication();
            item.CreatedBy = userId;
            item.ApplicationId = id;
            items.Add(item);
        }
        return items;
    }

    private ICollection<SubscriptionPlanRole> GetSubscriptionPlanRoles(int userId, ICollection<SubscriptionPlanRole> items, List<int> selectedIds)
    {
        foreach (var item in items)
        {
            if (selectedIds.Any(x => x == item.RoleId))
            {
                selectedIds.Remove(item.RoleId);
            }
            else
            {
                item.DeletedBy = userId;
                item.DeletedOn = DateTime.UtcNow;
                item.IsDelete = true;
            }
        }
        foreach (var id in selectedIds)
        {
            SubscriptionPlanRole item = new SubscriptionPlanRole();
            item.CreatedBy = userId;
            item.RoleId = id;
            items.Add(item);
        }
        return items;
    }

    private ICollection<SubscriptionPlanAssignedClub> GetSubscriptionPlanAssignedClubs(int userId, ICollection<SubscriptionPlanAssignedClub> items, List<int> selectedIds)
    {
        foreach (var item in items)
        {
            item.ModifiedBy = userId;
            item.ModifiedOn = DateTime.UtcNow;
            item.IsActive = selectedIds.Any(x => x == item.ClubId);
            if (item.IsActive)
            {
                selectedIds.Remove(item.ClubId);
            }
        }
        foreach (var id in selectedIds)
        {
            SubscriptionPlanAssignedClub item = new SubscriptionPlanAssignedClub();
            item.CreatedBy = userId;
            item.ClubId = id;
            items.Add(item);
        }
        return items;
    }

    private ICollection<SubscriptionPlanAvailableClub> GetSubscriptionPlanAvailableClubs(int userId, ICollection<SubscriptionPlanAvailableClub> items, List<int> selectedIds)
    {
        foreach (var item in items)
        {
            if (selectedIds.Any(x => x == item.ClubId))
            {
                selectedIds.Remove(item.ClubId);
            }
            else
            {
                item.DeletedBy = userId;
                item.DeletedOn = DateTime.UtcNow;
                item.IsDelete = true;
            }
        }
        foreach (var id in selectedIds)
        {
            SubscriptionPlanAvailableClub item = new SubscriptionPlanAvailableClub();
            item.CreatedBy = userId;
            item.ClubId = id;
            items.Add(item);
        }
        return items;
    }

    private ICollection<SubscriptionPlanSubscriptionPlanAddon> GetSubscriptionPlanAddons(int userId, ICollection<SubscriptionPlanSubscriptionPlanAddon> items, List<int> selectedIds)
    {
        foreach (var item in items)
        {
            if (selectedIds.Any(x => x == item.SubscriptionPlanAddonId))
            {
                selectedIds.Remove(item.SubscriptionPlanAddonId);
            }
            else
            {
                item.DeletedBy = userId;
                item.DeletedOn = DateTime.UtcNow;
                item.IsDelete = true;
            }
        }
        foreach (var id in selectedIds)
        {
            SubscriptionPlanSubscriptionPlanAddon item = new SubscriptionPlanSubscriptionPlanAddon();
            item.CreatedBy = userId;
            item.SubscriptionPlanAddonId = id;
            items.Add(item);
        }
        return items;
    }
}