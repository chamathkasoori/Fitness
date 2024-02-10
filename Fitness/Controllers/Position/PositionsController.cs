using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class PositionsController : CommonController<PositionsController>
{
    private readonly IMapper _mapper;
    private readonly IPositionService _positionService;
    public PositionsController(IPositionService positionService, IMapper mapper)
    {
        _positionService = positionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IReadOnlyList<PositionDto>> GetAllAsync()
    {
        var items = await _positionService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<PositionDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PositionPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Position>(model);
        item.CreatedBy = access!.UserId;

        await _positionService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, PositionPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Position>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _positionService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _positionService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Position Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _positionService.UpdateAsync(item);
        return NoContent();
    }
}