using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class SubscriptionPlanAssignedClubService : ISubscriptionPlanAssignedClubService
{
    private readonly ISubscriptionPlanAssignedClubRepository _subscriptionPlanAssignedClubRepository;
    private readonly ISubscriptionPlanService _subscriptionPlanService;
    public SubscriptionPlanAssignedClubService(ISubscriptionPlanAssignedClubRepository SubscriptionPlanAssignedClubRepository, ISubscriptionPlanService subscriptionPlanService)
    {
        _subscriptionPlanAssignedClubRepository = SubscriptionPlanAssignedClubRepository;
        _subscriptionPlanService = subscriptionPlanService;
    }

    async Task<IReadOnlyList<SubscriptionPlanAssignedClub>> IGenericService<SubscriptionPlanAssignedClub>.GetAllAsync()
    {
        return await _subscriptionPlanAssignedClubRepository.GetAllAsync();
    }

    async Task<SubscriptionPlanAssignedClub?> IGenericService<SubscriptionPlanAssignedClub>.GetByIdAsync(int id)
    {
        return await _subscriptionPlanAssignedClubRepository.GetByIdAsync(id);
    }

    async Task IGenericService<SubscriptionPlanAssignedClub>.AddAsync(SubscriptionPlanAssignedClub entity)
    {
        await _subscriptionPlanAssignedClubRepository.AddAsync(entity);
    }

    async Task IGenericService<SubscriptionPlanAssignedClub>.UpdateAsync(SubscriptionPlanAssignedClub entity)
    {
        await _subscriptionPlanAssignedClubRepository.UpdateAsync(entity);
    }

    
}