using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberDeviceInformationRepository : IGenericRepository<MemberDeviceInformation>
{
    Task<List<MemberDeviceInformation>> GetAllByMemberAsync(int memberId);

    public Task<MemberDeviceInformation?> GetByMemberAndDeviceModelAsync(int memberId, string deviceModel);
}