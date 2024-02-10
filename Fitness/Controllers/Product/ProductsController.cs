using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class ProductsController : CommonController<ProductsController>
{
    private readonly IMapper mapper;
    private readonly IProductService productService;
    private readonly IProductPriceAuditService productPriceAuditService;
    public ProductsController(IMapper mapper, IProductService productService, IProductPriceAuditService productPriceAuditService)
    {
        this.mapper = mapper;
        this.productService = productService;
        this.productPriceAuditService = productPriceAuditService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<ProductDto>> GetAll()
    {
        var items = await productService.GetAllAsync();
        return mapper.Map<IReadOnlyList<ProductDto>>(items);
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search(ProductSearchDto request)
    {
        var items = await productService.Search(request.Name, request.ProductCategoryId, request.ClubId, request.ApplicationId, request.ShowDeletedProducts);
        return Ok(mapper.Map<IReadOnlyList<ProductDto>>(items));
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<Product>(model);
        item.CreatedBy = access!.UserId;
        foreach (var productImage in item.ProductImages)
        {
            productImage.CreatedBy = access!.UserId;
        }
        foreach (var productAvailableClub in item.ProductAvailableClubs)
        {
            productAvailableClub.CreatedBy = access!.UserId;
        }
        foreach (var productAvailableApplication in item.ProductAvailableApplications)
        {
            productAvailableApplication.CreatedBy = access!.UserId;
        }
        await productService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductPostDto model)
    {
        var existingItem = await productService.GetByIdAsync(id);
        if (existingItem == null)
        {
            return BadRequest("Product Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<Product>(model);
        item.Id = id;
        item.CreatedBy = existingItem.CreatedBy;
        item.CreatedOn = existingItem.CreatedOn;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        foreach (var productImage in item.ProductImages)
        {
            if (productImage.Id == 0)
            {
                productImage.CreatedBy = access!.UserId;
            }
            else
            {
                productImage.ModifiedBy = access!.UserId;
            }
        }
        foreach (var productAvailableClub in item.ProductAvailableClubs)
        {
            if (productAvailableClub.Id == 0)
            {
                productAvailableClub.CreatedBy = access!.UserId;
            }
            else
            {
                productAvailableClub.ModifiedBy = access!.UserId;
            }
        }
        foreach (var productAvailableApplication in item.ProductAvailableApplications)
        {
            if (productAvailableApplication.Id == 0)
            {
                productAvailableApplication.CreatedBy = access!.UserId;
            }
            else
            {
                productAvailableApplication.ModifiedBy = access!.UserId;
            }
        }
        await productService.UpdateAsync(item);

        // Check For Price Change
        if (item.GrossPrice != existingItem.GrossPrice || item.NetPrice != existingItem.NetPrice)
        {
            ProductPriceAudit productPriceAudit = new ProductPriceAudit();
            productPriceAudit.ProductId = item.Id;
            productPriceAudit.GrossPriceOld = existingItem.GrossPrice;
            productPriceAudit.GrossPriceNew = item.GrossPrice;
            productPriceAudit.NetPriceOld = existingItem.NetPrice;
            productPriceAudit.NetPriceNew = item.NetPrice;
            productPriceAudit.CreatedBy = access!.UserId;
            await productPriceAuditService.AddAsync(productPriceAudit);
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await productService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Product Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.IsDelete = true;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        await productService.UpdateAsync(item);
        return Ok();
    }
}
