using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class EmployeeAssignedClubRepository : GenericRepository<EmployeeAssignedClub>, IEmployeeAssignedClubRepository
{
    private readonly FitnessContext _context;
    public EmployeeAssignedClubRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<int>> GetClulbIdsAsync(int employeeId)
    {
        return await _context.EmployeeAssignedClubs
            .Where(x => !x.IsDelete && x.IsActive && x.EmployeeId == employeeId)
            .Select(x => x.ClubId)
            .ToListAsync();
    }

    public async Task SaveAsync(IEnumerable<EmployeeAssignedClub> items)
    {
        await _context.EmployeeAssignedClubs.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }
}