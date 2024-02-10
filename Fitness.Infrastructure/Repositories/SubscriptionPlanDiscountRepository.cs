using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class SubscriptionPlanDiscountRepository : GenericRepository<SubscriptionPlanDiscount>, ISubscriptionPlanDiscountRepository
{
    private readonly FitnessContext _context;
    public SubscriptionPlanDiscountRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<SubscriptionPlanDiscount>> GetAllBySubscriptionPlanAsync(int subscriptionPlanId)
    {
        DateTime now = DateTime.UtcNow;
        return await _context.SubscriptionPlanDiscounts
            .Include(x => x.SubscriptionPlanDiscountSubscriptionPlans)
            .Where(x =>
                !x.IsDelete
                && (x.ApplyToAllSubscriptionPlans || x.SubscriptionPlanDiscountSubscriptionPlans.Any(y => y.SubscriptionPlanId == subscriptionPlanId))
                && x.StartDate <= now && x.EndDate >= now
             )
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<SubscriptionPlanDiscount>> GetAllByClubAndSubscriptionPlansAsync(int clubId, List<int> subscriptionPlanIds)
    {
        DateTime now = DateTime.UtcNow.Date;
        return await _context.SubscriptionPlanDiscounts
            .Include(x => x.SubscriptionPlanDiscountClubs)
            .Include(x => x.SubscriptionPlanDiscountSubscriptionPlans)
            .Where(x =>
                !x.IsDelete
                && x.StartDate <= now
                && x.EndDate >= now
                && (x.ApplyToAllClubs || x.SubscriptionPlanDiscountClubs.Any(x => x.ClubId == clubId))
                && (x.ApplyToAllSubscriptionPlans || x.SubscriptionPlanDiscountSubscriptionPlans.Any(x => subscriptionPlanIds.Contains(x.SubscriptionPlanId)))
            )
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    async Task<SubscriptionPlanDiscount?> IGenericRepository<SubscriptionPlanDiscount>.GetByIdAsync(int id)
    {
        return await _context.SubscriptionPlanDiscounts
            .Include(x => x.SubscriptionPlanDiscountSubscriptionPlans)
            .Include(x => x.SubscriptionPlanDiscountClubs)
            .Include(x => x.SubscriptionPlanDiscountRoles)
            .Include(x => x.SubscriptionPlanDiscountApplications)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> IsNameExist(int id, string name)
    {
        return await _context.SubscriptionPlanDiscounts.AnyAsync(x => !x.IsDelete && x.Id != id && x.Name == name);
    }
}
