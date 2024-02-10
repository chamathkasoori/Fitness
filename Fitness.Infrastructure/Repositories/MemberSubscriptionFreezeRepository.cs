using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberSubscriptionFreezeRepository : GenericRepository<MemberSubscriptionFreeze>, IMemberSubscriptionFreezeRepository
{
    private readonly FitnessContext _context;
    public MemberSubscriptionFreezeRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<MemberSubscriptionFreeze?> IGenericRepository<MemberSubscriptionFreeze>.GetByIdAsync(int id)
    {
        return await _context.MemberSubscriptionFreezes
            .Include(x => x.MemberSubscription)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<MemberSubscriptionFreeze>> GetAllByMemberSubscriptionAsync(int memberSubscriptionId)
    {
        return await _context.MemberSubscriptionFreezes
            .Where(x => !x.IsDelete && x.MemberSubscriptionId == memberSubscriptionId)
            .OrderBy(x => x.StartDate)
            .ToListAsync();
    }
}