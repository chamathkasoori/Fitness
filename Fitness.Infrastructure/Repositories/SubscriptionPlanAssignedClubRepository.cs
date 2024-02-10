using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class SubscriptionPlanAssignedClubRepository : GenericRepository<SubscriptionPlanAssignedClub>, ISubscriptionPlanAssignedClubRepository
{
    private readonly FitnessContext _context;
    public SubscriptionPlanAssignedClubRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task SaveAsync(IEnumerable<SubscriptionPlanAssignedClub> items)
    {
        await _context.SubscriptionPlanAssignedClubs.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }
}