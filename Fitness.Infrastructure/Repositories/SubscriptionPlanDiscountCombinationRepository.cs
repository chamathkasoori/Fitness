using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class SubscriptionPlanDiscountCombinationRepository : GenericRepository<SubscriptionPlanDiscountCombination>, ISubscriptionPlanDiscountCombinationRepository
{
    private readonly FitnessContext _context;
    public SubscriptionPlanDiscountCombinationRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<SubscriptionPlanDiscountCombination>> GetAllCombinedAsync(int subscriptionPlanDiscountId)
    {
        return await _context.SubscriptionPlanDiscountCombinations
            .Include(x => x.CombinedSubscriptionPlanDiscount)
            .Where(x => !x.IsDelete && x.SubscriptionPlanDiscountId == subscriptionPlanDiscountId)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}
