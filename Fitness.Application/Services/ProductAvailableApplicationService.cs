using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ProductAvailableApplicationService : IProductAvailableApplicationService
{
    private readonly IProductAvailableApplicationRepository _productAvailableApplicationRepository;
    public ProductAvailableApplicationService(IProductAvailableApplicationRepository productAvailableApplicationRepository)
    {
        _productAvailableApplicationRepository = productAvailableApplicationRepository;
    }

    public async Task<IReadOnlyList<ProductAvailableApplication>> GetAllAsync()
    {
        return await _productAvailableApplicationRepository.GetAllAsync();
    }

    Task<ProductAvailableApplication?> IGenericService<ProductAvailableApplication>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<ProductAvailableApplication>.AddAsync(ProductAvailableApplication entity)
    {
        await _productAvailableApplicationRepository.AddAsync(entity);
    }

    async Task IGenericService<ProductAvailableApplication>.UpdateAsync(ProductAvailableApplication entity)
    {
        await _productAvailableApplicationRepository.UpdateAsync(entity);
    }
}