using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface ISubscriptionPlanService : IGenericService<SubscriptionPlan>
{
    public Task<List<SubscriptionPlan>> GetAllByFilterAsync(int clubId, int ruleId);

    public Task<List<SubscriptionPlan>> GetAllByClubAsync(int clubId, int applicationId);

    public Task<List<SubscriptionPlan>> GetAllByAccessRuleAsync(int accessRuleId);

    public Task AddNewClubAsync(int clubId, int createdBy);
}