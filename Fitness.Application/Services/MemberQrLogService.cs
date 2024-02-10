using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberQrLogService : IMemberQrLogService
{
    private readonly IMemberQrLogRepository _memberQrLogRepository;
    public MemberQrLogService(IMemberQrLogRepository MemberQrLogRepository)
    {
        _memberQrLogRepository = MemberQrLogRepository;
    }

    public Task AddAsync(MemberQrLog entity)
    {
        return _memberQrLogRepository.AddAsync(entity);
    }

    public Task<IReadOnlyList<MemberQrLog>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<MemberQrLog?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetQrValidityAsync(Guid uniqueIdentifier, int clubId, long expirationDuration)
    {
        return _memberQrLogRepository.GetQrValidityAsync(uniqueIdentifier,clubId, expirationDuration);
    }

    public Task UpdateAsync(MemberQrLog entity)
    {
        throw new NotImplementedException();
    }
}