using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class CityRepository : GenericRepository<City>, ICityRepository
{
    private readonly FitnessContext _context;
    public CityRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<City>> GetAllByCountryIdAsync(int countryId)
    {
        return await _context.Cities.Where(x => !x.IsDelete && x.Country.Id == countryId).ToListAsync();
    }
}