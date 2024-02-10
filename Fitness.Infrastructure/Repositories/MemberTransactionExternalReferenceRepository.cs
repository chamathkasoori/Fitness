using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class MemberTransactionExternalReferenceRepository : GenericRepository<MemberTransactionExternalReference>, IMemberTransactionExternalReferenceRepository
{
    private readonly FitnessContext _context;
    public MemberTransactionExternalReferenceRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
}