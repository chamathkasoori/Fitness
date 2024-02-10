using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    public AddressService(IAddressRepository AddressRepository)
    {
        _addressRepository = AddressRepository;
    }

    public async Task<IReadOnlyList<Address>> GetAllAsync()
    {
        return await _addressRepository.GetAllAsync();
    }

    async Task<Address?> IGenericService<Address>.GetByIdAsync(int id)
    {
        return await _addressRepository.GetByIdAsync(id);
    }

    async Task IGenericService<Address>.AddAsync(Address entity)
    {
        await _addressRepository.AddAsync(entity);
    }

    async Task IGenericService<Address>.UpdateAsync(Address entity)
    {
        await _addressRepository.UpdateAsync(entity);
    }
}
