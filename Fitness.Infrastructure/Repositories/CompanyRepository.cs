using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(FitnessContext context) : base(context)
    {
    }
}