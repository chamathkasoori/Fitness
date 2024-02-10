using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IProductRepository : IGenericRepository<Product>
{
    public Task<List<Product>> Search(string name, int categoryId, int clubId, int applicationId, bool showDeleted);
}
