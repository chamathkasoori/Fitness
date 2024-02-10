using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IModuleRepository : IGenericRepository<Module>
{
    Task<bool> IsExists(List<int> moduleIds);

    Task<IReadOnlyList<Module>> GetParentModulesByChildModules(List<int> moduleIds);
}