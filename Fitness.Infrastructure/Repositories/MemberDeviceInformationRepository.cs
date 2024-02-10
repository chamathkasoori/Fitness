using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberDeviceInformationRepository : GenericRepository<MemberDeviceInformation>, IMemberDeviceInformationRepository
{
    private readonly FitnessContext _context;
    public MemberDeviceInformationRepository(FitnessContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<MemberDeviceInformation>> GetAllByMemberAsync(int memberId)
    {
        return await _context.MemberDeviceInformations
            .Where(x => !x.IsDelete && x.MemberId == memberId)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<MemberDeviceInformation?> GetByMemberAndDeviceModelAsync(int memberId, string deviceModel)
    {
        return await _context.MemberDeviceInformations.FirstOrDefaultAsync(x => x.MemberId == memberId && x.DeviceModel == deviceModel);
    }
}