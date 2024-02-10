using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MobileUsageLogRepository : GenericRepository<MobileUsageLog>, IMobileUsageLogRepository
{
    private readonly FitnessContext _context;
    public MobileUsageLogRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<MobileUsageLog>> GetByMemberIdAsync(int memberId)
    {
        return await _context.MobileUsageLogs
            .Where(x => !x.IsDelete && x.MemberId == memberId)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}
