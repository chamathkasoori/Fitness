using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IEmployeeRepository : IGenericRepository<Employee>
{
    public Task<IReadOnlyList<Employee>> GetAllAsync(int page, int pageSize, int roleId, int clubId, int positionId, bool isActiveOnly, string text);

    public Task<Employee?> GetByUserIdAsync(int userId);

    public Task<List<int>> GetAllAssignToNewClubsAsync();

    public Task<List<int>> GetAllAvaialbleInNewClubsAsync();
}
