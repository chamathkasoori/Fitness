using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class ModuleOperationRepository : GenericRepository<ModuleOperation>, IModuleOperationRepository
{
    private readonly FitnessContext _context;
    public ModuleOperationRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ModuleOperation>> GetByModuleIdAsync(int moduleId)
    {
        return await _context.ModuleOperations.Where(x => !x.IsDelete && x.ModuleId == moduleId).ToListAsync();
    }

    public async Task AddOperationsToModule(int moduleId, IEnumerable<int> operations)
    {
        foreach (var moduleOperation in operations.Select(operation => new ModuleOperation
                 {
                     ModuleId = moduleId,
                     OperationId = operation
                 }))
            await _context.ModuleOperations.AddAsync(moduleOperation);

        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveOperationsFromModule(int moduleId, IEnumerable<int> operations)
    {
        foreach (var moduleOperation in operations.Select(operation => new ModuleOperation
                 {
                     ModuleId = moduleId,
                     OperationId = operation
                 }))
            _context.ModuleOperations.Remove(moduleOperation);

        await _context.SaveChangesAsync();
    }
}