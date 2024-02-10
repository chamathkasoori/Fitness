using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberSubscriptionTransferService : IMemberSubscriptionTransferService
{
    private readonly IMemberSubscriptionTransferRepository _memberSubscriptionTransferRepository;
    public MemberSubscriptionTransferService(IMemberSubscriptionTransferRepository subscriptionPlanTransactionRepository, IMemberRepository memberRepository) 
    { 
        _memberSubscriptionTransferRepository = subscriptionPlanTransactionRepository;
    }

    async Task IGenericService<MemberSubscriptionTransfer>.AddAsync(MemberSubscriptionTransfer entity)
    {
        await _memberSubscriptionTransferRepository.AddAsync(entity);
    }

    Task<IReadOnlyList<MemberSubscriptionTransfer>> IGenericService<MemberSubscriptionTransfer>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<MemberSubscriptionTransfer?> IGenericService<MemberSubscriptionTransfer>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<MemberSubscriptionTransfer>.UpdateAsync(MemberSubscriptionTransfer entity)
    {
        throw new NotImplementedException();
    }
}