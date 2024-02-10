using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IProductService : IGenericService<Product>
{
    public Task<List<Product>> Search(string name, int categoryId, int clubId, int applicationId, bool showDeleted);
}
