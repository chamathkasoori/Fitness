using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMemberSessionRatingService : IGenericService<MemberSessionRating>
{
    public Task<List<MemberSessionRating>> GetAllByVisitsAsync(List<int> visitIds);
}