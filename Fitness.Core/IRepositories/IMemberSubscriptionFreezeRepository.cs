using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberSubscriptionFreezeRepository : IGenericRepository<MemberSubscriptionFreeze>
{
    public Task<List<MemberSubscriptionFreeze>> GetAllByMemberSubscriptionAsync(int memberSubscriptionId);
}