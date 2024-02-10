using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class AccessRulesController : CommonController<AccessRulesController>
{
    private readonly IMapper mapper;
    private readonly IAccessRuleService accessRuleService;
    private readonly IAccessRuleItemService accessRuleItemService;
    private readonly ISubscriptionPlanService subscriptionPlanService;
    public AccessRulesController(IMapper mapper, IAccessRuleService accessRuleService, IAccessRuleItemService accessRuleItemService, ISubscriptionPlanService subscriptionPlanService)
    {
        this.mapper = mapper;
        this.accessRuleService = accessRuleService;
        this.accessRuleItemService = accessRuleItemService;
        this.subscriptionPlanService = subscriptionPlanService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<AccessRuleDto>> GetAll()
    {
        var items = await accessRuleService.GetAllAsync();
        return mapper.Map<IReadOnlyList<AccessRuleDto>>(items);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await accessRuleService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("AccessRule Not Found");
        }
        return Ok(mapper.Map<AccessRuleDto>(item));
    }

    [HttpPost]
    public async Task<IActionResult> Add(AccessRulePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<AccessRule>(model);
        item.CreatedBy = access!.UserId;
        await accessRuleService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, mapper.Map<AccessRuleDto>(item));
    }

    [HttpPost("ruleitem/{id:int}")]
    public async Task<IActionResult> AddRuleItems(int id, AccessRuleItemPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<AccessRuleItem>(model);
        item.AccessRuleId = id;
        item.CreatedBy = access!.UserId;

        foreach (var accessRuleItemTiming in item.AccessRuleItemTimings)
        {
            accessRuleItemTiming.CreatedBy = access!.UserId;
        }
        if (!model.AddToAllClubs)
        {
            foreach (var clubId in model.ClubIds)
            {
                AccessRuleItemAssignedClub assignedClub = new AccessRuleItemAssignedClub();
                assignedClub.CreatedBy = access!.UserId;
                assignedClub.ClubId = clubId;
                item.AccessRuleItemAssignedClubs.Add(assignedClub);
            }
        }

        await accessRuleItemService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, mapper.Map<AccessRuleItemDto>(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AccessRulePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<AccessRule>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await accessRuleService.UpdateAsync(item);
        return NoContent();
    }

    [HttpPut("ruleitem/{ruleItemId:int}")]
    public async Task<IActionResult> UpdateRuleItems(int ruleItemId, AccessRuleItemPostDto model)
    {
        var item = await accessRuleItemService.GetByIdAsync(ruleItemId);
        if (item == null)
        {
            return BadRequest("Access Rule Item Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.Description = model.Description;
        item.IsActiveForNewClubs = model.IsActiveForNewClubs;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        #region Timing
        foreach (var accessRuleItemTiming in item.AccessRuleItemTimings)
        {
            accessRuleItemTiming.IsActive = false;
            accessRuleItemTiming.ModifiedBy = access!.UserId;
            accessRuleItemTiming.ModifiedOn = DateTime.UtcNow;
            if (model.AccessRuleItemTimings.Any(x => x.DayOfWeek == (int)accessRuleItemTiming.DayOfWeek))
            {
                var timing = model.AccessRuleItemTimings.Where(x => x.DayOfWeek == (int)accessRuleItemTiming.DayOfWeek).FirstOrDefault()!;
                accessRuleItemTiming.StartTime = timing.StartTime;
                accessRuleItemTiming.StartTime = timing.EndTime;
                accessRuleItemTiming.IsActive = true;
                model.AccessRuleItemTimings.Remove(timing);
            }
        }
        foreach (var accessRuleItemTiming in model.AccessRuleItemTimings)
        {
            var timing = mapper.Map<AccessRuleItemTiming>(accessRuleItemTiming);
            timing.CreatedBy = access!.UserId;
            item.AccessRuleItemTimings.Add(timing);
        }
        #endregion Timing

        #region Assigned Clubs
        foreach (var accessRuleItemAssignedClub in item.AccessRuleItemAssignedClubs)
        {
            item.ModifiedBy = access!.UserId;
            item.ModifiedOn = DateTime.UtcNow;
            item.IsActive = !model.AddToAllClubs && model.ClubIds.Any(x => x == accessRuleItemAssignedClub.ClubId);
            if (item.IsActive)
            {
                model.ClubIds.Remove(accessRuleItemAssignedClub.ClubId);
            }
        }
        if (!model.AddToAllClubs)
        {
            foreach (var clubId in model.ClubIds)
            {
                AccessRuleItemAssignedClub assignedClub = new AccessRuleItemAssignedClub();
                assignedClub.CreatedBy = access!.UserId;
                assignedClub.ClubId = clubId;
                item.AccessRuleItemAssignedClubs.Add(assignedClub);
            }
        }
        #endregion Assigned Clubs

        await accessRuleItemService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await accessRuleService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Invalid Access Rule");
        }
        else if (item.IsDelete)
        {
            return BadRequest("Access Rule is already Deleted");
        }

        // Check For Subscriptions
        var subscriptionPlans = await subscriptionPlanService.GetAllByAccessRuleAsync(id);
        if (subscriptionPlans.Any())
        {
            return BadRequest("Access Rule Delete Failed," + subscriptionPlans.Count + " Subscription Plan(s) linked.");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        foreach (var accessRuleItem in item.AccessRuleItems)
        {
            accessRuleItem.DeletedBy = access!.UserId;
            accessRuleItem.DeletedOn = DateTime.UtcNow;
            accessRuleItem.IsDelete = true;
        }

        await accessRuleService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("ruleitem/{ruleItemId:int}")]
    public async Task<IActionResult> DeleteRuleItem(int ruleItemId)
    {
        var item = await accessRuleItemService.GetByIdAsync(ruleItemId);
        if (item == null)
        {
            return BadRequest("Access Rule Item Not Found.");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await accessRuleItemService.UpdateAsync(item);
        return NoContent();
    }
}
