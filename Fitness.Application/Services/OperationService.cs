using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class OperationService : IOperationService
{
    private IOperationRepository _operationRepository;
    public OperationService(IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<IReadOnlyList<Operation>> GetAllAsync()
    {
        return await _operationRepository.GetAllAsync();
    }

    Task<Operation?> IGenericService<Operation>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<Operation>.AddAsync(Operation entity)
    {
        await _operationRepository.AddAsync(entity);
    }

    async Task IGenericService<Operation>.UpdateAsync(Operation entity)
    {
        await _operationRepository.UpdateAsync(entity);
    } 
}