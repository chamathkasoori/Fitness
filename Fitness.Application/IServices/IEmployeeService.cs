using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IEmployeeService : IGenericService<Employee>
{
    public Task<IReadOnlyList<Employee>> GetAllAsync(int page, int pageSize, int roleId, int clubId, int positionId, bool isActiveOnly, string text);

    public Task<List<int>> GetAssignedClulbIdsAsync(int employeeId);

    public Task<Employee?> GetByUserIdAsync(int userId);

    public Task AddNewClubAsync(int clubId, int createdBy);
}
