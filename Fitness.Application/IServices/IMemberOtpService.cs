using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IMemberOtpService
{
    public Task<MemberOtp?> GetByMemberIdAsync(int memberId);

    public Task<MemberOtp?> GetByMobileNoAsync(string mobileNo);

    public Task<bool> RequestOtpAsync(int memberId, string mobileNo, string language, bool isDefaultOTP);
}