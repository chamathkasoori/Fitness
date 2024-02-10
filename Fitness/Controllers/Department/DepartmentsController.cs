using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class DepartmentsController : CommonController<DepartmentsController>
{
    private readonly IMapper _mapper;
    private readonly IDepartmentService _departmentService;
    public DepartmentsController(IMapper mapper, IDepartmentService DepartmentService)
    {
        _mapper = mapper;
        _departmentService = DepartmentService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<DepartmentDto>> GetAllAsync()
    {
        var Departments = await _departmentService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<DepartmentDto>>(Departments);
    }

    [HttpPost]
    public async Task<IActionResult> Add(DepartmentPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Department>(model);
        item.CreatedBy = access!.UserId;

        await _departmentService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, DepartmentPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Department>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _departmentService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _departmentService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Department Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _departmentService.UpdateAsync(item);
        return NoContent();
    }
}