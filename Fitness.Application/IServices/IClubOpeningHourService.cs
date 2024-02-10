using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IClubOpeningHourService : IGenericService<ClubOpeningHour>
{
    public Task<List<ClubOpeningHour>> GetAllByClubAsync(int clubId);

    public Task<List<ClubOpeningHour>> GetAllForTodayAsync();

    public Task SaveAsync(List<ClubOpeningHour> items);
}