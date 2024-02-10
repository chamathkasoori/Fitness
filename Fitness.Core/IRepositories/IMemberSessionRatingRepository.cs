using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberSessionRatingRepository : IGenericRepository<MemberSessionRating>
{
    public Task<List<MemberSessionRating>> GetAllByVisitsAsync(List<int> visitIds);
}