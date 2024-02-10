using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMemberSubscriptionFreezeService : IGenericService<MemberSubscriptionFreeze>
{
    public Task<List<MemberSubscriptionFreeze>> GetAllByMemberSubscriptionAsync(int memberSubscriptionId);
}