using Fitness.Application.IServices;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;

namespace Fitness.Application.Services;
public class SubscriptionPlanDiscountCombinationService : ISubscriptionPlanDiscountCombinationService
{
    private readonly ISubscriptionPlanDiscountCombinationRepository subscriptionPlanDiscountCombinationRepository;
    public SubscriptionPlanDiscountCombinationService(ISubscriptionPlanDiscountCombinationRepository subscriptionPlanDiscountCombinationRepository)
    {
        this.subscriptionPlanDiscountCombinationRepository = subscriptionPlanDiscountCombinationRepository;
    }

    public Task<IReadOnlyList<SubscriptionPlanDiscountCombination>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<SubscriptionPlanDiscountCombination>> GetAllCombinedAsync(int subscriptionPlanDiscountId)
    {
        return await subscriptionPlanDiscountCombinationRepository.GetAllCombinedAsync(subscriptionPlanDiscountId);
    }

    public Task<SubscriptionPlanDiscountCombination?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(SubscriptionPlanDiscountCombination entity)
    {
        await subscriptionPlanDiscountCombinationRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(SubscriptionPlanDiscountCombination entity)
    {
        await subscriptionPlanDiscountCombinationRepository.UpdateAsync(entity);
    }
}
