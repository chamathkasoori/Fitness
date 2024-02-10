using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class ProductCategoriesController : CommonController<ProductCategoriesController>
{
    private readonly IMapper mapper;
    private readonly IProductCategoryService productCategoryService;
    public ProductCategoriesController(IMapper mapper, IProductCategoryService productCategoryService)
    {
        this.mapper = mapper;
        this.productCategoryService = productCategoryService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<ProductCategoryDto>> GetAllAsync()
    {
        var items = await productCategoryService.GetAllAsync();
        return mapper.Map<IReadOnlyList<ProductCategoryDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductCategoryPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<ProductCategory>(model);
        item.CreatedBy = access!.UserId;

        await productCategoryService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductCategoryPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<ProductCategory>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await productCategoryService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await productCategoryService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("ProductCategory Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await productCategoryService.UpdateAsync(item);
        return NoContent();
    }
}
