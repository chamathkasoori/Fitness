using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Fitness.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Fitness.Core.Constants;

namespace Fitness.Controllers.Core;
public class AuthenticationController : CommonController<AuthenticationController>
{
    private readonly SignInManager<User> signInManager;
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IRoleService roleService;
    private readonly IModuleService moduleService;
    private readonly IRoleModuleService roleModuleService;
    private readonly IEmployeeService employeeService;
    private readonly IMemberOtpService memberOtpService;
    private readonly IMemberService memberService;
    public AuthenticationController
    (
        SignInManager<User> signInManager,
        IConfiguration configuration,
        IUserService userService,
        IRoleService roleService,
        IMapper mapper,
        IModuleService moduleService,
        IRoleModuleService roleModuleService,
        IEmployeeService employeeService,
        IMemberOtpService memberOtpService,
        IMemberService memberService
    )
    {
        this.signInManager = signInManager;
        this.configuration = configuration;
        this.mapper = mapper;
        this.userService = userService;
        this.roleService = roleService;
        this.moduleService = moduleService;
        this.roleModuleService = roleModuleService;
        this.employeeService = employeeService;
        this.memberOtpService = memberOtpService;
        this.memberService = memberService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userService.GetByUsernameAsync(loginDto.Username);
        if (user == null) return NotFound("User Not Found");

        var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (!result.Succeeded) return Unauthorized();

        var employee = await employeeService.GetByUserIdAsync(user.Id);
        if (employee == null) return NotFound("Employee Not Found");

        var role = await roleService.GetByIdAsync(employee.RoleId);
        if (role == null) return NotFound("Roles Not Found");

        List<int> clubIds = await employeeService.GetAssignedClulbIdsAsync(employee.Id);

        List<Claim> claims = new List<Claim>();
        claims = new List<Claim>
        {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, role.Name!),
                new Claim(type: "UserId", value: employee!.UserId.ToString()),
                new Claim(type: "EmployeeId", value: employee!.Id.ToString()),
                new Claim(type: "ClubIds", value: string.Join<int>(",", clubIds)),
                new Claim(type: "RoleId", value: employee!.RoleId.ToString()),
                new Claim(type: "CompanyId", value: employee!.Department!.CompanyId.ToString()),
                new Claim(type: "DepartmentId", value: employee!.Department!.Id.ToString()),
                new Claim(type: "ApplicationId", value: configuration["PortalApplicationId"] ?? "0"),
                new Claim(type: ClaimTypes.GivenName, value: user.FirstName),
                new Claim(type: ClaimTypes.Surname, value: user.LastName),
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

        var allModules = await moduleService.GetAllAsync();
        var roleModules = await roleModuleService.GetAllByRoleId(employee!.RoleId);
        List<int> moduleIds = new List<int>();

        foreach (var roleModule in roleModules.Where(x => !x.IsDelete && x.IsActive))
        {
            var module = allModules.FirstOrDefault(x => x.Id == roleModule.ModuleId);
            if (module != null)
            {
                if (!moduleIds.Any(x => x == module.Id))
                {
                    moduleIds.Add(module.Id);
                }

                if (!string.IsNullOrEmpty(module.Hierarchy))
                {
                    var ids = module.Hierarchy.Split("-");
                    foreach (var id in ids)
                    {
                        if (!moduleIds.Any(x => x == int.Parse(id)))
                        {
                            moduleIds.Add(int.Parse(id));
                        }
                    }
                }
            }
        }
        List<Module> modules = allModules.Where(x => moduleIds.Contains(x.Id)).ToList();
        var authModuleTree = TreeBuilder.AuthModuleTree(modules);
        var response = new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            role = new { role = mapper.Map<AuthenticationRoleDto>(role), modules = authModuleTree },
            changePassword = employee!.ChangePassword,
            firstName = user.FirstName,  // Include first name in the response
            lastName = user.LastName,    // Include last name in the response
        };
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("RequestOTP")]
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
    [HttpPost("ResendOTP")]
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
    [HttpPost("AuthenticateOTP")]
    public async Task<IActionResult> AuthenticateOTP(MemberOtpPostDto model)
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

        var token = GetMobileAppToken(member);
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            memberId = member.Id
        });
    }

    [HttpDelete]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordPostDTO model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var user = await userService.GetByIdAsync(access!.UserId);
        var result = await signInManager.PasswordSignInAsync(user, model.CurrentPassword, false, false);
        if (!result.Succeeded)
        {
            return BadRequest("CurrentPassword Is Incorrect");
        }

        user.ModifiedBy = access!.UserId;
        user.ModifiedOn = DateTime.UtcNow;
        bool isSuccess = await userService.UpdatePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (isSuccess)
        {
            // Is Change Password On Login, we need to reset it
            var emaployee = await employeeService.GetByUserIdAsync(access!.UserId);
            if (emaployee != null && emaployee.ChangePassword!.Value)
            {
                emaployee.ChangePassword = false;
                emaployee.ModifiedBy = access!.UserId;
                emaployee.ModifiedOn = DateTime.UtcNow;
                emaployee.User = null!;
                await employeeService.UpdateAsync(emaployee);
            }
            return Ok();
        }
        else
        {
            return BadRequest("Change Password Failed");
        }
    }

    private JwtSecurityToken GetMobileAppToken(Member member)
    {
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
        return new JwtSecurityToken
        (
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(int.Parse(configuration["Jwt:DurationInHours"]!)),
            signingCredentials: credentials
        );
    }
}