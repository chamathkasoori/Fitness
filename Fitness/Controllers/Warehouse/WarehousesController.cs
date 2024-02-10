using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class WarehousesController : CommonController<WarehousesController>
{
    private readonly IMapper _mapper;
    private readonly IWarehouseService _warehouseService;
    public WarehousesController(IMapper mapper, IWarehouseService warehouseService)
    {
        _mapper = mapper;
        _warehouseService = warehouseService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<WarehouseDto>> GetAllAsync()
    {
        var warehouse = await _warehouseService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<WarehouseDto>>(warehouse);
    }

    [HttpPost]
    public async Task<IActionResult> Add(WarehousePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Warehouse>(model);
        item.CreatedBy = access!.UserId;

        await _warehouseService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, WarehousePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Warehouse>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _warehouseService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _warehouseService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Warehouse Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _warehouseService.UpdateAsync(item);
        return NoContent();
    }
}
