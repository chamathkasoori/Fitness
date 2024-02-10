using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class RoleModuleOperationService : IRoleModuleOperationService
{
    private readonly IRoleModuleOperationRepository _roleModuleOperationRepository;
    public RoleModuleOperationService(IRoleModuleOperationRepository roleModuleOperationRepository)
    {
        _roleModuleOperationRepository = roleModuleOperationRepository;
    }

    public Task<IReadOnlyList<RoleModuleOperation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<RoleModuleOperation?> IGenericService<RoleModuleOperation>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<RoleModuleOperation>.AddAsync(RoleModuleOperation entity)
    {
        await _roleModuleOperationRepository.AddAsync(entity);
    }

    async Task IGenericService<RoleModuleOperation>.UpdateAsync(RoleModuleOperation entity)
    {
        await _roleModuleOperationRepository.UpdateAsync(entity);
    }
}