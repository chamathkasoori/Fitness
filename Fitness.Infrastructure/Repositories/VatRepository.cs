using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class VatRepository : GenericRepository<Vat>, IVatRepository
{
    public VatRepository(FitnessContext context) : base(context)
    {
    }
}
