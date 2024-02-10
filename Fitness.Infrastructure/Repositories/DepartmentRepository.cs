using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    private readonly FitnessContext _context;
    public DepartmentRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<IReadOnlyList<Department>> IGenericRepository<Department>.GetAllAsync()
    {
        return await _context.Departments
            .Include(x => x.Company)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}