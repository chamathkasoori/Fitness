using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;
    public PositionService(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IReadOnlyList<Position>> GetAllAsync()
    {
        return await _positionRepository.GetAllAsync();
    }

    Task<Position?> IGenericService<Position>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Position entity)
    {
        await _positionRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Position entity)
    {
        await _positionRepository.UpdateAsync(entity);
    }
}