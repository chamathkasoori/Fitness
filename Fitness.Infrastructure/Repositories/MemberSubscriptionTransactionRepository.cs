using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberSubscriptionTransactionRepository : GenericRepository<MemberSubscriptionTransaction>, IMemberSubscriptionTransactionRepository
{
    private readonly FitnessContext _context;
    public MemberSubscriptionTransactionRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<MemberSubscription>> GetAllByMemberAsync(int memberId) 
        => await _context.MemberSubscriptions
            .Include(x => x.Invoices)
            .Include(x => x.MemberSubscriptionTransactions)
            .Where(x => !x.IsDelete && x.MemberId == memberId)
            .ToListAsync();
}
