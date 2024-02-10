using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class RoleModuleController : CommonController<RoleModuleController>
{
    private readonly IMapper _mapper;
    private readonly IRoleModuleService _roleModuleService;
    public RoleModuleController(IRoleModuleService roleModuleService, IMapper mapper)
    {
        _mapper = mapper;
        _roleModuleService = roleModuleService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<RoleModuleDto>> GetAllAsync()
    {
        var modules = await _roleModuleService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<RoleModuleDto>>(modules);
    }

    [HttpPost]
    public async Task<IActionResult> Add(RoleModulePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<RoleModule>(model);
        item.CreatedBy = access!.UserId;

        await _roleModuleService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RoleModulePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<RoleModule>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _roleModuleService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _roleModuleService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Role Module Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _roleModuleService.UpdateAsync(item);
        return NoContent();
    }
}
