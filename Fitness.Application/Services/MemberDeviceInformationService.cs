using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberDeviceInformationService : IMemberDeviceInformationService
{
    private readonly IMemberDeviceInformationRepository _memberDeviceInformationRepository;
    public MemberDeviceInformationService(IMemberDeviceInformationRepository deviceInformationRepository)
    {
        _memberDeviceInformationRepository = deviceInformationRepository;
    }

    public Task<IReadOnlyList<MemberDeviceInformation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<MemberDeviceInformation>> GetAllByMemberAsync(int memberId)
    {
        return await _memberDeviceInformationRepository.GetAllByMemberAsync(memberId);
    }

    public async Task<MemberDeviceInformation?> GetByMemberAndDeviceModelAsync(int memberId, string deviceModel)
    {
        return await _memberDeviceInformationRepository.GetByMemberAndDeviceModelAsync(memberId, deviceModel);
    }

    public Task<MemberDeviceInformation?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(MemberDeviceInformation model)
    {
        await _memberDeviceInformationRepository.AddAsync(model);
    }

    public async Task UpdateAsync(MemberDeviceInformation model)
    {
        await _memberDeviceInformationRepository.UpdateAsync(model);
    }
}