using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IInquiryReplyRepository : IGenericRepository<InquiryReply>
{
    Task<InquiryReply?> GetByInquiryIdAsync(int id);
}