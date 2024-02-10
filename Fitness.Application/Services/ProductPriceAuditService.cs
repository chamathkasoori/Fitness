using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ProductPriceAuditService : IProductPriceAuditService
{
    private readonly IProductPriceAuditRepository _productPriceAuditRepository;
    public ProductPriceAuditService(IProductPriceAuditRepository productPriceAuditRepository)
    {
        _productPriceAuditRepository = productPriceAuditRepository;
    }

    public async Task<IReadOnlyList<ProductPriceAudit>> GetAllAsync()
    {
        return await _productPriceAuditRepository.GetAllAsync();
    }

    Task<ProductPriceAudit?> IGenericService<ProductPriceAudit>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<ProductPriceAudit>.AddAsync(ProductPriceAudit entity)
    {
        await _productPriceAuditRepository.AddAsync(entity);
    }

    Task IGenericService<ProductPriceAudit>.UpdateAsync(ProductPriceAudit entity)
    {
        throw new NotImplementedException();
    }
}