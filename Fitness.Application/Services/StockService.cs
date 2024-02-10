using Fitness.Core.Common;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    public StockService(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<IReadOnlyList<Stock>> GetAllAsync()
    {
        return await _stockRepository.GetAllAsync();
    }

    public async Task<List<Stock>> SearchAsync(int clubId, int categoryId, bool showDeleted)
    {
        return await _stockRepository.SearchAsync(clubId, categoryId, showDeleted);
    }

    public async Task<List<StockDetail>> GetProductStats(int? warehouseId, int? categoryId)
    {
        return await _stockRepository.GetProductStats(warehouseId, categoryId);
    }
   
    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _stockRepository.GetByIdAsync(id);
    }

    async Task IGenericService<Stock>.AddAsync(Stock entity)
    {
        await _stockRepository.AddAsync(entity);
    }

    async Task IGenericService<Stock>.UpdateAsync(Stock entity)
    {
        await _stockRepository.UpdateAsync(entity);
    }
}
