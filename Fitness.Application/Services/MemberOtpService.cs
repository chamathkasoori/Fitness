using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class MemberOtpService : IMemberOtpService
{
    private readonly IMemberOtpRepository _memberOtpRepository;
    public MemberOtpService(IMemberOtpRepository memberOtpRepository)
    {
        _memberOtpRepository = memberOtpRepository;
    }

    public async Task<MemberOtp?> GetByMemberIdAsync(int memberId)
    {
        return await _memberOtpRepository.GetByMemberIdAsync(memberId);
    }

    public async Task<MemberOtp?> GetByMobileNoAsync(string mobileNo)
    {
        return await _memberOtpRepository.GetByMobileNoAsync(mobileNo);
    }

    public async Task<bool> RequestOtpAsync(int memberId, string mobileNo, string language, bool isDefaultOTP)
    {
        MemberOtp memberOtp = new MemberOtp();
        memberOtp.MemberId = memberId;
        memberOtp.OTP = isDefaultOTP ? 1234 : GenerateOTP();
        return await _memberOtpRepository.Save(memberOtp, mobileNo, language, isDefaultOTP);
    }

    private int GenerateOTP()
    {
        Random random = new Random();
        int otp;
        do
        {
            otp = random.Next(1000, 9999);
        } while (otp % 100 == 0);
        return otp;
    }
}