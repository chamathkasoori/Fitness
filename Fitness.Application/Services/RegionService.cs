using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class RegionService : IRegionService
{
    private readonly IRegionRepository _regionRepository;
    public RegionService(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    async Task<IReadOnlyList<Region>> IGenericService<Region>.GetAllAsync()
    {
        return await _regionRepository.GetAllAsync();
    }

    async Task<Region?> IGenericService<Region>.GetByIdAsync(int id)
    {
        return await _regionRepository.GetByIdAsync(id);
    }

    async Task IGenericService<Region>.AddAsync(Region entity)
    {
        await _regionRepository.AddAsync(entity);
    }

    async Task IGenericService<Region>.UpdateAsync(Region entity)
    {
        await _regionRepository.UpdateAsync(entity);
    }
}