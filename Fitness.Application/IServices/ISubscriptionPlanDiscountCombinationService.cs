using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface ISubscriptionPlanDiscountCombinationService : IGenericService<SubscriptionPlanDiscountCombination>
{
    public Task<List<SubscriptionPlanDiscountCombination>> GetAllCombinedAsync(int subscriptionPlanDiscountId);
}