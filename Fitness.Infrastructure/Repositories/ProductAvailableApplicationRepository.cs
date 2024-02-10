using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class ProductAvailableApplicationRepository : GenericRepository<ProductAvailableApplication>, IProductAvailableApplicationRepository
{
    public ProductAvailableApplicationRepository(FitnessContext context) : base(context)
    {
    }
}