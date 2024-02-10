using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ProductAvailableClubService : IProductAvailableClubService
{
    private readonly IProductAvailableClubRepository _productAvailableClubRepository;
    public ProductAvailableClubService(IProductAvailableClubRepository productAvailableClubRepository)
    {
        _productAvailableClubRepository = productAvailableClubRepository;
    }

    public async Task<IReadOnlyList<ProductAvailableClub>> GetAllAsync()
    {
        return await _productAvailableClubRepository.GetAllAsync();
    }

    Task<ProductAvailableClub?> IGenericService<ProductAvailableClub>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<ProductAvailableClub>.AddAsync(ProductAvailableClub entity)
    {
        await _productAvailableClubRepository.AddAsync(entity);
    }

    async Task IGenericService<ProductAvailableClub>.UpdateAsync(ProductAvailableClub entity)
    {
        await _productAvailableClubRepository.UpdateAsync(entity);
    }
}