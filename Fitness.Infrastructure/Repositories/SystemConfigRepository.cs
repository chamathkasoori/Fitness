using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class SystemConfigRepository : GenericRepository<SystemConfig>, ISystemConfigRepository
{
    private readonly FitnessContext _context;
    
    public SystemConfigRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<SystemConfig?> GetFirstAsync()
    {
        return await _context.SystemConfigs.AsNoTracking().FirstOrDefaultAsync();
    }
}