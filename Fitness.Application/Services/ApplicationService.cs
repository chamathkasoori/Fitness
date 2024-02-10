using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    public ApplicationService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<IReadOnlyList<Core.Entities.Application>> GetAllAsync()
    {
        return await _applicationRepository.GetAllAsync();
    }

    async Task<Core.Entities.Application?> IGenericService<Core.Entities.Application>.GetByIdAsync(int id)
    {
        return await _applicationRepository.GetByIdAsync(id);
    }

    async Task IGenericService<Core.Entities.Application>.AddAsync(Core.Entities.Application entity)
    {
        await _applicationRepository.AddAsync(entity);
    }

    async Task IGenericService<Core.Entities.Application>.UpdateAsync(Core.Entities.Application entity)
    {
        await _applicationRepository.UpdateAsync(entity);
    }
}
