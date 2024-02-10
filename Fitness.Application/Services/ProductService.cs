using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<List<Product>> Search(string name, int categoryId, int clubId, int applicationId, bool showDeleted)
    {
        return await _productRepository.Search(name, categoryId, clubId, applicationId, showDeleted);
    }

    async Task<Product?> IGenericService<Product>.GetByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    async Task IGenericService<Product>.AddAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    async Task IGenericService<Product>.UpdateAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }
}
