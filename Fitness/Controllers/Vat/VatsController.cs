using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class VatsController : CommonController<Vat>
{
    private readonly IVatService _vatService;
    private readonly IMapper _mapper;
    public VatsController(IVatService vatService, IMapper mapper)
    {
        _vatService = vatService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IReadOnlyList<VatDto>> GetAllAsync()
    {
        var items = await _vatService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<VatDto>>(items.Where(x=> x.IsActive).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Add(VatPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Vat>(model);
        item.CreatedBy = access!.UserId;

        await _vatService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, VatPostDto model)
    {
        var extItem = await _vatService.GetByIdAsync(id);
        if (extItem == null)
        {
            return BadRequest("Vat Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Vat>(model);
        item.Id = id;
        item.CreatedBy = extItem.CreatedBy;
        item.CreatedOn = extItem.CreatedOn;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        await _vatService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _vatService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Vat Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;
        await _vatService.UpdateAsync(item);
        return NoContent();
    }
}
