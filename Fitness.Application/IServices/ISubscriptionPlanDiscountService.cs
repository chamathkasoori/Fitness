using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface ISubscriptionPlanDiscountService : IGenericService<SubscriptionPlanDiscount>
{
    public Task<IReadOnlyList<SubscriptionPlanDiscount>> GetAllBySubscriptionPlanAsync(int subscriptionPlanId);

    public Task<List<SubscriptionPlanDiscount>> GetAllByClubAndSubscriptionPlansAsync(int clubId, List<int> subscriptionPlanIds);

    public Task<bool> IsNameExist(int id, string name);
}
