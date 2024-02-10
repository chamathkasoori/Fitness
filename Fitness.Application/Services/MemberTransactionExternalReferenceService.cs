using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberTransactionExternalReferenceService : IMemberTransactionExternalReferenceService
{
    private readonly IMemberTransactionExternalReferenceRepository _subscriptionPlanTransactionRepository;
    public MemberTransactionExternalReferenceService(IMemberTransactionExternalReferenceRepository subscriptionPlanTransactionRepository, IMemberRepository memberRepository) 
    { 
        _subscriptionPlanTransactionRepository = subscriptionPlanTransactionRepository;
    }

    async Task IGenericService<MemberTransactionExternalReference>.AddAsync(MemberTransactionExternalReference entity)
    {
        await _subscriptionPlanTransactionRepository.AddAsync(entity);
    }

    Task<IReadOnlyList<MemberTransactionExternalReference>> IGenericService<MemberTransactionExternalReference>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<MemberTransactionExternalReference?> IGenericService<MemberTransactionExternalReference>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<MemberTransactionExternalReference>.UpdateAsync(MemberTransactionExternalReference entity)
    {
        throw new NotImplementedException();
    }
}