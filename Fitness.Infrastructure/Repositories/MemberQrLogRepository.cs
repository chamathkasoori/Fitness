using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberQrLogRepository : GenericRepository<MemberQrLog>, IMemberQrLogRepository
{
    private readonly FitnessContext _context;
    public MemberQrLogRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> GetQrValidityAsync(Guid uniqueIdentifier, int clubId , long expirationDuration)
    {
        DateTime currentTime = DateTime.UtcNow;
        DateTime timeWindowEnd = currentTime;
        DateTime timeWindowStart = currentTime.AddSeconds((expirationDuration * -1));

        var matchingRecord = await _context.MemberQrLogs
             .FirstOrDefaultAsync(item =>
                 item.ClubId == clubId &&
                 item.UniqueIdentifier == uniqueIdentifier &&
                 item.CreatedOn >= timeWindowStart &&
                 item.CreatedOn <= timeWindowEnd);

        return matchingRecord != null;
        
    }
}
