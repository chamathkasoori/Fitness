using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
{
    public ApplicationRepository(FitnessContext context) : base(context)
    {
    }
}
