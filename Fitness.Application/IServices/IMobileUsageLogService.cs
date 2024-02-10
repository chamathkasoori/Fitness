using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMobileUsageLogService : IGenericService<MobileUsageLog>
{
    Task<List<MobileUsageLog>> GetByMemberAsync(int memberId);
}