using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberRepository : IGenericRepository<Member>
{
    public Task<IReadOnlyList<Member>> GetAllAsync(int page, int pageSize, string searchText);

    public Task<IReadOnlyList<Member>> GetAllByMemberTypeAsync(int page, int pageSize, bool memberType, DateTime startDate, DateTime endDate);

    public Task<Member> GetByMobileNo(string mobileNo);

    public Task<bool> IsPersonalIdentificationNumberExists(int memberId, string val);
}
