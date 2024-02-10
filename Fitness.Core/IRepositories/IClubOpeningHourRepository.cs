using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IClubOpeningHourRepository : IGenericRepository<ClubOpeningHour>
{
    public Task<List<ClubOpeningHour>> GetAllByClubAsync(int clubId);

    public Task<List<ClubOpeningHour>> GetAllForTodayAsync();

    public Task SaveAsync(List<ClubOpeningHour> items);
}