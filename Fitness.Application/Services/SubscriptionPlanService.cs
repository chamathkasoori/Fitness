using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;
using System;

namespace Fitness.Application.Services;
public class SubscriptionPlanService : ISubscriptionPlanService
{
    private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
    private readonly ISubscriptionPlanAssignedClubRepository _subscriptionPlanAssignedClubRepository;
    public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository, ISubscriptionPlanAssignedClubRepository subscriptionPlanAssignedClubRepository)
    {
        _subscriptionPlanRepository = subscriptionPlanRepository;
        _subscriptionPlanAssignedClubRepository = subscriptionPlanAssignedClubRepository;
    }

    Task<IReadOnlyList<SubscriptionPlan>> IGenericService<SubscriptionPlan>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task<List<SubscriptionPlan>> ISubscriptionPlanService.GetAllByFilterAsync(int clubId, int ruleId)
    {
        return await _subscriptionPlanRepository.GetAllByFilterAsync(clubId, ruleId);
    }

    async Task<List<SubscriptionPlan>> ISubscriptionPlanService.GetAllByClubAsync(int clubId, int applicationId)
    {
        return await _subscriptionPlanRepository.GetAllByClubAsync(clubId, applicationId);
    }

    async Task<List<SubscriptionPlan>> ISubscriptionPlanService.GetAllByAccessRuleAsync(int accessRuleId)
    {
        return await _subscriptionPlanRepository.GetAllByAccessRuleAsync(accessRuleId);
    }

    async Task<SubscriptionPlan?> IGenericService<SubscriptionPlan>.GetByIdAsync(int id)
    {
        return await _subscriptionPlanRepository.GetByIdAsync(id);
    }

    async Task IGenericService<SubscriptionPlan>.AddAsync(SubscriptionPlan entity)
    {
        await _subscriptionPlanRepository.AddAsync(entity);
    }

    async Task IGenericService<SubscriptionPlan>.UpdateAsync(SubscriptionPlan entity)
    {
        await _subscriptionPlanRepository.UpdateAsync(entity);
    }

    async Task ISubscriptionPlanService.AddNewClubAsync(int clubId, int createdBy)
    {
        var subscriptionPlanList = await _subscriptionPlanRepository.GetAllAddToNewClubsAsync();
        if (subscriptionPlanList.Any())
        {
            List<SubscriptionPlanAssignedClub> items = new List<SubscriptionPlanAssignedClub>();
            foreach (int subscriptionPlanId in subscriptionPlanList)
            {
                SubscriptionPlanAssignedClub item = new SubscriptionPlanAssignedClub();
                item.SubscriptionPlanId = subscriptionPlanId;
                item.ClubId = clubId;
                item.CreatedBy = createdBy;
                items.Add(item);
            }
            await _subscriptionPlanAssignedClubRepository.SaveAsync(items);
        }
    }
}