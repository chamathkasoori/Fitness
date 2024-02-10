using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class SupplierRepository: GenericRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(FitnessContext context) : base(context)
    {
    }
}