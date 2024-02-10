using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Fitness.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers.Core;
public class RoleController : CommonController<RoleController>
{
    private readonly IMapper _mapper;
    private readonly IRoleService _roleService;
    private readonly IRoleModuleService _roleModuleService;
    private readonly IRoleModuleOperationService _roleModuleOperationService;
    public RoleController(IRoleService roleService, IMapper mapper, IRoleModuleService roleModuleService, IRoleModuleOperationService roleModuleOperationService)
    {
        _roleService = roleService;
        _mapper = mapper;
        _roleModuleService = roleModuleService;
        _roleModuleOperationService = roleModuleOperationService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<RoleDto>> GetAll()
    {
        var roles = await _roleService.GetAllAsync();
        var roleDtos = _mapper.Map<List<RoleDto>>(roles);
        foreach (var role in roleDtos)
        {
            role.RoleModuleNodes = TreeBuilder.BuildRoleModuleTree(role.RoleModules.ToList());
        }
        return roleDtos;
    }

    [HttpPost]
    public async Task<IActionResult> Add(RolePostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Role>(model);
        item.CreatedBy = access!.UserId;
        item.CompanyId = access!.CompanyId;
        await _roleService.AddAsync(item);

        List<RoleModule> roleModuleList = new List<RoleModule>();
        List<RoleModuleOperation> operationList = new List<RoleModuleOperation>();
        foreach (var moduleOperation in model.ModuleOperations)
        {
            var data = moduleOperation.Split("-");
            int moduleId = int.Parse(data[0]);
            int operationId = int.Parse(data[1]);
            if (!roleModuleList.Any(x => x.ModuleId == moduleId))
            {
                RoleModule roleModule = new RoleModule();
                roleModule.ModuleId = int.Parse(data[0]);
                roleModule.RoleId = item.Id;
                roleModule.CreatedBy = access!.UserId;
                await _roleModuleService.AddAsync(roleModule);
                roleModuleList.Add(roleModule);
            }

            var extRoleModule = roleModuleList.FirstOrDefault(x => x.ModuleId == moduleId);
            RoleModuleOperation operation = new RoleModuleOperation();
            operation.RoleModuleId = extRoleModule!.Id;
            operation.OperationId = operationId;
            operation.CreatedBy = access!.UserId;
            await _roleModuleOperationService.AddAsync(operation);
        }
        return CreatedAtAction("GetAll", new { id = item.Id }, _mapper.Map<RoleDto>(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RolePostDto model)
    {
        var item = await _roleService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Role Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.Name = model.Name;
        item.NameAR = model.NameAr;
        item.Description = model.Description;
        item.DescriptionAR = model.DescriptionAr;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        await _roleService.UpdateAsync(item);

        // Check For Newly Added
        foreach (var moduleOperation in model.ModuleOperations)
        {
            var data = moduleOperation.Split("-");
            int moduleId = int.Parse(data[0]);
            int operationId = int.Parse(data[1]);
            int roleModuleId = 0;
            bool isRoleModuleOperationFound = false;
            if (item.RoleModules.Any(x => x.ModuleId == moduleId))
            {
                // Already In The List
                RoleModule roleModule = item.RoleModules.FirstOrDefault(x => x.ModuleId == moduleId)!;
                roleModuleId = roleModule.Id;
                isRoleModuleOperationFound = roleModule.RoleModuleOperations.Any(x => x.OperationId == operationId);
            }
            else
            {
                // Newly Added Role Module
                RoleModule roleModule = new RoleModule();
                roleModule.ModuleId = moduleId;
                roleModule.RoleId = item.Id;
                roleModule.CreatedBy = access!.UserId;
                await _roleModuleService.AddAsync(roleModule);
                roleModuleId = roleModule.Id;
            }
            if (!isRoleModuleOperationFound)
            {
                RoleModuleOperation roleModuleOperation = new RoleModuleOperation();
                roleModuleOperation.RoleModuleId = roleModuleId;
                roleModuleOperation.OperationId = operationId;
                roleModuleOperation.CreatedBy = access!.UserId;
                await _roleModuleOperationService.AddAsync(roleModuleOperation);
            }
        }

        // Check For Removed
        foreach (var roleModule in item.RoleModules)
        {
            // Check For Removed Role Modules
            if (!model.ModuleOperations.Any(x => x.StartsWith(roleModule.ModuleId.ToString() + "-")))
            {
                roleModule.IsDelete = true;
                roleModule.DeletedOn = DateTime.UtcNow;
                roleModule.DeletedBy = access!.UserId;
                await _roleModuleService.UpdateAsync(roleModule);
            }

            foreach (var roleModuleOperation in roleModule.RoleModuleOperations)
            {
                // Check For Removed Role Module Operations
                if (!model.ModuleOperations.Any(x => x.StartsWith(roleModule.ModuleId.ToString() + "-" + roleModuleOperation.OperationId)))
                {
                    roleModuleOperation.IsDelete = true;
                    roleModuleOperation.DeletedOn = DateTime.UtcNow;
                    roleModuleOperation.DeletedBy = access!.UserId;
                    await _roleModuleOperationService.UpdateAsync(roleModuleOperation);
                }
            }
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _roleService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Role Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.IsDelete = true;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        foreach (var roleModule in item.RoleModules)
        {
            roleModule.IsDelete = true;
            roleModule.DeletedBy = access!.UserId;
            roleModule.DeletedOn = DateTime.UtcNow;
            foreach (var roleModuleOperation in roleModule.RoleModuleOperations)
            {
                roleModuleOperation.IsDelete = true;
                roleModuleOperation.DeletedBy = access!.UserId;
                roleModuleOperation.DeletedOn = DateTime.UtcNow;
            }
        }

        await _roleService.UpdateAsync(item);
        return Ok();
    }
}