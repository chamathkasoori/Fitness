using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface ICityRepository : IGenericRepository<City>
{
    Task<IReadOnlyList<City>> GetAllByCountryIdAsync(int countryId);
}