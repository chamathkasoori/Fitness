using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberVisitRepository : GenericRepository<MemberVisit>, IMemberVisitRepository
{
    private readonly FitnessContext _context;
    public MemberVisitRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MemberVisit>> GetAllAsync(int memberId, int clubId, int pageSize)
    {
        if (pageSize == 0) pageSize = int.MaxValue;
        return await _context.MemberVisits
            .Include(x => x.Club)
            .Include(x => x.Club).ThenInclude(x => x.Address)
            .Where(x => !x.IsDelete && x.MemberId == memberId && (clubId == 0 || x.ClubId == clubId))
            .OrderByDescending(x => x.Id)
            .Take(pageSize)
            .ToListAsync();
    }
}