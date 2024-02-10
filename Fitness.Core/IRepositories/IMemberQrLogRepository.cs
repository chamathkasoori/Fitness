using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;

public interface IMemberQrLogRepository : IGenericRepository<MemberQrLog>
{
    public Task<bool> GetQrValidityAsync(Guid uniqueIdentifier, int clubId, long expirationDuration);
}
