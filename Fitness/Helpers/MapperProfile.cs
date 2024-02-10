using AutoMapper;
using Fitness.Core.Common;
using Fitness.Core.Common.Xero;
using Fitness.Core.Common.Xero.Contact;
using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Dtos;
using Fitness.Dtos.Invoice;

namespace Fitness.Helpers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();

        CreateMap<ModuleDto, Module>().ReverseMap();
        CreateMap<Module, ModuleDetailsDto>();
        CreateMap<ModulePostDto, Module>();
        CreateMap<ModuleOperation, OperationDto>()
            .ForMember(target => target.Id, act => act.MapFrom(src => src.OperationId))
            .ForMember(target => target.Name, act => act.MapFrom(src => src.Operation.Name));
        CreateMap<ModuleOperationPost, ModuleOperation>();

        CreateMap<OperationDto, Operation>().ReverseMap();
        CreateMap<OperationPostDto, Operation>();

        CreateMap<Role, RoleDto>()
            .ForMember(target => target.Company, act => act.MapFrom(src => src.Company.Name));
        CreateMap<Role, AuthenticationRoleDto>()
           .ForMember(target => target.Company, act => act.MapFrom(src => src.Company.Name));
        CreateMap<Role, AuthRoleDto>();
        CreateMap<RolePostDto, Role>();
        CreateMap<RoleModuleOperation, RoleModuleOperationDto>();

        CreateMap<Operation, OperationDto>().ReverseMap();
        CreateMap<ModuleOperation, ModuleOperationDto>().ReverseMap();

        CreateMap<RoleModuleDto, RoleModule>().ReverseMap();
        CreateMap<RoleModule, RoleModuleDetailsDto>();
        CreateMap<RoleModulePostDto, RoleModule>();

        CreateMap<CountryDto, Country>().ReverseMap();
        CreateMap<CityDto, City>().ReverseMap();
        CreateMap<Region, RegionDto>().ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        CreateMap<RegionPostDto, Region>();

        CreateMap<PositionDto, Position>().ReverseMap();
        CreateMap<PositionPostDto, Position>();

        CreateMap<DepartmentDto, Department>();
        CreateMap<Department, DepartmentDto>().ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
        CreateMap<DepartmentPostDto, Department>();

        CreateMap<CompanyDto, Company>().ReverseMap();
        CreateMap<CompanyPostDto, Company>();

        CreateMap<Club, ClubDto>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Address.City.Name))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Address.CountryId))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Address.CityId))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Address.RegionId))
            .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.Address.StateId))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.Address.TimeZone)); ;
        CreateMap<Address, ClubDto>()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId))
            .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.TimeZone));
        CreateMap<Club, ClubDetailsDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));
        CreateMap<ClubPostDto, Club>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.ClubNumber, opt => opt.MapFrom(src => src.ClubNumber))
            .ForPath(dest => dest.Address.CountryId, opt => opt.MapFrom(src => src.CountryId))
            .ForPath(dest => dest.Address.CityId, opt => opt.MapFrom(src => src.CityId))
            .ForPath(dest => dest.Address.RegionId, opt => opt.MapFrom(src => src.RegionId))
            .ForPath(dest => dest.Address.ZipCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
            .ForPath(dest => dest.Address.StateId, opt => opt.MapFrom(src => src.StateId))
            .ForPath(dest => dest.Address.ZipCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForPath(dest => dest.Address.TimeZone, opt => opt.MapFrom(src => src.TimeZone));
        CreateMap<ClubPostDto, Address>()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.StateId));
        CreateMap<ClubOpeningHour, ClubOpeningHourDto>().ReverseMap();
        CreateMap<ClubOpeningHour, ClubOpeningHourDetailDto>()
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek.ToString()))
            .ForMember(dest => dest.OpenFrom, opt => opt.MapFrom(src => "PT" + src.StartTime.Hours + "H"))
            .ForMember(dest => dest.OpenUntil, opt => opt.MapFrom(src => "PT" + src.EndTime.Hours + "H"))
            .ForMember(dest => dest.TimeFrom, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.TimeUntil, opt => opt.MapFrom(src => src.EndTime));

        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.User.Address!.Country.Id))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.User.Address!.Region.Id))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.User.Address!.City.Id))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.User.Address!.Street))
            .ForMember(dest => dest.PostCode, opt => opt.MapFrom(src => src.User.Address!.ZipCode))
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.User.MobileNo))
            .ForMember(dest => dest.HomePhone, opt => opt.MapFrom(src => src.User.MobileNo1))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.User.Dob))
            .ForMember(dest => dest.AssignedClubIds, opt => opt.MapFrom(src => src.EmployeeAssignedClubs.Select(x => x.ClubId)))
            .ForMember(dest => dest.AvailableClubIds, opt => opt.MapFrom(src => src.EmployeeAvailableClubs.Select(x => x.ClubId)));

        CreateMap<EmployeePostDto, Employee>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.PositionId, opt => opt.MapFrom(src => src.PositionId))
            .ForMember(dest => dest.EmploymentDate, opt => opt.MapFrom(src => src.EmploymentDate))
            .ForMember(dest => dest.NoLogin, opt => opt.MapFrom(src => src.NoLogin))
            .ForMember(dest => dest.ChangePassword, opt => opt.MapFrom(src => src.ChangePassword));

        CreateMap<EmployeePostDto, User>()
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        CreateMap<EmployeePostDto, Address>()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.PostCode))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.StateId, opt => opt.Ignore());

        CreateMap<User, EmployeeDto>()
          .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob))
          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
          .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

        CreateMap<ProductCategory, ProductCategoryDto>()
            .ForMember(dest => dest.IconName, opt => opt.MapFrom(src => src.Icon.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
        CreateMap<ProductCategoryDto, ProductCategory>();
        CreateMap<ProductCategoryPostDto, ProductCategory>();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductPostDto, Product>();
        CreateMap<ProductImagePostDto, ProductImage>();
        CreateMap<ProductAvailableClubPostDto, ProductAvailableClub>();
        CreateMap<ProductAvailableApplicationPostDto, ProductAvailableApplication>();
        CreateMap<StockDetail, StockDetailDto>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Quantity! * src.Price!));
        CreateMap<Stock, StockDetailDto>();
        CreateMap<StockDetail, Stock>();
        CreateMap<StockPostDto, Stock>();
        CreateMap<Stock, StockDto>();

        CreateMap<PaymentIntervalDto, PaymentInterval>().ReverseMap();
        CreateMap<PaymentIntervalPostDto, PaymentInterval>();

        CreateMap<PaymentMethod, PaymentMethodDto>();

        CreateMap<ApplicationDto, Core.Entities.Application>().ReverseMap();
        CreateMap<ApplicationPostDto, Core.Entities.Application>();

        CreateMap<VatDto, Vat>().ReverseMap();
        CreateMap<VatPostDto, Vat>();

        CreateMap<PushNotificationDto, PushNotification>().ReverseMap();
        CreateMap<PushNotificationPostDto, PushNotification>();

        CreateMap<SmsNotificationDto, SmsNotification>().ReverseMap();
        CreateMap<SmsNotificationPostDto, SmsNotification>();

        CreateMap<WarehouseDto, Warehouse>().ReverseMap();
        CreateMap<WarehousePostDto, Warehouse>();

        CreateMap<IconDto, Icon>().ReverseMap();
        CreateMap<IconPostDto, Icon>();

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Supplier, SupplierDetailDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
        CreateMap<SupplierPostDto, Supplier>();
        CreateMap<Tag, TagDto>().ReverseMap();

        CreateMap<AccessRule, AccessRuleDto>().ReverseMap();
        CreateMap<AccessRuleItem, AccessRuleItemDto>().ReverseMap();
        CreateMap<AccessRuleItemTiming, AccessRuleItemTimingDto>().ReverseMap();
        CreateMap<AccessRuleItemAssignedClub, AccessRuleItemAssignedClubDto>().ReverseMap();
        CreateMap<AccessRulePostDto, AccessRule>();
        CreateMap<AccessRuleItemPostDto, AccessRuleItem>();

        CreateMap<SubscriptionPlanSetting, SubscriptionPlanSettingDto>()
            .ForMember(dest => dest.DropdownItems, opt => opt.MapFrom(src => TextToList(src.DropdownValues)));

        CreateMap<SubscriptionPlanSubscriptionPlanSetting, SubscriptionPlanSubscriptionPlanSettingDto>().ReverseMap();

        CreateMap<SubscriptionPlan, SubscriptionPlanDto>()
            .ForMember(dest => dest.PaymentIntervalName, opt => opt.MapFrom(src => src.PaymentInterval.Name))
            .ForMember(dest => dest.CommitmentPeriodName, opt => opt.MapFrom(src => src.CommitmentPeriod.Name))
            .ForMember(dest => dest.AccessRuleName, opt => opt.MapFrom(src => src.AccessRule.Name));

        CreateMap<SubscriptionPlan, SubscriptionPlanDetailsDto>()
            .ForMember(dest => dest.SubscriptionPlanSubscriptionPlanSettings, opt => opt.MapFrom(src => src.SubscriptionPlanAssignedSettings));

        CreateMap<SubscriptionPlan, SubscriptionPlanMobileDto>()
            .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PlanNameAr, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DescriptionAr, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.PaymentInterval.Value))
            .ForMember(dest => dest.PlanPrice, opt => opt.MapFrom(src => src.MembershipFee))
            .ForMember(dest => dest.RegistrationFee, opt => opt.MapFrom(src => src.AdministrationFee))
            .ForMember(dest => dest.FreezeDays, opt => opt.MapFrom(src => GetFreezeDays(src.SubscriptionPlanSubscriptionPlanAddons)));

        CreateMap<SubscriptionPlanPostDto, SubscriptionPlan>()
            .ForMember(dest => dest.SubscriptionPlanAssignedSettings, opt => opt.MapFrom(src => src.SubscriptionPlanSubscriptionPlanSettings));

        CreateMap<SubscriptionPlanPaymentMethod, SubscriptionPlanPaymentMethodDto>().ReverseMap();
        CreateMap<SubscriptionPlanRole, SubscriptionPlanRoleDto>().ReverseMap();
        CreateMap<SubscriptionPlanApplication, SubscriptionPlanApplicationDto>().ReverseMap();
        CreateMap<SubscriptionPlanSubscriptionPlanAddon, SubscriptionPlanSubscriptionPlanAddonDto>().ReverseMap();
        CreateMap<SubscriptionPlanAssignedClub, SubscriptionPlanClubDto>().ReverseMap();
        CreateMap<SubscriptionPlanAvailableClub, SubscriptionPlanClubDto>().ReverseMap();
        CreateMap<SubscriptionPlanTag, SubscriptionPlanTagDto>().ReverseMap();
        CreateMap<SubscriptionPlanDiscountPostDto, SubscriptionPlanDiscount>();

        CreateMap<SubscriptionPlanDiscount, SubscriptionPlanDiscountDto>()
            .ForMember(dest => dest.SubscriptionPlanIds, opt => opt.MapFrom(src => src.SubscriptionPlanDiscountSubscriptionPlans.Where(x => x.IsActive).Select(x => x.SubscriptionPlanId)))
            .ForMember(dest => dest.ClubIds, opt => opt.MapFrom(src => src.SubscriptionPlanDiscountClubs.Where(x => x.IsActive).Select(x => x.ClubId)))
            .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.SubscriptionPlanDiscountRoles.Where(x => x.IsActive).Select(x => x.RoleId)))
            .ForMember(dest => dest.ApplicationIds, opt => opt.MapFrom(src => src.SubscriptionPlanDiscountApplications.Where(x => x.IsActive).Select(x => x.ApplicationId)));

        CreateMap<SubscriptionPlanAddonPostDto, SubscriptionPlanAddon>();

        CreateMap<SubscriptionPlanAddon, SubscriptionPlanAddonDto>()
            .ForMember(dest => dest.ClubIds, opt => opt.MapFrom(src => src.SubscriptionPlanAddonClubs.Select(x => x.ClubId)));

        CreateMap<Member, MemberDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.User.MobileNo))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.User.Dob))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.User.Address!.CountryId))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.User.Address!.CityId))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.User.Address!.City.Name))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.User.Address!.RegionId))
            .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.User.Address!.Region.Name))
            .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.User.Address!.StateId))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.User.Address!.ZipCode))
            .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.User.Address!.FullAddress))
            .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.Name))
            .ForMember(dest => dest.MemberNo, opt => opt.MapFrom(src => src.MemberNo));

        CreateMap<MemberOtpPostDto, MemberOtp>();
        CreateMap<MemberSessionRating, MemberSessionRatingDto>().ReverseMap();
        CreateMap<MemberSessionRatingPostDto, MemberSessionRating>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.AddedDate));
        CreateMap<MobileUsageLogDto, MobileUsageLog>().ReverseMap();
        CreateMap<MobileUsageLogPostDto, MobileUsageLog>();
        CreateMap<MemberSubscription, MemberSubscriptionDto>()
            .ForMember(dest => dest.AccessRuleName, opt => opt.MapFrom(src => src.SubscriptionPlan.AccessRule.Name))
            .ForMember(dest => dest.SubscriptionPlanName, opt => opt.MapFrom(src => src.SubscriptionPlan.Name))
            .ForMember(dest => dest.PaymentIntervalName, opt => opt.MapFrom(src => src.SubscriptionPlan.CommitmentPeriod.Name))
            .ForMember(dest => dest.CommitmentPeriodName, opt => opt.MapFrom(src => src.SubscriptionPlan.CommitmentPeriod.Name))
            .ForMember(dest => dest.CommitmentEndDate, opt => opt.MapFrom(src => CommitmentEndDate(src.StartDate, src.SubscriptionPlan.CommitmentPeriod)))
            .ForMember(dest => dest.PaymentInteralMonths, opt => opt.MapFrom(src => src.SubscriptionPlan.PaymentInterval.Value))
            .ForMember(dest => dest.MemberSubscriptionAddonList, opt => opt.MapFrom(src => src.MemberSubscriptionAddons))
            .ForMember(dest => dest.FreezeDays, opt => opt.MapFrom(src => GetFreezeDays(src.MemberSubscriptionAddons)))
            .ForMember(dest => dest.AvailableFreezeDays, opt => opt.MapFrom(src => GetAvailableFreezeDays(src.MemberSubscriptionAddons, src.MemberSubscriptionFreezes)));
        CreateMap<MemberSubscriptionTransactionPostDto, MemberSubscriptionTransaction>();
        CreateMap<MemberSubscriptionTransaction, MemberSubscriptionTransactionDto>();
        CreateMap<MemberSubscriptionAddon, MemberSubscriptionAddonDto>()
            .ForMember(dest => dest.MemberSubscriptionAddonId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SubscriptionPlanAddonId, opt => opt.MapFrom(src => src.SubscriptionPlanAddonId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SubscriptionPlanAddon.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.SubscriptionPlanAddon.Type))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.SubscriptionPlanAddon.Quantity))
            .ForMember(dest => dest.UsageLimitPerPlan, opt => opt.MapFrom(src => src.SubscriptionPlanAddon.UsageLimitPerPlan));
        CreateMap<MemberSubscriptionFreezeRequest, MemberSubscriptionFreeze>();
        CreateMap<MemberSubscriptionFreeze, MemberSubscriptionFreezeDto>();
        CreateMap<MemberVisit, MemberVisitDto>()
            .ForMember(dest => dest.ClubName, opt => opt.MapFrom(src => src.Club.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Club.Address));

        CreateMap<MemberDeviceInformationDetailDto, MemberDeviceInformation>();
        CreateMap<MemberDeviceInformation, MemberDeviceInformationDetailDto>();
        CreateMap<MemberDeviceInformationPostDto, MemberDeviceInformation>();
        CreateMap<MemberDeviceInformation, MemberDeviceInformationPostDto>();
        CreateMap<MemberDeviceInformationPutDto, MemberDeviceInformation>();
        CreateMap<MemberDeviceInformation, MemberDeviceInformationPutDto>();

        CreateMap<TagPostDto, Tag>();
        CreateMap<Tag, TagPostDto>();

        CreateMap<Inquiry, InquiryDto>();
        CreateMap<InquiryDto, Inquiry>();
        CreateMap<InquiryPostDto, Inquiry>();
        CreateMap<InquiryPutDto, Inquiry>();
        CreateMap<Inquiry, InquiryDetailsDto>()
            .ForMember(dest => dest.MemberNo, opt => opt.MapFrom(src => src.Member.MemberNo))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Member.User.FirstName + " " + src.Member.User.LastName));
        CreateMap<InquiryReply, InquiryReplyDto>()
            .ForMember(dest => dest.RepliedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(dest => dest.ReplyFrom, opt => opt.MapFrom(src => src.ReplyFrom.FirstName + " " + src.ReplyFrom.LastName));
        CreateMap<InquiryReplyPostDto, InquiryReply>();

        CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.DefaultQuestion));
        CreateMap<QuestionMultiLanguage, QuestionDto>()
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.QuestionText))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.QuestionId));
        CreateMap<AnswerPost, QuestionAnswer>();
        CreateMap<QuestionAnswer, QuestionAnswerDto>();

        CreateMap<XeroResponseContactDto, XeroContactDto>();
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src =>
                    $"{src.MemberSubscription.Member.User.FirstName} {src.MemberSubscription.Member.User.LastName}"))
            .ForMember(dest => dest.Club, opt => opt.MapFrom(src => src.MemberSubscription.Member.Club.Name));
        CreateMap<InvoiceResponseDto, Invoice>();

        CreateMap<MemberQrLogPostDto, MemberQrLog>();
    }

    private static List<string> TextToList(string words)
    {
        return string.IsNullOrWhiteSpace(words) ? new List<string>() : words.Split(",").ToList();
    }

    private static int GetFreezeDays(ICollection<SubscriptionPlanSubscriptionPlanAddon> items)
    {
        if (items.Any(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze))
        {
            var item = items.Where(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze).Select(x => x.SubscriptionPlanAddon).FirstOrDefault();
            return item!.Quantity;
        }
        return 0;
    }

    private static int GetFreezeDays(ICollection<MemberSubscriptionAddon> items)
    {
        if (items.Any(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze))
        {
            var item = items.Where(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze).Select(x => x.SubscriptionPlanAddon).FirstOrDefault();
            return item!.Quantity;
        }
        return 0;
    }

    private static int GetAvailableFreezeDays(ICollection<MemberSubscriptionAddon> addons, ICollection<MemberSubscriptionFreeze> freezes)
    {
        int freezeDays = 0;
        if (addons.Any(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze))
        {
            var item = addons.Where(x => x.SubscriptionPlanAddon.Type == SubscriptionPlanAddonType.Freeze).Select(x => x.SubscriptionPlanAddon).FirstOrDefault();
            freezeDays = item!.Quantity;
        }

        int usedDays = 0;
        foreach (var memberSubscriptionFreeze in freezes)
        {
            usedDays = usedDays + (memberSubscriptionFreeze.EndDate - memberSubscriptionFreeze.StartDate).Days + 1;
        }

        return freezeDays - usedDays > 0 ? freezeDays - usedDays : 0;
    }

    private static DateTime CommitmentEndDate(DateTime startDate, PaymentInterval commitmentPeriod)
    {
        if (commitmentPeriod.Type == PaymentType.Day)
        {
            return startDate.AddDays(commitmentPeriod.Value);
        }
        else if (commitmentPeriod.Type == PaymentType.Week)
        {
            return startDate.AddDays(commitmentPeriod.Value * 7).AddDays(-1);
        }
        else if (commitmentPeriod.Type == PaymentType.Month)
        {
            return startDate.AddMonths(commitmentPeriod.Value).AddDays(-1);
        }
        else if (commitmentPeriod.Type == PaymentType.Year)
        {
            return startDate.AddYears(commitmentPeriod.Value).AddDays(-1);
        }
        else
        {
            return startDate;
        }
    }
}