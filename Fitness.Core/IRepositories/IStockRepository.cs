using Fitness.Core.Common;
using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IStockRepository : IGenericRepository<Stock>
{
    public Task<List<Stock>> SearchAsync(int clubId, int categoryId, bool showDeleted);

    public Task<List<StockDetail>> GetProductStats(int? warehouseId, int? categoryId);
}
