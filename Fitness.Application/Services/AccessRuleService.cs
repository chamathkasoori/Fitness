using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class AccessRuleService : IAccessRuleService
{
    private readonly IAccessRuleRepository accessRuleRepository;
    public AccessRuleService(IAccessRuleRepository accessRuleRepository)
    {
        this.accessRuleRepository = accessRuleRepository;
    }

    async Task<IReadOnlyList<AccessRule>> IGenericService<AccessRule>.GetAllAsync()
    {
        return await accessRuleRepository.GetAllAsync();
    }

    async Task<AccessRule?> IGenericService<AccessRule>.GetByIdAsync(int id)
    {
        return await accessRuleRepository.GetByIdAsync(id);
    }

    async Task IGenericService<AccessRule>.AddAsync(AccessRule entity)
    {
        await accessRuleRepository.AddAsync(entity);
    }

    async Task IGenericService<AccessRule>.UpdateAsync(AccessRule entity)
    {
        await accessRuleRepository.UpdateAsync(entity);
    }
}
