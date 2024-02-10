using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class ClubOpeningHourService : IClubOpeningHourService
{
    private readonly IClubOpeningHourRepository _clubOpeningHourRepository;

    public ClubOpeningHourService(IClubOpeningHourRepository ClubOpeningHourRepository)
    {
        _clubOpeningHourRepository = ClubOpeningHourRepository;
    }

    public Task<IReadOnlyList<ClubOpeningHour>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<ClubOpeningHour>> GetAllByClubAsync(int clubId)
    {
        var items = await _clubOpeningHourRepository.GetAllByClubAsync(clubId);
        return items.Where(x => x.IsActive).ToList();
    }

    public async Task<List<ClubOpeningHour>> GetAllForTodayAsync()
    {
        var items = await _clubOpeningHourRepository.GetAllForTodayAsync();
        return items.Where(x => x.IsActive).ToList();
    }

    Task<ClubOpeningHour?> IGenericService<ClubOpeningHour>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<ClubOpeningHour>.AddAsync(ClubOpeningHour entity)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(List<ClubOpeningHour> items)
    {
        int clubId = items.First().ClubId;
        int userId = items.First().CreatedBy!.Value;
        List<ClubOpeningHour> exsitingItems = await _clubOpeningHourRepository.GetAllByClubAsync(clubId);
        foreach (var exsitingItem in exsitingItems)
        {
            if (items.Any(x => x.DayOfWeek == exsitingItem.DayOfWeek))
            {
                var item = items.Where(x => x.DayOfWeek == exsitingItem.DayOfWeek).FirstOrDefault()!;
                exsitingItem.StartTime = item.StartTime;
                exsitingItem.StartTime = item.EndTime;
                exsitingItem.IsActive = true;
                exsitingItem.ModifiedBy = userId;
                exsitingItem.ModifiedOn = DateTime.UtcNow;
                items.Remove(item);
            }
            else
            {
                exsitingItem.IsActive = false;
                exsitingItem.ModifiedBy = userId;
                exsitingItem.ModifiedOn = DateTime.UtcNow;
            }
        }
        foreach (var item in items)
        {
            exsitingItems.Add(item);
        }
        await _clubOpeningHourRepository.SaveAsync(exsitingItems);
    }

    Task IGenericService<ClubOpeningHour>.UpdateAsync(ClubOpeningHour entity)
    {
        throw new NotImplementedException();
    }
}