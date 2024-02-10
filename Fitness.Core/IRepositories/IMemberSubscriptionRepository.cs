using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberSubscriptionRepository : IGenericRepository<MemberSubscription>
{
    public Task<List<MemberSubscription>> GetActiveMemberSubscriptionsAsync(int memberId);

    public Task<List<MemberSubscription>> GetArchivedMemberSubscriptionsAsync(int memberId);

    public Task<List<MemberSubscription>> GetTransfferedMemberSubscriptionsAsync(int memberId);

    public Task<List<MemberSubscription>> GetAllFreezedExpiredAsync();

    public Task<List<MemberSubscription>> GetAllCurrentExpiredAsync();

    public Task<List<MemberSubscription>> GetAllForFreezeAsync();

    public Task<MemberSubscription?> GetLatestByMemberAsync(int memberId);

    public Task<MemberSubscription> GetCurrentByMemberAsync(int memberId);

    public Task<List<MemberSubscription>> GetAllNotStartedActiveAsync();

    public Task<MemberSubscription> GetBySubscriptionPlanAndMember(int subscriptionPlanId, int memberId);
}