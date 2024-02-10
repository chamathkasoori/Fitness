using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMobileUsageLogRepository : IGenericRepository<MobileUsageLog>
{
    Task<List<MobileUsageLog>> GetByMemberIdAsync(int memberId);
}