using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IEmployeeAvailableClubRepository : IGenericRepository<EmployeeAvailableClub>
{
    public Task SaveAsync(IEnumerable<EmployeeAvailableClub> items);
}