using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class SupplierService: ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<IReadOnlyList<Supplier>> GetAllAsync()
    {
        return await _supplierRepository.GetAllAsync();
    }

    public Task<Supplier?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Supplier entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Supplier entity)
    {
        throw new NotImplementedException();
    }
}