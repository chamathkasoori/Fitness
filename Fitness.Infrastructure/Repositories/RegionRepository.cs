using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class RegionRepository : GenericRepository<Region>, IRegionRepository
{
    private readonly FitnessContext _context;
    public RegionRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<IReadOnlyList<Region>> IGenericRepository<Region>.GetAllAsync()
    {
        return await _context.Regions.Include(x => x.Country).ToListAsync();
    }
}