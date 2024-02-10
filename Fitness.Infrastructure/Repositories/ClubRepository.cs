using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class ClubRepository : GenericRepository<Club>, IClubRepository
{
    private readonly FitnessContext _context;
    public ClubRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Club>> GetAllAsync(int page, int pageSize, string searchText, string gender)
    {
        if (page == 0) page = 1;
        if (pageSize == 0) pageSize = int.MaxValue;
        int skip = (page - 1) * pageSize;

        return await _context.Clubs
            .Include(x => x.Address).ThenInclude(x => x.City)
            .Where(x => !x.IsDelete
                && (searchText == "" || x.Name.Contains(searchText) || x.ShortName!.Contains(searchText) || x.Address!.City!.Name.Contains(searchText))
                && (gender == "" || x.Gender == gender))
            .OrderByDescending(x => x.Id)
            .Skip(skip).Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Club>> GetAllDetailsAsync()
    {
        return await _context.Clubs
            .Include(x => x.Address).ThenInclude(x => x.Country)
            .Include(x => x.Address).ThenInclude(x => x.City)
            .Where(x => !x.IsDelete)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    async Task<Club?> IGenericRepository<Club>.GetByIdAsync(int id)
    {
        return await _context.Clubs
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Club?> GetByNoAsync(int no)
    {
        return await _context.Clubs
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.ClubNumber == no && !x.IsDelete);
    }
}