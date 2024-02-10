using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class CompanyController : CommonController<CompanyController>
{
    private readonly IMapper _mapper;
    private readonly ICompanyService companyService;
    public CompanyController(ICompanyService companyService, IMapper mapper)
    {
        _mapper = mapper;
        this.companyService = companyService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyList<CompanyDto>> GetAllAsync()
    {
        var items = await companyService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<CompanyDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CompanyPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Company>(model);
        item.CreatedBy = access!.UserId;

        await companyService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CompanyPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Company>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await companyService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "Resend")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await companyService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Company Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await companyService.UpdateAsync(item);
        return NoContent();
    }
}