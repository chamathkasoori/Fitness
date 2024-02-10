using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberSubscriptionRepository : GenericRepository<MemberSubscription>, IMemberSubscriptionRepository
{
    private readonly FitnessContext _context;
    public MemberSubscriptionRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<MemberSubscription>> GetActiveMemberSubscriptionsAsync(int memberId)
    {
        return await _context.MemberSubscriptions
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.AccessRule)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.CommitmentPeriod)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.SubscriptionPlanSubscriptionPlanAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionFreezes)
            .Where(x => !x.IsDelete && x.MemberId == memberId && new List<string> { MemberSubscriptionStatus.Current, MemberSubscriptionStatus.NotStarted, MemberSubscriptionStatus.Freezed }.Contains(x.Status))
            .OrderBy(x => x.StartDate)
            .ToListAsync();
    }

    public async Task<List<MemberSubscription>> GetArchivedMemberSubscriptionsAsync(int memberId)
    {
        return await _context.MemberSubscriptions
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.AccessRule)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.CommitmentPeriod)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.SubscriptionPlanSubscriptionPlanAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionFreezes)
            .Where(x => !x.IsDelete && x.MemberId == memberId && new List<string> { MemberSubscriptionStatus.Cancelled, MemberSubscriptionStatus.Ended }.Contains(x.Status))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<MemberSubscription>> GetTransfferedMemberSubscriptionsAsync(int memberId)
    {
        return await _context.MemberSubscriptions
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.AccessRule)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.CommitmentPeriod)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.SubscriptionPlanSubscriptionPlanAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionFreezes)
            .Where(x => !x.IsDelete && x.MemberId == memberId && x.Status == MemberSubscriptionStatus.Transferred)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<MemberSubscription>> GetAllFreezedExpiredAsync()
    {
        DateTime utcToday = DateTime.UtcNow.Date;
        return await _context.MemberSubscriptions
            .Include(x => x.MemberSubscriptionFreezes)
            .Where(x => !x.IsDelete && x.Status == MemberSubscriptionStatus.Freezed && !x.MemberSubscriptionFreezes.Any(y => y.StartDate <= utcToday.Date && y.EndDate >= utcToday.Date))
            .ToListAsync();
    }

    public async Task<List<MemberSubscription>> GetAllCurrentExpiredAsync()
    {
        DateTime utcToday = DateTime.UtcNow.Date;
        return await _context.MemberSubscriptions
            .Where(x => !x.IsDelete && x.Status == MemberSubscriptionStatus.Current && x.EndDate < utcToday)
            .ToListAsync();
    }

    public async Task<List<MemberSubscription>> GetAllNotStartedActiveAsync()
    {
        DateTime utcToday = DateTime.UtcNow.Date;
        return await _context.MemberSubscriptions
            .Where(x => !x.IsDelete && x.Status == MemberSubscriptionStatus.NotStarted && x.StartDate <= utcToday && x.EndDate >= utcToday)
            .ToListAsync();
    }

    public async Task<List<MemberSubscription>> GetAllForFreezeAsync()
    {
        // This will return all the active which need to be freezed
        DateTime utcToday = DateTime.UtcNow.Date;
        return await _context.MemberSubscriptions
            .Include(x => x.MemberSubscriptionFreezes)
            .Where(x => !x.IsDelete && x.Status == MemberSubscriptionStatus.Current && x.MemberSubscriptionFreezes.Any(x => x.StartDate.Date == utcToday))
            .ToListAsync();
    }

    public async Task<MemberSubscription> GetCurrentByMemberAsync(int memberId)
    {
        DateTime utcToday = DateTime.UtcNow.Date;
        var items = await _context.MemberSubscriptions
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.AccessRule)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.CommitmentPeriod)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.SubscriptionPlanSubscriptionPlanAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Where(x => !x.IsDelete && x.MemberId == memberId && x.StartDate <= utcToday && x.EndDate >= utcToday)
            .ToListAsync();
        if (items.Any())
        {
            return items.Last();
        }

        items = await _context.MemberSubscriptions
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.AccessRule)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.CommitmentPeriod)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.SubscriptionPlanSubscriptionPlanAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Where(x => x.MemberId == memberId)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
        return items.Any() ? items.First() : new MemberSubscription();
    }

    public async Task<MemberSubscription?> GetLatestByMemberAsync(int memberId)
    {
        var items = await _context.MemberSubscriptions
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Where(x => !x.IsDelete && x.MemberId == memberId && new List<string> { MemberSubscriptionStatus.Current, MemberSubscriptionStatus.NotStarted, MemberSubscriptionStatus.Freezed }.Contains(x.Status))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
        return items.Any() ? items.First() : null;
    }

    public async Task<MemberSubscription> GetBySubscriptionPlanAndMember(int subscriptionPlanId, int memberId)
    {
        var items = await _context.MemberSubscriptions
            .Where(x => !x.IsDelete && x.SubscriptionPlanId == subscriptionPlanId && x.MemberId == memberId).ToListAsync();
        return items.Any() ? items.First() : new MemberSubscription();
    }

    async Task<MemberSubscription?> IGenericRepository<MemberSubscription>.GetByIdAsync(int id)
    {
        return await _context.MemberSubscriptions
            .Include(x => x.SubscriptionPlan)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.AccessRule)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.PaymentInterval)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.CommitmentPeriod)
            .Include(x => x.SubscriptionPlan).ThenInclude(x => x.SubscriptionPlanSubscriptionPlanAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionAddons).ThenInclude(x => x.SubscriptionPlanAddon)
            .Include(x => x.MemberSubscriptionFreezes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}