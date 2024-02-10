using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ModuleOperationService : IModuleOperationService
{
    private readonly IModuleOperationRepository _moduleOperationRepository;
    public ModuleOperationService(IModuleOperationRepository moduleOperationRepository)
    {
        _moduleOperationRepository = moduleOperationRepository;
    }

    public async Task<IReadOnlyList<ModuleOperation>> GetAllAsync()
    {
        return await _moduleOperationRepository.GetAllAsync();
    }

    Task<ModuleOperation?> IGenericService<ModuleOperation>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<ModuleOperation>> GetByModuleIdAsync(int moduleId)
    {
        return await _moduleOperationRepository.GetByModuleIdAsync(moduleId);
    }

    //Add ModuleOperation by ModuleId
    public async Task AddOperationsToModule(int moduleId, IEnumerable<int> operations)
    {
        await _moduleOperationRepository.AddOperationsToModule(moduleId, operations);
    }

    //Remove ModuleOperation by ModuleId
    public async Task RemoveOperationsFromModule(int moduleId, IEnumerable<int> operations)
    {
        await _moduleOperationRepository.RemoveOperationsFromModule(moduleId, operations);
    }

    async Task IGenericService<ModuleOperation>.AddAsync(ModuleOperation entity)
    {
        await _moduleOperationRepository.AddAsync(entity);
    }

    async Task IGenericService<ModuleOperation>.UpdateAsync(ModuleOperation entity)
    {
        await _moduleOperationRepository.UpdateAsync(entity);
    }
}