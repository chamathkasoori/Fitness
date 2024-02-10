using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    public DepartmentService(IDepartmentRepository DepartmentRepository)
    {
        _departmentRepository = DepartmentRepository;
    }

    public async Task<IReadOnlyList<Department>> GetAllAsync()
    {
        return await _departmentRepository.GetAllAsync();
    }

    Task<Department?> IGenericService<Department>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<Department>.AddAsync(Department entity)
    {
        await _departmentRepository.AddAsync(entity);
    }

    async Task IGenericService<Department>.UpdateAsync(Department entity)
    {
        await _departmentRepository.UpdateAsync(entity);
    }
}