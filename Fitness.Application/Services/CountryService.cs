using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    public CountryService(ICountryRepository cityRepository)
    {
        _countryRepository = cityRepository;
    }

    public async Task<IReadOnlyList<Country>> GetAllAsync()
    {
        return await _countryRepository.GetAllAsync();
    }

    Task<Country?> IGenericService<Country>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<Country>.AddAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<Country>.UpdateAsync(Country entity)
    {
        throw new NotImplementedException();
    }
}