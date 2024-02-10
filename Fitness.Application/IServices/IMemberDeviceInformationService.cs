using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMemberDeviceInformationService : IGenericService<MemberDeviceInformation>
{
    Task<List<MemberDeviceInformation>> GetAllByMemberAsync(int memberId);

    public Task<MemberDeviceInformation?> GetByMemberAndDeviceModelAsync(int memberId, string deviceModel);
}