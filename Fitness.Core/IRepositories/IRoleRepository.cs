using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IRoleRepository
{
    public Task<IReadOnlyList<Role>> GetAllAsync();

    public Task<Role?> GetByIdAsync(int id);

    public Task<Role> GetByNameAsync(string name);

    public Task AddAsync(Role entity);

    public Task UpdateAsync(Role entity);
}