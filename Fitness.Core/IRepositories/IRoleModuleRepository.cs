using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IRoleModuleRepository : IGenericRepository<RoleModule>
{
    public Task<List<RoleModule>> GetAllByRoleId(int roleId);
}