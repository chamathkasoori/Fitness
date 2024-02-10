using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface ISubscriptionPlanAddonService : IGenericService<SubscriptionPlanAddon>
{
    public Task<List<SubscriptionPlanAddon>> GetBySubscriptionPlanAsync(int subscriptionPlanId);

    Task<IReadOnlyList<SubscriptionPlanAddon>> GetByTypeAsync(string type);
}