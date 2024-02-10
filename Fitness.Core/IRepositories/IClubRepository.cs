using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IClubRepository : IGenericRepository<Club>
{
    public Task<IReadOnlyList<Club>> GetAllAsync(int page, int pageSize, string searchText, string gender);

    public Task<List<Club>> GetAllDetailsAsync();

    public Task<Club?> GetByNoAsync(int no);
}