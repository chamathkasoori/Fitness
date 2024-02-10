using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    public CompanyService(ICompanyRepository CompanyRepository)
    {
        _companyRepository = CompanyRepository;
    }

    public async Task<IReadOnlyList<Company>> GetAllAsync()
    {
        return await _companyRepository.GetAllAsync();
    }

    Task<Company?> IGenericService<Company>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<Company>.AddAsync(Company entity)
    {
        await _companyRepository.AddAsync(entity);
    }

    async Task IGenericService<Company>.UpdateAsync(Company entity)
    {
        await _companyRepository.UpdateAsync(entity);
    }
}