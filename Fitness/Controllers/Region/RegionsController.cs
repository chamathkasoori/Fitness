using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class RegionsController : CommonController<RegionsController>
{
    private readonly IMapper _mapper;
    private readonly IRegionService _regionService;
    public RegionsController(IRegionService regionService, IMapper mapper)
    {
        _mapper = mapper;
        _regionService = regionService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<RegionDto>> GetAllAsync()
    {
        var items = await _regionService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<RegionDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(RegionPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Region>(model);
        item.CreatedBy = access!.UserId;

        await _regionService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RegionPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Region>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _regionService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _regionService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Region Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _regionService.UpdateAsync(item);
        return NoContent();
    }
}