using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Task<Invoice?> GetByMemberSubscriptionIdAsync(int memberSubscriptionId);
}