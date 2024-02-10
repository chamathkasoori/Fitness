using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class SubscriptionPlanAddonsController : CommonController<SubscriptionPlanAddonsController>
{
    private readonly IMapper mapper;
    private readonly ISubscriptionPlanAddonService subscriptionPlanAddonService;
    public SubscriptionPlanAddonsController(IMapper mapper, ISubscriptionPlanAddonService subscriptionPlanAddonService)
    {
        this.mapper = mapper;
        this.subscriptionPlanAddonService = subscriptionPlanAddonService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await subscriptionPlanAddonService.GetAllAsync();
        var result = mapper.Map<IReadOnlyList<SubscriptionPlanAddonDto>>(items);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await subscriptionPlanAddonService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("SubscriptionPlanAddon Not Found");
        }

        item.SubscriptionPlanAddonClubs = item.SubscriptionPlanAddonClubs.Where(x => x.IsActive).ToList();
        var result = mapper.Map<SubscriptionPlanAddonDto>(item);
        return Ok(result);
    }

    [HttpGet("Type/{type}")]
    public async Task<IActionResult> GetByType(string type)
    {
        var items = await subscriptionPlanAddonService.GetByTypeAsync(type);
        var result = mapper.Map<IReadOnlyList<SubscriptionPlanAddonDto>>(items);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(SubscriptionPlanAddonPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<SubscriptionPlanAddon>(model);
        item.CreatedBy = access!.UserId;
        if (!model.AddToAllClubs)
        {
            foreach (var clubId in model.ClubIds)
            {
                SubscriptionPlanAddonClub club = new SubscriptionPlanAddonClub();
                club.ClubId = clubId;
                club.CreatedBy = access!.UserId;
                item.SubscriptionPlanAddonClubs.Add(club);
            }
        }

        await subscriptionPlanAddonService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, mapper.Map<SubscriptionPlanAddonDto>(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SubscriptionPlanAddonPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = await subscriptionPlanAddonService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("SubscriptionPlanAddon Not Found");
        }

        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        item.Name = model.Name;
        item.Type = model.Type;
        item.Quantity = model.Quantity;
        item.AddToAllClubs = model.AddToAllClubs;
        if (model.AddToAllClubs)
        {
            foreach (var subscriptionPlanAddonClub in item.SubscriptionPlanAddonClubs)
            {
                subscriptionPlanAddonClub.IsActive = false;
                subscriptionPlanAddonClub.ModifiedBy = access!.UserId;
                subscriptionPlanAddonClub.ModifiedOn = DateTime.UtcNow;
            }
        }
        else
        {
            foreach (var subscriptionPlanAddonClub in item.SubscriptionPlanAddonClubs)
            {
                subscriptionPlanAddonClub.ModifiedBy = access!.UserId;
                subscriptionPlanAddonClub.ModifiedOn = DateTime.UtcNow;
                subscriptionPlanAddonClub.IsActive = model.ClubIds.Any(x => x == subscriptionPlanAddonClub.ClubId);
                if (subscriptionPlanAddonClub.IsActive)
                {
                    model.ClubIds.Remove(subscriptionPlanAddonClub.ClubId);
                }
            }
            foreach (var clubId in model.ClubIds)
            {
                SubscriptionPlanAddonClub subscriptionPlanAddonClub = new SubscriptionPlanAddonClub();
                subscriptionPlanAddonClub.CreatedBy = access!.UserId;
                subscriptionPlanAddonClub.ClubId = clubId;
                item.SubscriptionPlanAddonClubs.Add(subscriptionPlanAddonClub);
            }
        }

        await subscriptionPlanAddonService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await subscriptionPlanAddonService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Subscription Plan Addon Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;
        await subscriptionPlanAddonService.UpdateAsync(item);
        return NoContent();
    }
}
