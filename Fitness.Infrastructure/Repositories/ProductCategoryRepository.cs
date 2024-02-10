using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
{
    private readonly FitnessContext _context;
    public ProductCategoryRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<IReadOnlyList<ProductCategory>> IGenericRepository<ProductCategory>.GetAllAsync()
    {
        return await _context.ProductCategories
            .Include(x => x.Icon)
            .Include(x => x.Department)
            .Include(x => x.Company)
            .Where(x=> !x.IsDelete)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}