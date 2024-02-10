using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;

public interface IModuleOperationRepository : IGenericRepository<ModuleOperation>
{
    Task<IReadOnlyList<ModuleOperation>> GetByModuleIdAsync(int moduleId);

    Task AddOperationsToModule(int moduleId, IEnumerable<int> operations);

    Task RemoveOperationsFromModule(int moduleId, IEnumerable<int> operations);
}