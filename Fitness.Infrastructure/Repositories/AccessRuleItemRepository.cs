using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class AccessRuleItemRepository : GenericRepository<AccessRuleItem>, IAccessRuleItemRepository
{
    private readonly FitnessContext _context;
    public AccessRuleItemRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<AccessRuleItem?> IGenericRepository<AccessRuleItem>.GetByIdAsync(int id)
    {
        return await _context.AccessRuleItems
            .Include(x => x.AccessRuleItemTimings)
            .Include(x => x.AccessRuleItemAssignedClubs)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<AccessRuleItem>> GetAllIsActiveForNewClubsAsync()
    {
        return await _context.AccessRuleItems
            .Where(x => !x.IsDelete && x.IsActiveForNewClubs && !x.IsDelete)
            .ToListAsync();
    }

    public async Task SaveAssignedClubsAsync(IEnumerable<AccessRuleItemAssignedClub> items)
    {
        await _context.AccessRuleItemAssignedClubs.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }
}
