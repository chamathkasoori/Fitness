using Fitness.Application.IServices;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;

namespace Fitness.Application.Services;
public class MemberSubscriptionTransactionService : IMemberSubscriptionTransactionService
{
    private readonly IMemberSubscriptionTransactionRepository memberSubscriptionTransactionRepository;

    public MemberSubscriptionTransactionService(IMemberSubscriptionTransactionRepository MemberSubscriptionTransactionRepository)
    {
        memberSubscriptionTransactionRepository = MemberSubscriptionTransactionRepository;
    }

    Task<IReadOnlyList<MemberSubscriptionTransaction>> IGenericService<MemberSubscriptionTransaction>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<MemberSubscription>> GetAllByMemberAsync(int memberId)
    {
        return await memberSubscriptionTransactionRepository.GetAllByMemberAsync(memberId);
    }

    Task<MemberSubscriptionTransaction?> IGenericService<MemberSubscriptionTransaction>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<MemberSubscriptionTransaction>.AddAsync(MemberSubscriptionTransaction entity)
    {
        await memberSubscriptionTransactionRepository.AddAsync(entity);
    }

    Task IGenericService<MemberSubscriptionTransaction>.UpdateAsync(MemberSubscriptionTransaction entity)
    {
        throw new NotImplementedException();
    }
}
