using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class SubscriptionPlanDiscountService : ISubscriptionPlanDiscountService
{
    private readonly ISubscriptionPlanDiscountRepository _subscriptionPlanDiscountRepository;
    public SubscriptionPlanDiscountService(ISubscriptionPlanDiscountRepository subscriptionPlanDiscountRepository)
    {
        _subscriptionPlanDiscountRepository = subscriptionPlanDiscountRepository;
    }

    async Task<IReadOnlyList<SubscriptionPlanDiscount>> IGenericService<SubscriptionPlanDiscount>.GetAllAsync()
    {
        return await _subscriptionPlanDiscountRepository.GetAllAsync();
    }

    public async Task<IReadOnlyList<SubscriptionPlanDiscount>> GetAllBySubscriptionPlanAsync(int subscriptionPlanId)
    {
        return await _subscriptionPlanDiscountRepository.GetAllBySubscriptionPlanAsync(subscriptionPlanId);
    }

    public async Task<List<SubscriptionPlanDiscount>> GetAllByClubAndSubscriptionPlansAsync(int clubId, List<int> subscriptionPlanIds)
    {
        return await _subscriptionPlanDiscountRepository.GetAllByClubAndSubscriptionPlansAsync(clubId, subscriptionPlanIds);
    }

    async Task<SubscriptionPlanDiscount?> IGenericService<SubscriptionPlanDiscount>.GetByIdAsync(int id)
    {
        return await _subscriptionPlanDiscountRepository.GetByIdAsync(id);
    }

    public async Task<bool> IsNameExist(int id, string name)
    {
        return await _subscriptionPlanDiscountRepository.IsNameExist(id, name);
    }

    async Task IGenericService<SubscriptionPlanDiscount>.AddAsync(SubscriptionPlanDiscount entity)
    {
        await _subscriptionPlanDiscountRepository.AddAsync(entity);
    }

    async Task IGenericService<SubscriptionPlanDiscount>.UpdateAsync(SubscriptionPlanDiscount entity)
    {
        await _subscriptionPlanDiscountRepository.UpdateAsync(entity);
    }
}
