using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class RoleModuleRepository : GenericRepository<RoleModule>, IRoleModuleRepository
{
    private readonly FitnessContext _context;
    public RoleModuleRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<RoleModule>> GetAllByRoleId(int roleId)
    {
        return await _context.RoleModules
            .Where(x => !x.IsDelete && x.RoleId == roleId)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}