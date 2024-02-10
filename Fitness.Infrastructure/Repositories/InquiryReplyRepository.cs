using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class InquiryReplyRepository : GenericRepository<InquiryReply>, IInquiryReplyRepository
{
    private readonly FitnessContext _context;
    public InquiryReplyRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<InquiryReply?> GetByInquiryIdAsync(int id)
    {
        return await _context.InquiryReplies.FirstOrDefaultAsync(x => x.InquiryId == id);
    }
}