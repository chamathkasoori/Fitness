using Fitness.Core.Common;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class StockRepository : GenericRepository<Stock>, IStockRepository
{
    private readonly FitnessContext _context;
    public StockRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Stock>> SearchAsync(int clubId, int categoryId, bool showDeleted)
    {
        return await _context.Stocks
            .Include(x => x.Product)
            .Include(x => x.Warehouse)
            .Where(x => !x.IsDelete 
                && (clubId == 0 || x.Warehouse.ClubId == clubId)
                && (categoryId == 0 || x.Product.CategoryId == categoryId)
                && (x.IsDelete == false || x.IsDelete == showDeleted))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<StockDetail>> GetProductStats(int? warehouseId, int? categoryId)
    {
        var query = _context.Products
                    .GroupJoin
                    (
                        _context.Stocks,
                        product => product.Id,
                        stock => stock.ProductId,
                        (product, stocks) => new
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            Price = product.GrossPrice,
                            Threshod = product.Threshold,
                            UOM = product.UOM,
                            CategoryId = product.CategoryId,
                            Stocks = stocks.ToList()
                        }
                    )
                    .SelectMany
                    (
                        result => result.Stocks.DefaultIfEmpty(),
                        (product, stock) => new StockDetail
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Quantity = stock!.Quantity,
                            Threshold = product.Threshod,
                            UOM = product.UOM,
                            Price = product.Price,
                            WarehouseId = stock.WarehouseId,
                            CategoryId = product.CategoryId
                        }
                    );
        if (warehouseId > 0)
        {
            query = query.Where(stock => stock.WarehouseId == warehouseId.Value);
        }
        if (categoryId > 0)
        {
            query = query.Where(stock => stock.CategoryId == categoryId.Value);
        }
        return await query.ToListAsync();
    }
}
