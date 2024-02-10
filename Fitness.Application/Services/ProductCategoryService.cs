using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<IReadOnlyList<ProductCategory>> GetAllAsync()
    {
        return await _productCategoryRepository.GetAllAsync();
    }

    async Task<ProductCategory?> IGenericService<ProductCategory>.GetByIdAsync(int id)
    {
        return await _productCategoryRepository.GetByIdAsync(id);
    }

    async Task IGenericService<ProductCategory>.AddAsync(ProductCategory entity)
    {
        await _productCategoryRepository.AddAsync(entity);
    }

    async Task IGenericService<ProductCategory>.UpdateAsync(ProductCategory entity)
    {
        await _productCategoryRepository.UpdateAsync(entity);
    }
}