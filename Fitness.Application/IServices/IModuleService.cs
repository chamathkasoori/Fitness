using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IModuleService : IGenericService<Module>
{
    Task<bool> FindByIds(List<int> moduleIds);

    Task<IReadOnlyList<Module>> GetParentModulesByChildModules(List<int> moduleIds);
}