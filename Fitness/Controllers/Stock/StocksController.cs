using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class StocksController : CommonController<StocksController>
{
    private readonly IMapper _mapper;
    private readonly IStockService _stockService;
    public StocksController(IMapper mapper, IStockService stockService)
    {
        _mapper = mapper;
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<StockDetailDto>> GetAllAsync()
    {
        var stock = await _stockService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<StockDetailDto>>(stock);
    }

    [HttpGet("stats")]
    public async Task<IReadOnlyList<StockDetailDto>> GetProductStats(int warehouseId = 0, int categoryId = 0)
    {
        var items = await _stockService.GetProductStats(warehouseId, categoryId);
        return _mapper.Map<IReadOnlyList<StockDetailDto>>(items);
    }

    [HttpPost("search")]
    public async Task<IReadOnlyList<StockDto>> Search(StockSearchDto request)
    {
        var items = await _stockService.SearchAsync(request.ClubId, request.ProductCategoryId, request.ShowDeletedProducts);
        return _mapper.Map<IReadOnlyList<StockDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(StockPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Stock>(model);
        item.CreatedBy = access!.UserId;

        await _stockService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, _mapper.Map<StockDto>(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, StockPostDto model)
    {
        var item = await _stockService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Stock Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        item.Quantity = model.Quantity;

        await _stockService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _stockService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Stock Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _stockService.UpdateAsync(item);
        return NoContent();
    }
}
