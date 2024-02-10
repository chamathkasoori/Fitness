using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class RoleModuleOperationRepository : GenericRepository<RoleModuleOperation>, IRoleModuleOperationRepository
{
    private readonly FitnessContext _context;
    public RoleModuleOperationRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<RoleModuleOperation> entities)
    {
        await _context.RoleModuleOperations.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}