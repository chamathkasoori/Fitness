using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class ProductPriceAuditRepository : GenericRepository<ProductPriceAudit>, IProductPriceAuditRepository
{
    public ProductPriceAuditRepository(FitnessContext context) : base(context)
    {
    }
}