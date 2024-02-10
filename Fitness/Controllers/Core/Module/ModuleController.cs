using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Fitness.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class ModuleController : CommonController<ModuleController>
{
    private readonly IMapper _mapper;
    private readonly IModuleService _moduleService;
    public ModuleController(IMapper mapper, IModuleService moduleService)
    {
        _mapper = mapper;
        _moduleService = moduleService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var modules = await _moduleService.GetAllAsync();
        return Ok(_mapper.Map<List<ModuleDetailsDto>>(modules));
    }

    [HttpGet("Tree")]
    public async Task<IReadOnlyList<TreeNodeDto>> GetModuleTree()
    {
        var modules = await _moduleService.GetAllAsync();
        return TreeBuilder.ModuleTree(modules.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> Add(ModulePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var module = _mapper.Map<Fitness.Core.Entities.Module>(model);
        module.CreatedBy = access!.UserId;
        if (module.ParentModuleId > 0)
        {
            var parentModule = await _moduleService.GetByIdAsync(module.ParentModuleId.Value);
            module.Hierarchy = parentModule!.Hierarchy;
        }
        else
        {
            module.ParentModuleId = null;
            module.Hierarchy = "";
        }

        foreach (var moduleOperations in module.ModuleOperations)
        {
            moduleOperations.CreatedBy = access!.UserId;
        }

        await _moduleService.AddAsync(module);
        return CreatedAtAction("GetAll", new { id = module.Id }, module);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ModulePostDto model)
    {
        var module = await _moduleService.GetByIdAsync(id);
        if (module == null)
        {
            return BadRequest("Module Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Fitness.Core.Entities.Module>(model);
        item.Id = id;
        item.CreatedBy = module.CreatedBy;
        item.CreatedOn = module.CreatedOn;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        if (item.ParentModuleId > 0)
        {
            var parentModule = await _moduleService.GetByIdAsync(item.ParentModuleId.Value);
            item.Hierarchy = parentModule!.Hierarchy + "-" + item.Id.ToString();
        }
        else
        {
            item.ParentModuleId = null;
            item.Hierarchy = item.Id.ToString();
        }

        foreach (var moduleOperation in item.ModuleOperations.Where(x => !x.IsDelete))
        {
            if (model.ModuleOperations.Any(x => x.OperationId == moduleOperation.OperationId))
            {
                // Already In The List
                model.ModuleOperations.Remove(model.ModuleOperations.First(x => x.OperationId == moduleOperation.OperationId));
            }
            else
            {
                // Removed From The List
                moduleOperation.IsDelete = true;
                moduleOperation.DeletedBy = access!.UserId;
                moduleOperation.DeletedOn = DateTime.UtcNow;
            }
        }
        foreach (var moduleOperationItem in model.ModuleOperations)
        {
            Fitness.Core.Entities.ModuleOperation moduleOperation = new Fitness.Core.Entities.ModuleOperation();
            moduleOperation.ModuleId = item.Id;
            moduleOperation.OperationId = moduleOperationItem.OperationId;
            moduleOperation.CreatedBy = access!.UserId;
            moduleOperation.CreatedOn = DateTime.UtcNow;
            item.ModuleOperations.Add(moduleOperation);
        }

        await _moduleService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _moduleService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Module Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _moduleService.UpdateAsync(item);
        return NoContent();
    }
}
