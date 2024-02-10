using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class SubscriptionPlanAddonService : ISubscriptionPlanAddonService
{
    private readonly ISubscriptionPlanAddonRepository _subscriptionPlanAddonRepository;
    public SubscriptionPlanAddonService(ISubscriptionPlanAddonRepository SubscriptionPlanAddonRepository)
    {
        _subscriptionPlanAddonRepository = SubscriptionPlanAddonRepository;
    }

    async Task<IReadOnlyList<SubscriptionPlanAddon>> IGenericService<SubscriptionPlanAddon>.GetAllAsync()
    {
        return await _subscriptionPlanAddonRepository.GetAllAsync();
    }

    public async Task<List<SubscriptionPlanAddon>> GetBySubscriptionPlanAsync(int subscriptionPlanId)
    {
        return await _subscriptionPlanAddonRepository.GetBySubscriptionPlanAsync(subscriptionPlanId);
    }

    async Task<IReadOnlyList<SubscriptionPlanAddon>> ISubscriptionPlanAddonService.GetByTypeAsync(string type)
    {
        return await _subscriptionPlanAddonRepository.GetByTypeAsync(type);
    }

    async Task<SubscriptionPlanAddon?> IGenericService<SubscriptionPlanAddon>.GetByIdAsync(int id)
    {
        return await _subscriptionPlanAddonRepository.GetByIdAsync(id);
    }

    async Task IGenericService<SubscriptionPlanAddon>.AddAsync(SubscriptionPlanAddon entity)
    {
        await _subscriptionPlanAddonRepository.AddAsync(entity);
    }

    async Task IGenericService<SubscriptionPlanAddon>.UpdateAsync(SubscriptionPlanAddon entity)
    {
        await _subscriptionPlanAddonRepository.UpdateAsync(entity);
    }
}