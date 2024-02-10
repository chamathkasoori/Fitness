using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class ModuleRepository : GenericRepository<Module>, IModuleRepository
{
    private readonly FitnessContext _context;
    public ModuleRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<IReadOnlyList<Module>> IGenericRepository<Module>.GetAllAsync()
    {
        return await _context.Modules
            .Include(m => m.ModuleOperations).ThenInclude(o => o.Operation)
            .Where(x => !x.IsDelete)
            .ToListAsync();
    }

    public async Task<bool> IsExists(List<int> moduleIds)
    {
        return await _context.Modules.AnyAsync(m => moduleIds.Contains(m.Id));
    }

    public async Task<IReadOnlyList<Module>> GetParentModulesByChildModules(List<int> moduleIds)
    {
        var parentModuleIds = await _context.Modules.Where(mo => !mo.IsDelete && moduleIds.Contains(mo.Id)).Select(mo => mo.ParentModuleId).ToListAsync();
        var parentModuleIds2 = await _context.Modules.Where(mo => !mo.IsDelete && parentModuleIds.Contains(mo.Id)).Select(mo => mo.ParentModuleId).ToListAsync();
        var parentModuleIds3 = await _context.Modules.Where(mo => !mo.IsDelete && parentModuleIds2.Contains(mo.Id)).Select(mo => mo.ParentModuleId).ToListAsync();
        return await _context.Modules.Where(m => parentModuleIds.Contains(m.Id) || parentModuleIds2.Contains(m.Id) || parentModuleIds3.Contains(m.Id)).ToListAsync();
    }

    async Task<Module?> IGenericRepository<Module>.GetByIdAsync(int id)
    {
        return await _context.Modules
            .Include(x => x.ModuleOperations)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Module entity)
    {
        await _context.Modules.AddAsync(entity);
        await _context.SaveChangesAsync();
        if (string.IsNullOrEmpty(entity.Hierarchy))
        {
            entity.Hierarchy = entity.Id.ToString();
        }
        else
        {
            entity.Hierarchy = entity.Hierarchy + "-" + entity.Id.ToString();
        }
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}