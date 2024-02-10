using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class EmployeeAvailableClubRepository : GenericRepository<EmployeeAvailableClub>, IEmployeeAvailableClubRepository
{
    private readonly FitnessContext _context;
    public EmployeeAvailableClubRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task SaveAsync(IEnumerable<EmployeeAvailableClub> items)
    {
        await _context.EmployeeAvailableClubs.AddRangeAsync(items);
        await _context.SaveChangesAsync();
    }
}