using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    private readonly FitnessContext _context;
    public AddressRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
}
