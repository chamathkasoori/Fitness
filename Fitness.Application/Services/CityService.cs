using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IReadOnlyList<City>> GetAllAsync()
    {
        return await _cityRepository.GetAllAsync();
    }

    public async Task<IReadOnlyList<City>> GetAllByCountryIdAsync(int countryId)
    {
        return await _cityRepository.GetAllByCountryIdAsync(countryId);
    }

    Task<City?> IGenericService<City>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<City>.AddAsync(City entity)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<City>.UpdateAsync(City entity)
    {
        throw new NotImplementedException();
    }
}