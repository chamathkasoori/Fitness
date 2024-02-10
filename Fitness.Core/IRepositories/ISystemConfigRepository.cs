using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;

public interface ISystemConfigRepository : IGenericRepository<SystemConfig>
{
    Task<SystemConfig?> GetFirstAsync();
}