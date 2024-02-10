using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class SubscriptionPlanAddonRepository : GenericRepository<SubscriptionPlanAddon>, ISubscriptionPlanAddonRepository
{
    private readonly FitnessContext _context;
    public SubscriptionPlanAddonRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<SubscriptionPlanAddon>> GetByTypeAsync(string type)
    {
        return await _context.SubscriptionPlanAddons
            .Include(x => x.SubscriptionPlanAddonClubs)
            .Where(x => x.Type == type)
            .ToListAsync();
    }

    public async Task<List<SubscriptionPlanAddon>> GetBySubscriptionPlanAsync(int subscriptionPlanId)
    {
        return await _context.SubscriptionPlanAddons
            .Include(x => x.SubscriptionPlanSubscriptionPlanAddons)
            .Where(x => !x.IsDelete && (x.IsAvailableForAllPlans || x.SubscriptionPlanSubscriptionPlanAddons.Any(y => y.SubscriptionPlanId == subscriptionPlanId)))
            .ToListAsync();
    }

    async Task<SubscriptionPlanAddon?> IGenericRepository<SubscriptionPlanAddon>.GetByIdAsync(int id)
    {
        return await _context.SubscriptionPlanAddons
            .Include(x => x.SubscriptionPlanAddonClubs)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}