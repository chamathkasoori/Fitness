using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class ProductAvailableClubRepository : GenericRepository<ProductAvailableClub>, IProductAvailableClubRepository
{
    public ProductAvailableClubRepository(FitnessContext context) : base(context)
    {
    }
}