using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class RoleModuleService : IRoleModuleService
{
    private readonly IRoleModuleRepository _roleModuleRepository;
    public RoleModuleService(IRoleModuleRepository roleModuleRepository)
    {
        _roleModuleRepository = roleModuleRepository;
    }

    public async Task<IReadOnlyList<RoleModule>> GetAllAsync()
    {
        return await _roleModuleRepository.GetAllAsync();
    }

    public async Task<List<RoleModule>> GetAllByRoleId(int roleId)
    {
        return await _roleModuleRepository.GetAllByRoleId(roleId);
    }

    Task<RoleModule?> IGenericService<RoleModule>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<RoleModule>.AddAsync(RoleModule entity)
    {
        await _roleModuleRepository.AddAsync(entity);
    }

    async Task IGenericService<RoleModule>.UpdateAsync(RoleModule entity)
    {
        await _roleModuleRepository.UpdateAsync(entity);
    }
}