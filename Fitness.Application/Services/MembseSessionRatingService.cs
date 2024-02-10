using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MembseSessionRatingService : IMemberSessionRatingService
{
    private readonly IMemberSessionRatingRepository _membseSessionRatingRepository;

    public MembseSessionRatingService(IMemberSessionRatingRepository membseSessionRatingRepository)
    {
        _membseSessionRatingRepository = membseSessionRatingRepository;
    }

    public async Task<IReadOnlyList<MemberSessionRating>> GetAllAsync()
    {
        return await _membseSessionRatingRepository.GetAllAsync();
    }

    public async Task<List<MemberSessionRating>> GetAllByVisitsAsync(List<int> visitIds)
    {
        return await _membseSessionRatingRepository.GetAllByVisitsAsync(visitIds);
    }

    public Task<MemberSessionRating?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(MemberSessionRating entity)
    {
        await _membseSessionRatingRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(MemberSessionRating entity)
    {
        await _membseSessionRatingRepository.UpdateAsync(entity);
    }
}