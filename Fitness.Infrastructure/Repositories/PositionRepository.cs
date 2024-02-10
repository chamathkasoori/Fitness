using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class PositionRepository : GenericRepository<Position>, IPositionRepository
{
    public PositionRepository(FitnessContext context) : base(context)
    {
    }
}