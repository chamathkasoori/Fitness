using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MobileUsageLogService : IMobileUsageLogService
{
    private readonly IMobileUsageLogRepository _mobileUsageLogRepository;
    public MobileUsageLogService(IMobileUsageLogRepository mobileUsageLogRepository)
    {
        _mobileUsageLogRepository = mobileUsageLogRepository;
    }

    public Task<IReadOnlyList<MobileUsageLog>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<MobileUsageLog>> GetByMemberAsync(int memberId)
    {
        return await _mobileUsageLogRepository.GetByMemberIdAsync(memberId);
    }

    public Task<MobileUsageLog?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(MobileUsageLog mobileUsage)
    {
        await _mobileUsageLogRepository.AddAsync(mobileUsage);
    }

    public Task UpdateAsync(MobileUsageLog entity)
    {
        throw new NotImplementedException();
    }
}