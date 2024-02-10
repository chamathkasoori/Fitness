using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface ICityService : IGenericService<City>
{
    Task<IReadOnlyList<City>> GetAllByCountryIdAsync(int countryId);
}