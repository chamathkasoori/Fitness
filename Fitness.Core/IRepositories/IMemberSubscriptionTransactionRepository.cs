using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberSubscriptionTransactionRepository : IGenericRepository<MemberSubscriptionTransaction>
{
    public Task<List<MemberSubscription>> GetAllByMemberAsync(int memberId);
}