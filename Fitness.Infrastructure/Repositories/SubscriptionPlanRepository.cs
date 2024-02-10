using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class SubscriptionPlanRepository : GenericRepository<SubscriptionPlan>, ISubscriptionPlanRepository
{
    private readonly FitnessContext _context;
    public SubscriptionPlanRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<SubscriptionPlan>> GetAllByFilterAsync(int clubId, int ruleId)
    {
        return await _context.SubscriptionPlans
            .Include(x => x.PaymentInterval)
            .Include(x => x.CommitmentPeriod)
            .Include(x => x.AccessRule)
            .Where(x => !x.IsDelete && (clubId == 0 || x.AssignToAllClubs || x.SubscriptionPlanAssignedClubs.Any(y => y.ClubId == clubId)))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<SubscriptionPlan>> GetAllByClubAsync(int clubId, int applicationId)
    {
        return await _context.SubscriptionPlans
            .Include(x => x.SubscriptionPlanPaymentMethods).ThenInclude(y => y.PaymentMethod)
            .Include(x => x.AccessRule)
            .Include(x => x.SubscriptionPlanAssignedClubs)
            .Include(x => x.SubscriptionPlanSubscriptionPlanAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.SubscriptionPlanApplications)
            .Where(x => !x.IsDelete && (x.AssignToAllClubs || x.SubscriptionPlanAssignedClubs.Any(y => y.ClubId == clubId)) && x.SubscriptionPlanApplications.Any(y => y.ApplicationId == applicationId))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<SubscriptionPlan>> GetAllByAccessRuleAsync(int accessRuleId)
    {
        return await _context.SubscriptionPlans
            .Where(x => !x.IsDelete && x.AccessRuleId == accessRuleId && !x.IsDelete)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    async Task<SubscriptionPlan?> IGenericRepository<SubscriptionPlan>.GetByIdAsync(int id)
    {
        return await _context.SubscriptionPlans
            .Include(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlanPaymentMethods)
            .Include(x => x.SubscriptionPlanAssignedClubs)
            .Include(x => x.SubscriptionPlanAvailableClubs)
            .Include(x => x.SubscriptionPlanTags)
            .Include(x => x.SubscriptionPlanApplications)
            .Include(x => x.SubscriptionPlanRoles)
            .Include(x => x.SubscriptionPlanAssignedSettings)
            .Include(x => x.SubscriptionPlanSubscriptionPlanAddons)
            .Include(x => x.MembershipFeeVat)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<int>> GetAllAddToNewClubsAsync()
    {
        return await _context.SubscriptionPlans
            .Where(x => !x.IsDelete && x.AddToNewClubs)
            .Select(x => x.Id)
            .ToListAsync();
    }
}