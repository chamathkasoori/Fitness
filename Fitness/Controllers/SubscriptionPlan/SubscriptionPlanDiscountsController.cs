using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class SubscriptionPlanDiscountsController : CommonController<SubscriptionPlanDiscountsController>
{
    private readonly IMapper mapper;
    private readonly ISubscriptionPlanDiscountService subscriptionPlanDiscountService;
    private readonly ISubscriptionPlanDiscountCombinationService subscriptionPlanDiscountCombinationService;
    public SubscriptionPlanDiscountsController(
        IMapper mapper,
        ISubscriptionPlanDiscountService subscriptionPlanDiscountService,
        ISubscriptionPlanDiscountCombinationService subscriptionPlanDiscountCombinationService
    )
    {
        this.mapper = mapper;
        this.subscriptionPlanDiscountService = subscriptionPlanDiscountService;
        this.subscriptionPlanDiscountCombinationService = subscriptionPlanDiscountCombinationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await subscriptionPlanDiscountService.GetAllAsync();
        var result = mapper.Map<IReadOnlyList<SubscriptionPlanDiscountDto>>(items);
        return Ok(result);
    }

    [HttpGet("SubscriptionPlan/{subscriptionPlanId:int}")]
    public async Task<IActionResult> GetAllBySubscriptionPlanAsync(int subscriptionPlanId)
    {
        var items = await subscriptionPlanDiscountService.GetAllBySubscriptionPlanAsync(subscriptionPlanId);
        var result = mapper.Map<IReadOnlyList<SubscriptionPlanDiscountDto>>(items);
        return Ok(result);
    }

    [HttpGet("Combined/{subscriptionPlanDiscountId:int}")]
    public async Task<IActionResult> GetAllCombinedBySubscriptionPlanAsync(int subscriptionPlanDiscountId)
    {
        var combinedItems = await subscriptionPlanDiscountCombinationService.GetAllCombinedAsync(subscriptionPlanDiscountId);
        var items = combinedItems.Select(x => x.CombinedSubscriptionPlanDiscount).ToList();
        var result = mapper.Map<IReadOnlyList<SubscriptionPlanDiscountDto>>(items);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await subscriptionPlanDiscountService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Subscription Plan Discount Not Found.");
        }
        var combinedItems = await subscriptionPlanDiscountCombinationService.GetAllCombinedAsync(id);
        var result = mapper.Map<SubscriptionPlanDiscountDto>(item);
        result.CombinedDiscountIds = combinedItems.Select(x=> x.CombinedSubscriptionPlanDiscountId).ToList();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(SubscriptionPlanDiscountPostDto model)
    {
        if (await subscriptionPlanDiscountService.IsNameExist(0, model.Name))
        {
            return BadRequest("Name Already Exist");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<SubscriptionPlanDiscount>(model);
        item.CreatedBy = access!.UserId;
        if (!model.ApplyToAllSubscriptionPlans)
        {
            foreach (int subscriptionPlanId in model.SubscriptionPlanIds)
            {
                var subscriptionPlanMapping = new SubscriptionPlanDiscountSubscriptionPlan();
                subscriptionPlanMapping.SubscriptionPlanId = subscriptionPlanId;
                subscriptionPlanMapping.CreatedBy = access!.UserId;
                item.SubscriptionPlanDiscountSubscriptionPlans.Add(subscriptionPlanMapping);
            }
        }
        if (!model.ApplyToAllClubs)
        {
            foreach (int clubId in model.ClubIds)
            {
                var club = new SubscriptionPlanDiscountClub();
                club.ClubId = clubId;
                club.CreatedBy = access!.UserId;
                item.SubscriptionPlanDiscountClubs.Add(club);
            }
        }
        if (model.OnlyForChoosenRoles)
        {
            foreach (int roleId in model.RoleIds)
            {
                var role = new SubscriptionPlanDiscountRole();
                role.RoleId = roleId;
                role.CreatedBy = access!.UserId;
                item.SubscriptionPlanDiscountRoles.Add(role);
            }
        }
        if (model.OnlyForChoosenApplications)
        {
            foreach (int applicationId in model.ApplicationIds)
            {
                var application = new SubscriptionPlanDiscountApplication();
                application.ApplicationId = applicationId;
                application.CreatedBy = access!.UserId;
                item.SubscriptionPlanDiscountApplications.Add(application);
            }
        }
        await subscriptionPlanDiscountService.AddAsync(item);

        // Check For Combined Discounts
        foreach (int combinedDiscountId in model.CombinedDiscountIds)
        {
            SubscriptionPlanDiscountCombination combinationItem = new SubscriptionPlanDiscountCombination();
            combinationItem.SubscriptionPlanDiscountId = item.Id;
            combinationItem.CombinedSubscriptionPlanDiscountId = combinedDiscountId;
            combinationItem.CreatedBy = access!.UserId;
            await subscriptionPlanDiscountCombinationService.AddAsync(combinationItem);
        }

        return Ok(mapper.Map<SubscriptionPlanDiscountDto>(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SubscriptionPlanDiscountPostDto model)
    {
        var existingItem = await subscriptionPlanDiscountService.GetByIdAsync(id);
        if (existingItem == null)
        {
            return BadRequest("Subscription Plan Discount Not Found.");
        }
        if (await subscriptionPlanDiscountService.IsNameExist(id, model.Name))
        {
            return BadRequest("Name Already Exist");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<SubscriptionPlanDiscount>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        item.CreatedBy = existingItem.CreatedBy;
        item.CreatedOn = existingItem.CreatedOn;
        item.SubscriptionPlanDiscountSubscriptionPlans = existingItem.SubscriptionPlanDiscountSubscriptionPlans;
        item.SubscriptionPlanDiscountClubs = existingItem.SubscriptionPlanDiscountClubs;
        item.SubscriptionPlanDiscountRoles = existingItem.SubscriptionPlanDiscountRoles;
        item.SubscriptionPlanDiscountApplications = existingItem.SubscriptionPlanDiscountApplications;

        if (model.ApplyToAllSubscriptionPlans)
        {
            foreach (var subscriptionPlanSubscriptionPlanDiscount in item.SubscriptionPlanDiscountSubscriptionPlans)
            {
                subscriptionPlanSubscriptionPlanDiscount.IsActive = false;
                subscriptionPlanSubscriptionPlanDiscount.ModifiedBy = access!.UserId;
                subscriptionPlanSubscriptionPlanDiscount.ModifiedOn = DateTime.UtcNow;
            }
        }
        else
        {
            foreach (var subscriptionPlanSubscriptionPlanDiscount in item.SubscriptionPlanDiscountSubscriptionPlans)
            {
                subscriptionPlanSubscriptionPlanDiscount.ModifiedBy = access!.UserId;
                subscriptionPlanSubscriptionPlanDiscount.ModifiedOn = DateTime.UtcNow;
                subscriptionPlanSubscriptionPlanDiscount.IsActive = model.SubscriptionPlanIds.Any(x => x == subscriptionPlanSubscriptionPlanDiscount.SubscriptionPlanId);
                if (subscriptionPlanSubscriptionPlanDiscount.IsActive)
                {
                    model.SubscriptionPlanIds.Remove(subscriptionPlanSubscriptionPlanDiscount.SubscriptionPlanId);
                }
            }
            foreach (int subscriptionPlanId in model.SubscriptionPlanIds)
            {
                var subscriptionPlanSubscriptionPlanDiscount = new SubscriptionPlanDiscountSubscriptionPlan();
                subscriptionPlanSubscriptionPlanDiscount.CreatedBy = access!.UserId;
                subscriptionPlanSubscriptionPlanDiscount.SubscriptionPlanId = subscriptionPlanId;
                item.SubscriptionPlanDiscountSubscriptionPlans.Add(subscriptionPlanSubscriptionPlanDiscount);
            }
        }

        if (model.ApplyToAllClubs)
        {
            foreach (var subscriptionPlanDiscountClub in item.SubscriptionPlanDiscountClubs)
            {
                subscriptionPlanDiscountClub.IsActive = false;
                subscriptionPlanDiscountClub.ModifiedBy = access!.UserId;
                subscriptionPlanDiscountClub.ModifiedOn = DateTime.UtcNow;
            }
        }
        else
        {
            foreach (var subscriptionPlanDiscountClub in item.SubscriptionPlanDiscountClubs)
            {
                subscriptionPlanDiscountClub.ModifiedBy = access!.UserId;
                subscriptionPlanDiscountClub.ModifiedOn = DateTime.UtcNow;
                subscriptionPlanDiscountClub.IsActive = model.ClubIds.Any(x => x == subscriptionPlanDiscountClub.ClubId);
                if (subscriptionPlanDiscountClub.IsActive)
                {
                    model.ClubIds.Remove(subscriptionPlanDiscountClub.ClubId);
                }
            }
            foreach (int clubId in model.ClubIds)
            {
                var subscriptionPlanDiscountClub = new SubscriptionPlanDiscountClub();
                subscriptionPlanDiscountClub.CreatedBy = access!.UserId;
                subscriptionPlanDiscountClub.ClubId = clubId;
                item.SubscriptionPlanDiscountClubs.Add(subscriptionPlanDiscountClub);
            }
        }

        if (!model.OnlyForChoosenRoles)
        {
            foreach (var subscriptionPlanDiscountRole in item.SubscriptionPlanDiscountRoles)
            {
                subscriptionPlanDiscountRole.IsActive = false;
                subscriptionPlanDiscountRole.ModifiedBy = access!.UserId;
                subscriptionPlanDiscountRole.ModifiedOn = DateTime.UtcNow;
            }
        }
        else
        {
            foreach (var subscriptionPlanDiscountRole in item.SubscriptionPlanDiscountRoles)
            {
                subscriptionPlanDiscountRole.ModifiedBy = access!.UserId;
                subscriptionPlanDiscountRole.ModifiedOn = DateTime.UtcNow;
                subscriptionPlanDiscountRole.IsActive = model.RoleIds.Any(x => x == subscriptionPlanDiscountRole.RoleId);
                if (subscriptionPlanDiscountRole.IsActive)
                {
                    model.RoleIds.Remove(subscriptionPlanDiscountRole.RoleId);
                }
            }
            foreach (int roleId in model.RoleIds)
            {
                var role = new SubscriptionPlanDiscountRole();
                role.RoleId = roleId;
                role.CreatedBy = access!.UserId;
                item.SubscriptionPlanDiscountRoles.Add(role);
            }
        }

        if (!model.OnlyForChoosenApplications)
        {
            foreach (var subscriptionPlanDiscountApplication in item.SubscriptionPlanDiscountApplications)
            {
                subscriptionPlanDiscountApplication.IsActive = false;
                subscriptionPlanDiscountApplication.ModifiedBy = access!.UserId;
                subscriptionPlanDiscountApplication.ModifiedOn = DateTime.UtcNow;
            }
        }
        else
        {
            foreach (var subscriptionPlanDiscountApplication in item.SubscriptionPlanDiscountApplications)
            {
                subscriptionPlanDiscountApplication.ModifiedBy = access!.UserId;
                subscriptionPlanDiscountApplication.ModifiedOn = DateTime.UtcNow;
                subscriptionPlanDiscountApplication.IsActive = model.ApplicationIds.Any(x => x == subscriptionPlanDiscountApplication.ApplicationId);
                if (subscriptionPlanDiscountApplication.IsActive)
                {
                    model.ApplicationIds.Remove(subscriptionPlanDiscountApplication.ApplicationId);
                }
            }
            foreach (int applicationId in model.ApplicationIds)
            {
                var application = new SubscriptionPlanDiscountApplication();
                application.ApplicationId = applicationId;
                application.CreatedBy = access!.UserId;
                item.SubscriptionPlanDiscountApplications.Add(application);
            }
        }

        await subscriptionPlanDiscountService.UpdateAsync(item);

        // Check For Combined Discounts
        List<SubscriptionPlanDiscountCombination> combinedDiscountList = await subscriptionPlanDiscountCombinationService.GetAllCombinedAsync(item.Id);
        foreach (var combinationItem in combinedDiscountList)
        {
            combinationItem.ModifiedBy = access!.UserId;
            combinationItem.ModifiedOn = DateTime.UtcNow;
            if (model.CombinedDiscountIds.Any(x => x == combinationItem.CombinedSubscriptionPlanDiscountId))
            {
                model.CombinedDiscountIds.Remove(combinationItem.CombinedSubscriptionPlanDiscountId);
            }
            else
            {
                combinationItem.DeletedBy = access!.UserId;
                combinationItem.DeletedOn = DateTime.UtcNow;
                combinationItem.IsDelete = true;
            }
            await subscriptionPlanDiscountCombinationService.UpdateAsync(combinationItem);
        }
        foreach (int combinedDiscountId in model.CombinedDiscountIds)
        {
            SubscriptionPlanDiscountCombination combinationItem = new SubscriptionPlanDiscountCombination();
            combinationItem.SubscriptionPlanDiscountId = item.Id;
            combinationItem.CombinedSubscriptionPlanDiscountId = combinedDiscountId;
            combinationItem.CreatedBy = access!.UserId;
            await subscriptionPlanDiscountCombinationService.AddAsync(combinationItem);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await subscriptionPlanDiscountService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Subscription Plan Discount Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;
        foreach (var subscriptionPlanDiscountClub in item.SubscriptionPlanDiscountClubs)
        {
            subscriptionPlanDiscountClub.DeletedBy = access!.UserId;
            subscriptionPlanDiscountClub.DeletedOn = DateTime.UtcNow;
            subscriptionPlanDiscountClub.IsDelete = true;
        }
        foreach (var subscriptionPlanDiscountRole in item.SubscriptionPlanDiscountRoles)
        {
            subscriptionPlanDiscountRole.DeletedBy = access!.UserId;
            subscriptionPlanDiscountRole.DeletedOn = DateTime.UtcNow;
            subscriptionPlanDiscountRole.IsDelete = true;
        }
        await subscriptionPlanDiscountService.UpdateAsync(item);
        return NoContent();
    }
}