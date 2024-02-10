using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IClubService : IGenericService<Club>
{
    public Task<IReadOnlyList<Club>> GetAllAsync(int page, int size, string searchText, string gender);

    public Task<List<Club>> GetAllDetailsAsync();

    public Task<Club?> GetByNoAsync(int no);
}