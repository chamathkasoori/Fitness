using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class ClubOpeningHourRepository : GenericRepository<ClubOpeningHour>, IClubOpeningHourRepository
{
    private readonly FitnessContext _context;
    public ClubOpeningHourRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ClubOpeningHour>> GetAllByClubAsync(int clubId)
    {
        return await _context.ClubOpeningHours
            .Where(x => !x.IsDelete && x.ClubId == clubId)
            .ToListAsync();
    }

    public async Task<List<ClubOpeningHour>> GetAllForTodayAsync()
    {
        int dayOfWeek = (int)DateTime.UtcNow.DayOfWeek;
        return await _context.ClubOpeningHours
            .Where(x => (int)x.DayOfWeek == dayOfWeek)
            .ToListAsync();
    }

    public async Task SaveAsync(List<ClubOpeningHour> items)
    {
        if (items.Any(x => x.Id == 0))
        {
            _context.AddRange(items.Where(x => x.Id == 0));
        }
        if (items.Any(x => x.Id > 0))
        {
            _context.UpdateRange(items.Where(x => x.Id > 0));
        }
        await _context.SaveChangesAsync();
    }
}