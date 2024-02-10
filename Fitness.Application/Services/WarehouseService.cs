using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<IReadOnlyList<Warehouse>> GetAllAsync()
    {
        return await _warehouseRepository.GetAllAsync();
    }

    public async Task<Warehouse?> GetByIdAsync(int id)
    {
        return await _warehouseRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Warehouse entity)
    {
        await _warehouseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Warehouse entity)
    {
        await _warehouseRepository.UpdateAsync(entity);
    }
}
