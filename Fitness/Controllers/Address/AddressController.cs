using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class AddressController : CommonController<AddressController>
{
    private readonly IMapper _mapper;
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService, IMapper mapper)
    {
        _mapper = mapper;
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<AddressDto>> GetAllAsync()
    {
        var addresss = await _addressService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<AddressDto>>(addresss);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddressDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Address>(model);
        item.CreatedBy = access!.UserId;
        await _addressService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AddressDto addressDto)
    {
        var existing = await _addressService.GetByIdAsync(id);
        if (existing == null)
        {
            return BadRequest("Address Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Address>(addressDto);
        item.Id = id;
        item.CreatedBy = existing.CreatedBy;
        item.CreatedOn = existing.CreatedOn;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        await _addressService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _addressService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Address Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _addressService.UpdateAsync(item);
        return NoContent();
    }
}
