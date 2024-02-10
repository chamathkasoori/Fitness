using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class MemberSubscriptionTransferRepository : GenericRepository<MemberSubscriptionTransfer>, IMemberSubscriptionTransferRepository
{
    private readonly FitnessContext _context;
    public MemberSubscriptionTransferRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
}
