using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IInquiryReplyService : IGenericService<InquiryReply>
{
    Task<InquiryReply?> GetByInquiryIdAsync(int id);
}