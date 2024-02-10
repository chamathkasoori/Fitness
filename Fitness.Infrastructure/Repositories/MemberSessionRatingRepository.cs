using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberSessionRatingRepository : GenericRepository<MemberSessionRating>, IMemberSessionRatingRepository
{
    private readonly FitnessContext _context;
    public MemberSessionRatingRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<MemberSessionRating>> GetAllByVisitsAsync(List<int> visitIds)
    {
        return await _context.MemberSessionRatings
            .Where(x => !x.IsDelete && visitIds.Contains(x.VisitId))
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }
}