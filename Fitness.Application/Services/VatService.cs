using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class VatService : IVatService
{
    private readonly IVatRepository _vatRepository;
    public VatService(IVatRepository vatRepository)
    {
        _vatRepository = vatRepository;
    }

    public async Task<IReadOnlyList<Vat>> GetAllAsync()
    {
        return await _vatRepository.GetAllAsync();
    }

    Task<Vat?> IGenericService<Vat>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<Vat>.AddAsync(Vat entity)
    {
        await _vatRepository.AddAsync(entity);
    }

    async Task IGenericService<Vat>.UpdateAsync(Vat entity)
    {
        await _vatRepository.UpdateAsync(entity);
    }
}
