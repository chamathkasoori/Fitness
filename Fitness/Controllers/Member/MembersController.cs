using AutoMapper;
using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fitness.Core.Enums;

namespace Fitness.Controllers;
public class MembersController : CommonController<MembersController>
{
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMemberService memberService;
    private readonly IUserService userService;
    private readonly ISubscriptionPlanService subscriptionPlanService;
    private readonly ISubscriptionPlanDiscountService subscriptionPlanDiscountService;
    private readonly IMemberSubscriptionService memberSubscriptionService;
    private readonly IQuestionCommentService questionCommentService;
    private readonly IQuestionAnswerService questionAnswerService;
    public MembersController
    (
        IMapper mapper,
        IConfiguration configuration,
        IMemberService memberService,
        IUserService userService,
        ISubscriptionPlanService subscriptionPlanService,
        ISubscriptionPlanDiscountService subscriptionPlanDiscountService,
        IMemberSubscriptionService memberSubscriptionService,
        IQuestionCommentService questionCommentService,
        IQuestionAnswerService questionAnswerService
    )
    {
        this.mapper = mapper;
        this.configuration = configuration;
        this.memberService = memberService;
        this.userService = userService;
        this.subscriptionPlanService = subscriptionPlanService;
        this.subscriptionPlanDiscountService = subscriptionPlanDiscountService;
        this.memberSubscriptionService = memberSubscriptionService;
        this.questionCommentService = questionCommentService;
        this.questionAnswerService = questionAnswerService;
    }

    [HttpGet]
    [HttpGet("page/{page:int}/size/{size:int}")]
    public async Task<IActionResult> GetAll(int page = 1, int size = 100, string searchText = "")
    {
        var items = await memberService.GetAllAsync(page, size, searchText);
        return Ok(mapper.Map<IReadOnlyList<MemberDto>>(items));
    }

