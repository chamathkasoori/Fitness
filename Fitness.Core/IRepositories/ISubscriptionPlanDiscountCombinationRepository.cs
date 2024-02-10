using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface ISubscriptionPlanDiscountCombinationRepository : IGenericRepository<SubscriptionPlanDiscountCombination>
{
    public Task<List<SubscriptionPlanDiscountCombination>> GetAllCombinedAsync(int subscriptionPlanDiscountId);
}
