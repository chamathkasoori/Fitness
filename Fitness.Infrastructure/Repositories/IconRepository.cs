using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class IconRepository : GenericRepository<Icon>, IIconRepository
{
    public IconRepository(FitnessContext context) : base(context)
    {
    }
}
