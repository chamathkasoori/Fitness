using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IRoleModuleService : IGenericService<RoleModule>
{
    public Task<List<RoleModule>> GetAllByRoleId(int roleId);
}