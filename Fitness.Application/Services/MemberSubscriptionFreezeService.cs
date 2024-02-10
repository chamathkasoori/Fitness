using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberSubscriptionFreezeService : IMemberSubscriptionFreezeService
{
    private readonly IMemberSubscriptionFreezeRepository _memberSubscriptionFreezeRepository;
    public MemberSubscriptionFreezeService(IMemberSubscriptionFreezeRepository MemberSubscriptionFreezeRepository)
    {
        _memberSubscriptionFreezeRepository = MemberSubscriptionFreezeRepository;
    }

    async Task<IReadOnlyList<MemberSubscriptionFreeze>> IGenericService<MemberSubscriptionFreeze>.GetAllAsync()
    {
        return await _memberSubscriptionFreezeRepository.GetAllAsync();
    }

    async Task<MemberSubscriptionFreeze?> IGenericService<MemberSubscriptionFreeze>.GetByIdAsync(int id)
    {
        return await _memberSubscriptionFreezeRepository.GetByIdAsync(id);
    }

    public async Task<List<MemberSubscriptionFreeze>> GetAllByMemberSubscriptionAsync(int memberSubscriptionId)
    {
        return await _memberSubscriptionFreezeRepository.GetAllByMemberSubscriptionAsync(memberSubscriptionId);
    }

    async Task IGenericService<MemberSubscriptionFreeze>.AddAsync(MemberSubscriptionFreeze entity)
    {
        await _memberSubscriptionFreezeRepository.AddAsync(entity);
    }

    async Task IGenericService<MemberSubscriptionFreeze>.UpdateAsync(MemberSubscriptionFreeze entity)
    {
        await _memberSubscriptionFreezeRepository.UpdateAsync(entity);
    }
}