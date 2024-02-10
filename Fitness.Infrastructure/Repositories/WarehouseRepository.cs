using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class WarehouseRepository: GenericRepository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(FitnessContext context) : base(context)
    {
    }
}
