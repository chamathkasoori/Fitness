using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMemberVisitService : IGenericService<MemberVisit>
{
    public Task<IReadOnlyList<MemberVisit>> GetAllAsync(int memberId, int clubId, int pageSize);
}