using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;
    public ProductImageService(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }
    Task<IReadOnlyList<ProductImage>> IGenericService<ProductImage>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<ProductImage?> IGenericService<ProductImage>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<ProductImage>.AddAsync(ProductImage entity)
    {
        await _productImageRepository.AddAsync(entity);
    }

    async Task IGenericService<ProductImage>.UpdateAsync(ProductImage entity)
    {
        await _productImageRepository.UpdateAsync(entity);
    }
}
