using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface ISubscriptionPlanAssignedClubRepository : IGenericRepository<SubscriptionPlanAssignedClub>
{
    public Task SaveAsync(IEnumerable<SubscriptionPlanAssignedClub> items);
}