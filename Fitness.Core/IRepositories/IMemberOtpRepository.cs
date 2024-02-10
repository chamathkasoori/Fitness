using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface IMemberOtpRepository
{
    public Task<MemberOtp?> GetByMemberIdAsync(int memberId);

    public Task<MemberOtp?> GetByMobileNoAsync(string mobileNo);

    public Task<bool> Save(MemberOtp model, string mobileNo, string language, bool isDefaultOTP);
}