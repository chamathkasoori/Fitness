using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface ISubscriptionPlanAddonRepository : IGenericRepository<SubscriptionPlanAddon>
{
    public Task<List<SubscriptionPlanAddon>> GetBySubscriptionPlanAsync(int subscriptionPlanId);

    Task<IReadOnlyList<SubscriptionPlanAddon>> GetByTypeAsync(string type);
}