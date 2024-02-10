using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberVisitService : IMemberVisitService
{
    private readonly IMemberVisitRepository _memberVisitRepository;
    public MemberVisitService(IMemberVisitRepository MemberVisitRepository)
    {
        _memberVisitRepository = MemberVisitRepository;
    }

    Task<IReadOnlyList<MemberVisit>> IGenericService<MemberVisit>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<MemberVisit>> GetAllAsync(int memberId, int clubId, int pageSize)
    {
        return await _memberVisitRepository.GetAllAsync(memberId, clubId, pageSize);
    }

    Task<MemberVisit?> IGenericService<MemberVisit>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<MemberVisit>.AddAsync(MemberVisit entity)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<MemberVisit>.UpdateAsync(MemberVisit entity)
    {
        throw new NotImplementedException();
    }
}