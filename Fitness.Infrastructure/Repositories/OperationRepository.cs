using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class OperationRepository : GenericRepository<Operation>, IOperationRepository
{
    public OperationRepository(FitnessContext context) : base(context)
    {
    }
}