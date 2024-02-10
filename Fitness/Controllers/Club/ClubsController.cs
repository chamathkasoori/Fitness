using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Fitness.Controllers;
public class ClubsController : CommonController<ClubsController>
{
    private readonly IMapper mapper;
    private readonly IClubService clubService;
    private readonly IClubOpeningHourService clubOpeningHourService;
    private readonly ISubscriptionPlanService subscriptionPlanService;
    private readonly IAccessRuleItemService accessRuleItemService;
    private readonly IEmployeeService employeeService;
    public ClubsController(IMapper mapper, IClubService clubService, IClubOpeningHourService clubOpeningHourService, ISubscriptionPlanService subscriptionPlanService, IAccessRuleItemService accessRuleItemService, IEmployeeService employeeService)
    {
        this.mapper = mapper;
        this.clubService = clubService;
        this.clubOpeningHourService = clubOpeningHourService;
        this.subscriptionPlanService = subscriptionPlanService;
        this.accessRuleItemService = accessRuleItemService;
        this.employeeService = employeeService;
    }

    [AllowAnonymous]
    [HttpGet]
    [HttpGet("page/{page:int}/size/{size:int}")]
    public async Task<IReadOnlyList<ClubDto>> GetAllAsync(int page = 1, int size = 100, string searchText = "", string gender = "")
    {
        var items = await clubService.GetAllAsync(page, size, searchText, gender);
        var records = mapper.Map<IReadOnlyList<ClubDto>>(items);
        return records;
    }

    [HttpGet("details")]
    public async Task<List<ClubDetailsDto>> GetAllDetailsAsync()
    {
        var items = await clubService.GetAllDetailsAsync();
        return mapper.Map<List<ClubDetailsDto>>(items);
    }

    [HttpGet("{id:int}/openinghours")]
    public async Task<IReadOnlyList<ClubOpeningHourDto>> GetClubOpeningHours(int id)
    {
        var items = await clubOpeningHourService.GetAllByClubAsync(id);
        return mapper.Map<IReadOnlyList<ClubOpeningHourDto>>(items);
    }

    [HttpGet("{id:int}/openinghours/details")]
    public async Task<IReadOnlyList<ClubOpeningHourDetailDto>> GetClubOpeningHourDetails(int id)
    {
        var items = await clubOpeningHourService.GetAllByClubAsync(id);
        return mapper.Map<IReadOnlyList<ClubOpeningHourDetailDto>>(items);
    }

    [HttpGet("openinghours/today")]
    public async Task<List<ClubOpeningHourDetailDto>> GetTodaysClubOpeningHours()
    {
        var clubOpeningHours = await clubOpeningHourService.GetAllForTodayAsync();
        var items = mapper.Map<List<ClubOpeningHourDetailDto>>(clubOpeningHours);

        var clubs = await clubService.GetAllAsync();
        var closedClubs = clubs.Where(x => !clubOpeningHours.Any(y => y.ClubId == x.Id)).Select(x => x.Id);
        foreach (var clubId in closedClubs)
        {
            ClubOpeningHourDetailDto item = new ClubOpeningHourDetailDto();
            item.ClubId = clubId;
            item.DayOfWeek = DateTime.Now.DayOfWeek.ToString();
            item.IsClosed = true;
            items.Add(item);
        }

        return items;
    }

    [HttpPost]
    public async Task<IActionResult> Add(ClubPostDto model)
    {
        var extItem = await clubService.GetByNoAsync(model.ClubNumber);
        if (extItem != null)
        {
            return BadRequest("Club Number already exist");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<Club>(model);
        item.CompanyId = access!.CompanyId;
        item.CreatedBy = access!.UserId;

        var address = mapper.Map<Address>(model);
        address.CreatedBy = access!.UserId;
        item.Address = address;

        await clubService.AddAsync(item);

        //If SubscriptionPlan's AddToNewClub is True we need to add the mapping
        await subscriptionPlanService.AddNewClubAsync(item.Id, access!.UserId);

        //If AccessRuleItemTiming's IsActiveForNewClubs is True we need to add the mapping
        await accessRuleItemService.AddNewClubAsync(item.Id, access!.UserId);

        //If Employee's AssignToNewClubs or AvaialbleInNewClubs is True we need to add the mapping
        await employeeService.AddNewClubAsync(item.Id, access!.UserId);

        return CreatedAtAction("GetAll", new { id = item.Id }, mapper.Map<ClubDto>(item));
    }

    [HttpPost("{id:int}/OpeningHours")]
    public async Task<IActionResult> SaveClubOpeningHours(int id, List<ClubOpeningHourDto> items)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        List<ClubOpeningHour> clubOpeningHours = new List<ClubOpeningHour>();
        foreach (var item in items)
        {
            ClubOpeningHour openingHour = mapper.Map<ClubOpeningHour>(item);
            openingHour.ClubId = id;
            openingHour.CreatedBy = access!.UserId;
            clubOpeningHours.Add(openingHour);
        }

        await clubOpeningHourService.SaveAsync(clubOpeningHours);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ClubPostDto model)
    {
        var exsitingItem = await clubService.GetByIdAsync(id);
        if (exsitingItem == null)
        {
            return BadRequest("Club Not Found");
        }

        var extClub = await clubService.GetByNoAsync(model.ClubNumber);
        if (extClub != null && extClub.Id != id)
        {
            return BadRequest("Club Number already exist");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<Club>(model);
        item.Id = id;
        item.CompanyId = exsitingItem.CompanyId;
        item.AddressId = exsitingItem.AddressId;
        item.CreatedBy = exsitingItem.CreatedBy;
        item.CreatedOn = exsitingItem.CreatedOn;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        item.Address = mapper.Map<Address>(model);
        item.Address.Id = exsitingItem.AddressId;
        item.Address.CreatedBy = exsitingItem.CreatedBy;
        item.Address.CreatedOn = exsitingItem.CreatedOn;
        item.Address.ModifiedBy = access!.UserId;
        item.Address.ModifiedOn = DateTime.UtcNow;

        await clubService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await clubService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Club Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await clubService.UpdateAsync(item);
        return NoContent();
    }
}