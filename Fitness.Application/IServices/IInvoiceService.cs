using Fitness.Core.Entities;
using Fitness.Core.Enums;

namespace Fitness.Application.IServices;
public interface IInvoiceService : IGenericService<Invoice>
{
    Task<Invoice?> GetByMemberSubscriptionIdAsync(int memberSubscriptionId);
    
    Task SendMail(int id, MailTypes mailType, Invoice? invoice, MemberSubscription memberSubscription);
}