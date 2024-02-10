using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class SubscriptionPlanSettingService : ISubscriptionPlanSettingService
{
    private readonly ISubscriptionPlanSettingRepository _subscriptionPlanSettingRepository;
    public SubscriptionPlanSettingService(ISubscriptionPlanSettingRepository SubscriptionPlanSettingRepository)
    {
        _subscriptionPlanSettingRepository = SubscriptionPlanSettingRepository;
    }

    async Task<IReadOnlyList<SubscriptionPlanSetting>> IGenericService<SubscriptionPlanSetting>.GetAllAsync()
    {
        return await _subscriptionPlanSettingRepository.GetAllAsync();
    }

    Task<SubscriptionPlanSetting?> IGenericService<SubscriptionPlanSetting>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<SubscriptionPlanSetting>.AddAsync(SubscriptionPlanSetting entity)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<SubscriptionPlanSetting>.UpdateAsync(SubscriptionPlanSetting entity)
    {
        throw new NotImplementedException();
    }
}