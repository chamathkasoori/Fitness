using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private readonly FitnessContext _context;
    public EmployeeRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync(int page, int pageSize, int roleId, int clubId, int positionId, bool isActiveOnly, string text)
    {
        if (page == 0) page = 1;
        if (pageSize == 0) pageSize = int.MaxValue;
        int skip = (page - 1) * pageSize;

        var query = _context.Employees
            .Include(x => x.User)
            .Include(x => x.Department)
            .Include(x => x.Position)
            .Include(x => x.Role)
            .Include(x => x.EmployeeAssignedClubs)
            .Where(x => !x.IsDelete
                && (text == "" || x.User.FirstName.Contains(text) || x.User.LastName.Contains(text))
                && (positionId == 0 || x.PositionId == positionId)
                && (roleId == 0 || x.RoleId == roleId)
                && (clubId == 0 || x.EmployeeAssignedClubs.Any(ec => ec.Id == clubId))
                && (!isActiveOnly || x.IsActive))
            .OrderByDescending(x => x.Id)
            .Skip(skip)
            .Take(pageSize);
        return await query.ToListAsync();
    }

    async Task<Employee?> IGenericRepository<Employee>.GetByIdAsync(int id)
    {
        return await _context
            .Employees
            .Include(x => x.User).ThenInclude(y => y.Address).ThenInclude(c => c!.Country)
            .Include(x => x.User).ThenInclude(y => y.Address).ThenInclude(c => c!.Region)
            .Include(x => x.User).ThenInclude(y => y.Address).ThenInclude(c => c!.City)
            .Include(x => x.EmployeeAssignedClubs)
            .Include(x => x.EmployeeAvailableClubs)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Employee?> GetByUserIdAsync(int userId)
    {
        return await _context.Employees
            .Include(x => x.User)
            .Include(x => x.Department)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<List<int>> GetAllAssignToNewClubsAsync()
    {
        return await _context.Employees
            .Where(x => x.AssignToNewClubs).Select(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<int>> GetAllAvaialbleInNewClubsAsync()
    {
        return await _context.Employees
            .Where(x => x.AvailableToNewClubs).Select(x => x.Id)
            .ToListAsync();
    }
}
