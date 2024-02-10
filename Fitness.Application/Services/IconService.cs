using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class IconService : IIconService
{
    private readonly IIconRepository _iconRepository;
    public IconService(IIconRepository iconRepository)
    {
        _iconRepository = iconRepository;
    }

    public async Task<IReadOnlyList<Icon>> GetAllAsync()
    {
        return await _iconRepository.GetAllAsync();
    }

    public async Task<Icon?> GetByIdAsync(int id)
    {
        return await _iconRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Icon entity)
    {
        await _iconRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Icon entity)
    {
        await _iconRepository.UpdateAsync(entity);
    }
}
