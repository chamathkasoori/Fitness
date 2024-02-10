using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IRoleService
{
    public Task<IReadOnlyList<Role>> GetAllAsync();

    public Task<Role?> GetByIdAsync(int roleId);

    public Task<Role> GetByNameAsync(string name);

    public Task AddAsync(Role entity);

    public Task UpdateAsync(Role entity);
}