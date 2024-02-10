using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;
public class MemberOtpRepository : IMemberOtpRepository
{
    private readonly FitnessContext _context;
    private readonly ISmsNotificationRepository _smsNotificationRepository;
    public MemberOtpRepository(FitnessContext context, ISmsNotificationRepository smsNotificationRepository)
    {
        _context = context;
        _smsNotificationRepository = smsNotificationRepository;
    }

    public async Task<MemberOtp?> GetByMemberIdAsync(int memberId)
    {
        return await _context.MemberOtps.FirstOrDefaultAsync(x => x.MemberId == memberId);
    }

    public async Task<MemberOtp?> GetByMobileNoAsync(string mobileNo)
    {
        return await _context.MemberOtps.FirstOrDefaultAsync(x => x.Member.User.MobileNo == mobileNo);
    }

    public async Task<bool> Save(MemberOtp model, string mobileNo, string language, bool isDefaultOTP)
    {
        model.ExpiresOn = DateTime.UtcNow.AddMinutes(5);
        if (_context.MemberOtps.Any(x => x.MemberId == model.MemberId))
        {
            var existing = await _context.MemberOtps.FirstOrDefaultAsync(x => x.MemberId == model.MemberId);
            existing!.OTP = model.OTP;
            existing!.ExpiresOn = model.ExpiresOn;
            existing!.ModifiedBy = model.MemberId;
            existing!.ModifiedOn = DateTime.UtcNow;
            _context.Update(existing!);
            await _context.SaveChangesAsync();
        }
        else
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        if (isDefaultOTP)
        {
            return true;
        }
        else
        {
            return await SendSMS(mobileNo, model.OTP, language);
        }
    }

    private async Task<bool> SendSMS(string mobileNo, int otp, String language)
    {
        SmsNotification smsOtp = new SmsNotification();
        smsOtp.Msg = otp.ToString();
        smsOtp.UserSender = "9round";
        smsOtp.Numbers = language == "Ar"
            ? "Your OTP is : " + mobileNo.ToString()
            : mobileNo.ToString() + ": كلمة المرور الخاصة بك هي";


        var sendResult = await _smsNotificationRepository.SendSmsNotification(smsOtp);
        if (sendResult == "Success")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}