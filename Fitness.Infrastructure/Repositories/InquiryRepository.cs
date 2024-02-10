using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class InquiryRepository : GenericRepository<Inquiry>, IInquiryRepository
{
    private readonly FitnessContext _context;
    public InquiryRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Inquiry>> GetAllByClubsAsync(List<int> clubIdList, string type)
    {
        return await _context.Inquiries
            .Include(i => i.Member).ThenInclude(m => m.User)
            .Include(i => i.InquiryReply)
            .Where(x => !x.IsDelete && clubIdList.Contains(x.ClubId) && (type == "" || x.Type == type))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Inquiry>> GetAllByMemberAsync(int memberId)
    {
        return await _context.Inquiries
            .Include(i => i.InquiryReply)
            .Where(x => !x.IsDelete && x.MemberId == memberId)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    async Task<Inquiry?> IGenericRepository<Inquiry>.GetByIdAsync(int id)
    {
        return await _context.Inquiries
            .Include(i => i.Member).ThenInclude(m => m.User)
            .Include(i => i.InquiryReply).ThenInclude(m => m.ReplyFrom)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}