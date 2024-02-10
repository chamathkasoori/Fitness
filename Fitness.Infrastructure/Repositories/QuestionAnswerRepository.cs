using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class QuestionAnswerRepository : GenericRepository<QuestionAnswer>, IQuestionAnswerRepository
{
    private readonly FitnessContext _context;
    public QuestionAnswerRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<IReadOnlyList<QuestionAnswer>> GetAllByMemberAsync(int memberId)
    {
        return await _context.QuestionAnswers.Where(x => (memberId == 0 || x.MemberId == memberId)).ToListAsync();
    }

    public async Task<IReadOnlyList<Member>> GetAllDistinctMembersAsync()
    {
        return await _context.QuestionAnswers
            .Include(q => q.Member).ThenInclude(m => m.User)
            .Select(q => q.Member).Distinct()
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task SaveAsync(IEnumerable<QuestionAnswer> list)
    {
        await _context.QuestionAnswers.AddRangeAsync(list);
        await _context.SaveChangesAsync();
    }    
}