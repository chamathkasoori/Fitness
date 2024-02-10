using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class SuppliersController : CommonController<SuppliersController>
{
    private readonly IMapper _mapper;
    private readonly ISupplierService _supplierService;
    public SuppliersController(IMapper mapper, ISupplierService supplierService)
    {
        _mapper = mapper;
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<SupplierDto>> GetAll()
    {
        var items = await _supplierService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<SupplierDto>>(items);
    }

    [HttpGet("{id:int}")]
    public SupplierDetailDto GetById(int id)
    {
        var supplier = _supplierService.GetByIdAsync(id);
        return _mapper.Map<SupplierDetailDto>(supplier);
    }

    [HttpPost]
    public IActionResult Add(SupplierPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Supplier>(model);
        item.CreatedBy = access!.UserId;

        _supplierService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, _mapper.Map<SupplierDetailDto>(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SupplierPostDto model)
    {
        var extItem = await _supplierService.GetByIdAsync(id);
        if (extItem == null)
        {
            return BadRequest("Supplier Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Supplier>(model);
        item.Id = id;
        item.CreatedBy = extItem.CreatedBy;
        item.CreatedOn = extItem.CreatedOn;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        await _supplierService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _supplierService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Supplier Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;
        await _supplierService.UpdateAsync(item);
        return NoContent();
    }
}