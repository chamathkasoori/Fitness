using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly FitnessContext _context;
    public ProductRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    async Task<IReadOnlyList<Product>> IGenericRepository<Product>.GetAllAsync()
    {
        return await _context.Products.Include(x => x.Category).Include(x => x.Vat).Where(x => x.IsDelete == false).ToListAsync();
    }

    public async Task<List<Product>> Search(string name, int categoryId, int clubId, int applicationId, bool showDeleted)
    {
        return await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Vat)
            .Where(x => !x.IsDelete 
                && (name == "" || x.Name.Contains(name))
                && (categoryId == 0 || x.CategoryId == categoryId)
                && (clubId == 0 || x.ProductAvailableClubs.Any(y => y.ClubId == clubId))
                && (applicationId == 0 || x.ProductAvailableApplications.Any(y => y.ApplicationId == applicationId))
                && (x.IsDelete == false || x.IsDelete == showDeleted))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}
