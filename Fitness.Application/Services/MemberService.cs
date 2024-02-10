using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberService : IMemberService
{
    private readonly IMemberRepository memberRepository;
    public MemberService(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    Task<IReadOnlyList<Member>> IGenericService<Member>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Member>> GetAllAsync(int page, int pageSize, string searchText)
    {
        return await memberRepository.GetAllAsync(page, pageSize, searchText);
    }

    public async Task<IReadOnlyList<Member>> GetAllByMemberTypeAsync(int page, int pageSize, bool membertype, DateTime startDate, DateTime endDate)
    {
        return await memberRepository.GetAllByMemberTypeAsync(page, pageSize, membertype, startDate, endDate);
    }

    async Task<Member?> IGenericService<Member>.GetByIdAsync(int id)
    {
        return await memberRepository.GetByIdAsync(id);
    }

    public async Task<Member> GetByMobileNo(string mobileNo)
    {
        return await memberRepository.GetByMobileNo(mobileNo);
    }

    public async Task AddAsync(Member entity)
    {
        await memberRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Member entity)
    {
        await memberRepository.UpdateAsync(entity);
    }

    public async Task<bool> IsPersonalIdentificationNumberExists(int memberId, string val)
    {
        return await memberRepository.IsPersonalIdentificationNumberExists(memberId, val);
    }
}
