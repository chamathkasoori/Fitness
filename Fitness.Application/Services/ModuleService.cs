using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ModuleService : IModuleService
{
    private readonly IModuleRepository _moduleRepository;
    public ModuleService(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<IReadOnlyList<Module>> GetAllAsync()
    {
        return await _moduleRepository.GetAllAsync();
    }

    //Get parent modules by child modules
    public async Task<IReadOnlyList<Module>> GetParentModulesByChildModules(List<int> moduleIds)
    {
        return await _moduleRepository.GetParentModulesByChildModules(moduleIds);
    }

    //Check by ids
    public async Task<bool> FindByIds(List<int> moduleIds)
    {
        return await _moduleRepository.IsExists(moduleIds);
    }

    async Task<Module?> IGenericService<Module>.GetByIdAsync(int id)
    {
        return await _moduleRepository.GetByIdAsync(id);
    }

    async Task IGenericService<Module>.AddAsync(Module entity)
    {
        await _moduleRepository.AddAsync(entity);
    }
    async Task IGenericService<Module>.UpdateAsync(Module entity)
    {
        await _moduleRepository.UpdateAsync(entity);
    }
}