using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class ApplicationsController : CommonController<ApplicationsController>
{
    private readonly IMapper _mapper;
    private readonly IApplicationService _applicationService;
    public ApplicationsController(IMapper mapper, IApplicationService applicationService)
    {
        _mapper = mapper;
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<ApplicationDto>> GetAllAsync()
    {
        var applications = await _applicationService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<ApplicationDto>>(applications);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ApplicationPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Fitness.Core.Entities.Application>(model);
        item.CreatedBy = access!.UserId;

        await _applicationService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ApplicationPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Fitness.Core.Entities.Application>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _applicationService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _applicationService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Application Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _applicationService.UpdateAsync(item);
        return NoContent();
    }
}
