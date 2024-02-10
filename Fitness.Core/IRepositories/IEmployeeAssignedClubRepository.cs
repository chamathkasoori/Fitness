using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IEmployeeAssignedClubRepository : IGenericRepository<EmployeeAssignedClub>
{
    public Task<List<int>> GetClulbIdsAsync(int employeeId);

    public Task SaveAsync(IEnumerable<EmployeeAssignedClub> items);
}