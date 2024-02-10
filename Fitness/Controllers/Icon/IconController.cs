using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class IconController : CommonController<IconController>
{
    private readonly IMapper _mapper;
    private readonly IIconService _iconService;
    public IconController(IIconService iconService, IMapper mapper)
    {
        _iconService = iconService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IReadOnlyList<IconDto>> GetAllAsync()
    {
        var icon = await _iconService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<IconDto>>(icon);
    }

    [HttpPost]
    public async Task<IActionResult> Add(IconPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Icon>(model);
        item.CompanyId = access!.CompanyId;
        item.CreatedBy = access!.UserId;

        await _iconService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, IconPostDto model)
    {
        var item = await _iconService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Icon Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.Name = model.Name;
        item.IconValue = model.IconValue;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _iconService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _iconService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Icon Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _iconService.UpdateAsync(item);
        return NoContent();
    }
}
