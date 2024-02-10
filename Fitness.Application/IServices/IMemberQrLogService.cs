using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMemberQrLogService : IGenericService<MemberQrLog>
{
    public Task<bool> GetQrValidityAsync(Guid uniqueIdentifier,int clubId, long expirationDuration);
}