using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface ISystemConfigService : IGenericService<SystemConfig>
{
    Task<SystemConfig?> GetFirstAsync();
}