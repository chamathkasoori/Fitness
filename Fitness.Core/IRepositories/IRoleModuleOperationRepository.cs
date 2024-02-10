using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;

public interface IRoleModuleOperationRepository : IGenericRepository<RoleModuleOperation>
{
    Task AddRangeAsync(IEnumerable<RoleModuleOperation> entities);
}