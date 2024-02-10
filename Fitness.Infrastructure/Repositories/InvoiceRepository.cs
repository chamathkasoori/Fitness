using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    private readonly FitnessContext _context;
    public InvoiceRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(x => x.MemberSubscription).ThenInclude(x => x.Member).ThenInclude(x => x.User)
            .Include(x => x.MemberSubscription).ThenInclude(x => x.Member).ThenInclude(x => x.Club)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    async Task<Invoice?> IGenericRepository<Invoice>.GetByIdAsync(int id)
    {
        return await _context.Invoices
            .Include(x => x.MemberSubscription).ThenInclude(x => x.Member).ThenInclude(x => x.User)
            .Include(x => x.MemberSubscription).ThenInclude(x => x.SubscriptionPlan).ThenInclude(x => x.MembershipFeeVat)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    //Get by membersubscription id
    public async Task<Invoice?> GetByMemberSubscriptionIdAsync(int memberSubscriptionId)
    {
        return await _context.Invoices
            .Include(x => x.MemberSubscription).ThenInclude(x => x.Member).ThenInclude(x => x.User)
            .Include(x => x.MemberSubscription).ThenInclude(x => x.SubscriptionPlan)
            .FirstOrDefaultAsync(x => x.MemberSubscriptionId == memberSubscriptionId);
    }
}