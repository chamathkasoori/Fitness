using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class SystemConfigService : ISystemConfigService
{
    private readonly ISystemConfigRepository _systemConfigRepository;

    public SystemConfigService(ISystemConfigRepository systemConfigRepository)
    {
        _systemConfigRepository = systemConfigRepository;
    }

    public async Task<IReadOnlyList<SystemConfig>> GetAllAsync()
    {
        return await _systemConfigRepository.GetAllAsync();
    }

    public async Task<SystemConfig?> GetFirstAsync()
    {
        return await _systemConfigRepository.GetFirstAsync();
    }

    public async Task<SystemConfig?> GetByIdAsync(int id)
    {
        return await _systemConfigRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(SystemConfig entity)
    {
        await _systemConfigRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(SystemConfig entity)
    {
        await _systemConfigRepository.UpdateAsync(entity);
    }
}