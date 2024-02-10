using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface ISubscriptionPlanRepository : IGenericRepository<SubscriptionPlan>
{
    public Task<List<SubscriptionPlan>> GetAllByFilterAsync(int clubId, int ruleId);

    public Task<List<SubscriptionPlan>> GetAllByClubAsync(int clubId, int applicationId);

    public Task<List<SubscriptionPlan>> GetAllByAccessRuleAsync(int accessRuleId);

    public Task<List<int>> GetAllAddToNewClubsAsync();
}