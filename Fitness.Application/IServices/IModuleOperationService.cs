using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IModuleOperationService : IGenericService<ModuleOperation>
{
    Task<IReadOnlyList<ModuleOperation>> GetByModuleIdAsync(int moduleId);

    Task AddOperationsToModule(int moduleId, IEnumerable<int> operations);

    Task RemoveOperationsFromModule(int moduleId, IEnumerable<int> operations);
}