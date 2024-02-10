using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberVisitRepository : IGenericRepository<MemberVisit>
{
    public Task<IReadOnlyList<MemberVisit>> GetAllAsync(int memberId, int clubId, int pageSize);
}