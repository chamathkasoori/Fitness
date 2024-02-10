using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class OperationsController : CommonController<OperationsController>
{
    private readonly IMapper _mapper;
    private readonly IOperationService _operationService;
    public OperationsController(IMapper mapper, IOperationService operationService)
    {
        _mapper = mapper;
        _operationService = operationService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<OperationDto>> GetAllAsync()
    {
        var operations = await _operationService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<OperationDto>>(operations);
    }

    [HttpPost]
    public async Task<IActionResult> Add(OperationPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Operation>(model);
        item.CreatedBy = access!.UserId;

        await _operationService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, OperationPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Operation>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _operationService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _operationService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Operation Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _operationService.UpdateAsync(item);
        return NoContent();
    }
}