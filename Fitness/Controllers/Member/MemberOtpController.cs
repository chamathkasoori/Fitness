// MOVED TO AuthenticationController

using Fitness.Core.Constants;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Fitness.Controllers;
public class MemberOtpController : CommonController<MemberOtpController>
{
    private readonly IConfiguration configuration;
    private readonly IMemberOtpService memberOtpService;
    private readonly IMemberService memberService;
    public MemberOtpController(IConfiguration configuration, IMemberOtpService memberOtpService, IMemberService memberService)
    {
        this.configuration = configuration;
        this.memberOtpService = memberOtpService;
        this.memberService = memberService;
    }

    [AllowAnonymous]
    [HttpPost("Request")]
    public async Task<IActionResult> RequestOTP([FromBody] string mobileNo)
    {
        MemberOtpDto response = new MemberOtpDto();
        var member = await memberService.GetByMobileNo(mobileNo);
        if (member.Id == 0)
        {
            response.Message = "No Member Found For Given Mobile No";
            return BadRequest(response);
        }

        bool isDefaultOTP = bool.Parse(configuration["IsDefaultOTP"] ?? "false");
        string testMobileNo = configuration["TestMobileNo"] ?? "";
        if (member.User.MobileNo == testMobileNo)
        {
            isDefaultOTP = true;
        }
        response.MemberId = member.Id;
        response.ClubId = member.ClubId;
        response.Status = await memberOtpService.RequestOtpAsync(member.Id, mobileNo, member.PreferredLanguage!, isDefaultOTP);
        if (response.Status)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.otpSentSuccessfully, member.PreferredLanguage!);
            response.Message = configuration.GetSection(messageKey).Value ?? "";
            return Ok(response);
        }
        else
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.failedToSendSMS, member.PreferredLanguage!);
            response.Message = configuration.GetSection(messageKey).Value ?? "";
            return BadRequest(response);
        }
    }

    [AllowAnonymous]
    [HttpPost("Resend")]
    public async Task<IActionResult> ResendOTP([FromBody] string mobileNo)
    {
        MemberOtpDto response = new MemberOtpDto();
        var member = await memberService.GetByMobileNo(mobileNo);
        if (member.Id == 0)
        {
            response.Message = "No Member Found For Given Mobile No";
            return BadRequest(response);
        }

        bool isDefaultOTP = bool.Parse(configuration["IsDefaultOTP"] ?? "false"); // If True NO SMS, OTP is set to Default
        response.MemberId = member.Id;
        response.ClubId = member.ClubId;
        response.Status = await memberOtpService.RequestOtpAsync(member.Id, mobileNo, member.PreferredLanguage!, isDefaultOTP);
        if (response.Status)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.otpSentSuccessfully, member.PreferredLanguage!);
            response.Message = configuration.GetSection(messageKey).Value ?? "";
            return Ok(response);
        }
        else
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.failedToSendSMS, member.PreferredLanguage!);
            response.Message = configuration.GetSection(messageKey).Value ?? "";
            return BadRequest(response);
        }
    }

    [AllowAnonymous]
    [HttpPost("Validate")]
    public async Task<IActionResult> Validate(MemberOtpPostDto model)
    {
        var member = await memberService.GetByMobileNo(model.MobileNo);
        if (member.Id == 0)
        {
            return BadRequest("No Member Found For Given Mobile No");
        }

        var memberOtp = await memberOtpService.GetByMemberIdAsync(member.Id);
        if (memberOtp == null)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.otpNotFound, member.PreferredLanguage!);
            return BadRequest(configuration.GetSection(messageKey).Value ?? "OTP Not Found");
        }

        if (memberOtp!.OTP != model.OTP)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.invalidOTP, member.PreferredLanguage!);
            return BadRequest(configuration.GetSection(messageKey).Value ?? "Invalid OTP");
        }
        if (memberOtp!.ExpiresOn < DateTime.UtcNow)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.otpExpired, member.PreferredLanguage!);
            return BadRequest(configuration.GetSection(messageKey).Value ?? "OTP Expired");
        }

        return Ok(member.Id);
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate(MemberOtpPostDto model)
    {
        var member = await memberService.GetByMobileNo(model.MobileNo);
        if (member.Id == 0)
        {
            return BadRequest("No Member Found For Given Mobile No");
        }

        var memberOtp = await memberOtpService.GetByMemberIdAsync(member.Id);
        if (memberOtp == null)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.otpNotFound, member.PreferredLanguage!);
            return BadRequest(configuration.GetSection(messageKey).Value ?? "OTP Not Found");
        }

        if (memberOtp!.OTP != model.OTP)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.invalidOTP, member.PreferredLanguage!);
            return BadRequest(configuration.GetSection(messageKey).Value ?? "Invalid OTP");
        }
        if (memberOtp!.ExpiresOn < DateTime.UtcNow)
        {
            var messageKey = AppSettingsMessages.getMessageByLanguage(AppSettingsMessages.otpExpired, member.PreferredLanguage!);
            return BadRequest(configuration.GetSection(messageKey).Value ?? "OTP Expired");
        }

        var applicationId = configuration["MobileApplicationId"] ?? "0";
        List<Claim> claims = new List<Claim>
        {
            new Claim(type: "UserId", value: member.UserId.ToString()),
            new Claim(type: "MemberId", value: member.Id.ToString()),
            new Claim(type: "ClubIds", value: member.ClubId.ToString()),
            new Claim(type: "ApplicationId", value: applicationId),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken
        (
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(int.Parse(configuration["Jwt:DurationInHours"]!)),
            signingCredentials: credentials
        );
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            memberId = member.Id
        });
    }
}