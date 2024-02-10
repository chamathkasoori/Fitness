using Fitness.Core.Common;
using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IStockService: IGenericService<Stock>
{
    public Task<List<Stock>> SearchAsync(int clubId, int categoryId, bool showDeleted);

    public Task<List<StockDetail>> GetProductStats(int? warehouseId, int? categoryId);
}
