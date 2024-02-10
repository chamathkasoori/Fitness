using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(FitnessContext context) : base(context)
    {
    }
}