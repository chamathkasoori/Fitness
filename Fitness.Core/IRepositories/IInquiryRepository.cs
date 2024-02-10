using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IInquiryRepository : IGenericRepository<Inquiry>
{
    Task<IReadOnlyList<Inquiry>> GetAllByClubsAsync(List<int> clubIdList, string type);

    Task<IReadOnlyList<Inquiry>> GetAllByMemberAsync(int memberId);
}