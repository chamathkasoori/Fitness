using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ClubService : IClubService
{
    private readonly IClubRepository _clubRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    public ClubService(IClubRepository cityRepository, IWarehouseRepository warehouseRepository)
    {
        _clubRepository = cityRepository;
        _warehouseRepository = warehouseRepository;
    }

    async Task<IReadOnlyList<Club>> IGenericService<Club>.GetAllAsync()
    {
        return await _clubRepository.GetAllAsync();
    }

    public async Task<IReadOnlyList<Club>> GetAllAsync(int page, int pageSize, string searchText, string gender)
    {
        return await _clubRepository.GetAllAsync(page, pageSize, searchText, gender);
    }

    public async Task<List<Club>> GetAllDetailsAsync()
    {
        return await _clubRepository.GetAllDetailsAsync();
    }

    async Task<Club?> IGenericService<Club>.GetByIdAsync(int id)
    {
        return await _clubRepository.GetByIdAsync(id);
    }

    public async Task<Club?> GetByNoAsync(int no)
    {
        return await _clubRepository.GetByNoAsync(no);
    }

    async Task IGenericService<Club>.AddAsync(Club entity)
    {
        await _clubRepository.AddAsync(entity);

        await _warehouseRepository.AddAsync(new Warehouse
        {
            Club = entity,
            IsMain = true,
            Name = $"Warehouse - {entity.Name}"
        });
    }

    async Task IGenericService<Club>.UpdateAsync(Club entity)
    {
        await _clubRepository.UpdateAsync(entity);
    }


}