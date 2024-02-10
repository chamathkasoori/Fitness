using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class AccessRuleRepository : GenericRepository<AccessRule>, IAccessRuleRepository
{
    private readonly FitnessContext _context;
    public AccessRuleRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<AccessRule?> IGenericRepository<AccessRule>.GetByIdAsync(int id)
    {
        return await _context.AccessRules
            .Include(x => x.AccessRuleItems.Where(z => !z.IsDelete)).ThenInclude(x => x.AccessRuleItemTimings.Where(z=> z.IsActive))
            .Include(x => x.AccessRuleItems.Where(z => !z.IsDelete)).ThenInclude(x => x.AccessRuleItemAssignedClubs.Where(z => z.IsActive))
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
