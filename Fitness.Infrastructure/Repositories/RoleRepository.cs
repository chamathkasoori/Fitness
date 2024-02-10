using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly FitnessContext _context;
    public RoleRepository(FitnessContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync()
    {
        return await _context.Roles
            .Include(r => r.Company)
            .Include(r => r.RoleModules.Where(x=> x.IsDelete == false)).ThenInclude(r => r.RoleModuleOperations.Where(y => y.IsDelete == false))
            .Include(r => r.RoleModules).ThenInclude(r => r.Module).ThenInclude(r => r.ParentModule)
            .Where(x => !x.IsDelete)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .Include(x => x.RoleModules.Where(y => y.IsDelete == false)).ThenInclude(x => x.RoleModuleOperations.Where(y => y.IsDelete == false))
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Role> GetByNameAsync(string name)
    {
        var item = await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
        return item == null ? new Role() : item;
    }

    async Task IRoleRepository.AddAsync(Role entity)
    {
        await _context.Roles.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role entity)
    {
        var exists = _context.Roles.Any(r => r.Id == entity.Id);
        if (!exists)
            throw new Exception("Not Found");
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}