    [HttpGet("report/type/{memberType :bool}/page/{page:int}/size/{size:int}")]
    public async Task<IActionResult> GetAllByMemberType(int page = 1, int size = 100, bool memberType = false, DateTime? startDate = null, DateTime? endDate = null)
    {
        var items = await memberService.GetAllByMemberTypeAsync(page, size, memberType, startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue);
        return Ok(mapper.Map<IReadOnlyList<MemberDto>>(items));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await memberService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Member Not Found");
        }
        return Ok(mapper.Map<MemberDto>(item));
    }

    [AllowAnonymous]
    [HttpPost("validate")]
    public async Task<IActionResult> Validate(MemberValidateDto model)
    {
        string message = await ValidateMember(model.MemberId, model.UserId, model.MobileNo, model.Email, model.PersonalIdentificationNumber);
        if (!string.IsNullOrEmpty(message))
        {
            return BadRequest(message);
        }
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(MemberRegisterPostDto model)
    {
        string message = await ValidateMember(0, 0, model.MobileNo, model.Email, model.PersonalIdentificationNumber);
        if (!string.IsNullOrEmpty(message))
        {
            return BadRequest(message);
        }

        #region Member
        Address address = new Address();
        address.FullAddress = model.FullAddress;
        address.CountryId = model.CountryId;
        address.CityId = model.CityId;
        address.ZipCode = model.ZipCode;
        address.StateId = model.StateId;

        User user = new User();
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.MobileNo = model.MobileNo;
        user.Email = model.Email;
        user.Gender = model.Gender;
        user.Dob = model.Dob;
        user.UserName = model.Email;
        user.Type = UserType.Member;
        user.CreatedBy = user.Id;
        user.Address = address;
        user.Address.CreatedBy = user.Id;

        Member member = new Member();
        member.ClubId = model.ClubId;
        member.UserId = user.Id;
        member.PreferredLanguage = model.PreferredLanguage;
        member.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
        member.CreatedBy = user.Id;
        member.ApplicationId = int.Parse(configuration["MobileApplicationId"] ?? "0");
        member.User = user;
        member.IsGuest = true;

        await memberService.AddAsync(member);
        #endregion Member

        #region QuestionAnswer
        if (!string.IsNullOrEmpty(model.QuestionAnswer.Comment))
        {
            var questionComment = new QuestionComment();
            questionComment.MemberId = member.Id;
            questionComment.Comment = model.QuestionAnswer.Comment;
            questionComment.CreatedBy = member.UserId;

            await questionCommentService.AddAsync(questionComment);
        }

        var questionAnswerList = mapper.Map<List<QuestionAnswer>>(model.QuestionAnswer.Answers);
        foreach (var item in questionAnswerList)
        {
            item.MemberId = member.Id;
            item.CreatedBy = member.UserId;
        }
        if (questionAnswerList.Any())
        {
            await questionAnswerService.SaveAsync(questionAnswerList);
        }
        #endregion QuestionAnswer

        int freeTrialSubscriptionPlanId = int.Parse(configuration["FreeTrialSubscriptionPlanId"] ?? "0");
        var subscriptionPlan = await subscriptionPlanService.GetByIdAsync(freeTrialSubscriptionPlanId);
        if (subscriptionPlan != null)
        {
            var memberSubscription = new MemberSubscription();
            memberSubscription.CreatedBy = member.UserId;
            memberSubscription.MemberId = member.Id;
            memberSubscription.Status = MemberSubscriptionStatus.Current;
            memberSubscription.SubscriptionPlanId = subscriptionPlan!.Id;
            await memberSubscriptionService.SaveAsync(memberSubscription, member, subscriptionPlan, string.Empty);
        }

        return CreatedAtAction("GetAll", new { id = member.Id }, mapper.Map<MemberDto>(member));
    }

    [HttpPost]
    public async Task<IActionResult> Add(MemberPostDto model)
    {
        string message = await ValidateMember(0, 0, model.MobileNo, model.Email, model.PersonalIdentificationNumber);
        if (!string.IsNullOrEmpty(message))
        {
            return BadRequest(message);
        }
        if (model.CityId == 0)
        {
            return BadRequest("CityId selection required");
        }

        var subscriptionPlan = await subscriptionPlanService.GetByIdAsync(model.SubscriptionPlanId);
        if (!model.IsGuest)
        {
            if (subscriptionPlan == null)
            {
                return BadRequest("Subscription Plan Not Found");
            }
        }

        var access = HttpContext.Items["Access"] as AccessDto;

        Address address = new Address();
        address.FullAddress = model.FullAddress;
        address.CountryId = model.CountryId;
        address.CityId = model.CityId;
        address.ZipCode = model.ZipCode;
        address.StateId = model.StateId;
        address.CreatedBy = access!.UserId;

        User user = new User();
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.MobileNo = model.MobileNo;
        user.Email = model.Email;
        user.Gender = model.Gender;
        user.Dob = model.Dob;
        user.UserName = model.Email;
        user.Type = UserType.Member;
        user.Address = address;
        user.CreatedBy = access!.UserId;

        Member member = new Member();
        member.ClubId = model.ClubId;
        member.UserId = user.Id;
        member.PreferredLanguage = model.PreferredLanguage;
        member.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
        member.ApplicationId = int.Parse(configuration["PortalApplicationId"] ?? "0");
        member.IsGuest = model.IsGuest;
        member.User = user;
        member.CreatedBy = access!.UserId;
        foreach (var tagId in model.TagIds)
        {
            member.MemberTags.Add(new() { Member = member, TagId = tagId });
        }
        await memberService.AddAsync(member);
        member = (await memberService.GetByIdAsync(member.Id))!;

        if (!model.IsGuest)
        {
            MemberSubscription memberSubscription = new MemberSubscription();
            memberSubscription.CreatedBy = access!.UserId;
            memberSubscription.MemberId = member.Id;
            memberSubscription.ApplicationId = access!.ApplicationId;
            memberSubscription.Status = MemberSubscriptionStatus.Current;
            memberSubscription.SubscriptionPlanId = model.SubscriptionPlanId;
            if (model.SubscriptionPlanDiscountId > 0)
            {
                var subscriptionPlanDiscount = await subscriptionPlanDiscountService.GetByIdAsync(model.SubscriptionPlanDiscountId);
                if (subscriptionPlanDiscount != null)
                {
                    MemberSubscriptionDiscount memberSubscriptionDiscount = new MemberSubscriptionDiscount();
                    memberSubscriptionDiscount.SubscriptionPlanDiscount = subscriptionPlanDiscount;
                    memberSubscriptionDiscount.SubscriptionPlanDiscountId = subscriptionPlanDiscount.Id;
                    memberSubscriptionDiscount.CreatedBy = access!.UserId;
                    memberSubscriptionDiscount.CreatedOn = DateTime.UtcNow;
                    memberSubscription.MemberSubscriptionDiscounts.Add(memberSubscriptionDiscount);
                }
            }
            await memberSubscriptionService.SaveAsync(memberSubscription, member, subscriptionPlan!, model.PaymentMethod);
        }
        
        return CreatedAtAction("GetAll", new { id = member.Id }, mapper.Map<MemberDto>(member));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, MemberPostDto model)
    {
        var member = await memberService.GetByIdAsync(id);
        if (member == null)
        {
            return BadRequest("Member Not Found");
        }

        string message = await ValidateMember(member.Id, member.UserId, model.MobileNo, model.Email, model.PersonalIdentificationNumber);
        if (!string.IsNullOrEmpty(message))
        {
            return BadRequest(message);
        }

        var access = HttpContext.Items["Access"] as AccessDto;

        member.ClubId = model.ClubId;
        member.Club = null!;
        member.PreferredLanguage = model.PreferredLanguage;
        member.PersonalIdentificationNumber = model.PersonalIdentificationNumber;
        member.ModifiedBy = access!.UserId;
        member.ModifiedOn = DateTime.UtcNow;

        member.User.FirstName = model.FirstName;
        member.User.LastName = model.LastName;
        member.User.MobileNo = model.MobileNo;
        member.User.Email = model.Email;
        member.User.Gender = model.Gender;
        member.User.Dob = model.Dob;
        member.User.UserName = model.Email;
        member.User.Type = UserType.Member;
        member.User.ModifiedBy = access!.UserId;
        member.User.ModifiedOn = DateTime.UtcNow;

        member.User.Address!.FullAddress = model.FullAddress;
        member.User.Address!.CountryId = model.CountryId;
        member.User.Address!.CityId = model.CityId;
        member.User.Address!.ZipCode = model.ZipCode;
        member.User.Address!.StateId = model.StateId;
        member.User.Address!.ModifiedBy = access!.UserId;
        member.User.Address!.ModifiedOn = DateTime.UtcNow;
        await memberService.UpdateAsync(member);

        return NoContent();
    }

    [HttpPut("{id:int}/profile")]
    public async Task<IActionResult> UpdateProfile(int id, MemberPutDto model)
    {
        var member = await memberService.GetByIdAsync(id);
        if (member == null)
        {
            return BadRequest("Member Not Found");
        }

        string message = await ValidateMember(member.Id, member.UserId, model.MobileNo, model.Email, "");
        if (!string.IsNullOrEmpty(message))
        {
            return BadRequest(message);
        }

        var access = HttpContext.Items["Access"] as AccessDto;

        member.ModifiedBy = access!.UserId;
        member.ModifiedOn = DateTime.UtcNow;

        member.User.FirstName = model.FirstName;
        member.User.LastName = model.LastName;
        member.User.MobileNo = model.MobileNo;
        member.User.Email = model.Email;
        member.User.Gender = model.Gender;
        member.User.Dob = model.Dob;
        member.User.ModifiedBy = access!.UserId;
        member.User.ModifiedOn = DateTime.UtcNow;

        member.User.Address!.FullAddress = model.FullAddress;
        member.User.Address!.CountryId = model.CountryId;
        member.User.Address!.CityId = model.CityId;
        member.User.Address!.ZipCode = model.ZipCode;
        member.User.Address!.RegionId = model.RegionId;
        member.User.Address!.ModifiedBy = access!.UserId;
        member.User.Address!.ModifiedOn = DateTime.UtcNow;
        await memberService.UpdateAsync(member);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await memberService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Member Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await memberService.UpdateAsync(item);
        return NoContent();
    }

    private async Task<string> ValidateMember(int memberId, int userId, string mobileNo, string email, string personalIdentificationNumber)
    {
        if (await userService.IsMobileNoExists(UserType.Member, userId, mobileNo))
        {
            return $"MobileNo '{mobileNo}' Already Exist";
        }

        if (await userService.IsEmailExists(UserType.Member, userId, email))
        {
            return $"Email '{email}' Already Exist";
        }

        if (!string.IsNullOrEmpty(personalIdentificationNumber) && await memberService.IsPersonalIdentificationNumberExists(memberId, personalIdentificationNumber))
        {
            return $"PersonalIdentificationNumber '{personalIdentificationNumber}' Already Exist";
        }
        return "";
    }
}
