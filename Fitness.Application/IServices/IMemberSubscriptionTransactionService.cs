using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMemberSubscriptionTransactionService : IGenericService<MemberSubscriptionTransaction>
{
    public Task<List<MemberSubscription>> GetAllByMemberAsync(int memberId);

}
