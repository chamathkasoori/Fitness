using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Fitness.Application.Services;
using Fitness.Application.IServices;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Dtos;
using Fitness.Helpers;
using Fitness.Infrastructure.Data;
using Fitness.Infrastructure.Repositories;
using Fitness.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Fitness.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FitnessContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "SaaS Portal", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"] ?? string.Empty))
    };
});

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<FitnessContext>().AddDefaultTokenProviders();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MapperProfile()); });
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton(new Random());

builder.Services.AddScoped<FitnessContext, FitnessContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IClubOpeningHourRepository, ClubOpeningHourRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleModuleRepository, RoleModuleRepository>();
builder.Services.AddScoped<IRoleModuleOperationRepository, RoleModuleOperationRepository>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IModuleOperationRepository, ModuleOperationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeAvailableClubRepository, EmployeeAvailableClubRepository>();
builder.Services.AddScoped<IEmployeeAssignedClubRepository, EmployeeAssignedClubRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IPaymentIntervalRepository, PaymentIntervalRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IVatRepository, VatRepository>();
builder.Services.AddScoped<IPushNotificationRepository, PushNotificationRepository>();
builder.Services.AddScoped<ISmsNotificationRepository, SmsNotificationRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IIconRepository, IconRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductAvailableApplicationRepository, ProductAvailableApplicationRepository>();
builder.Services.AddScoped<IProductAvailableClubRepository, ProductAvailableClubRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductPriceAuditRepository, ProductPriceAuditRepository>();
builder.Services.AddScoped<IMobileUsageLogRepository, MobileUsageLogRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberOtpRepository, MemberOtpRepository>();
builder.Services.AddScoped<IMemberSubscriptionRepository, MemberSubscriptionRepository>();
builder.Services.AddScoped<IMemberSubscriptionTransactionRepository, MemberSubscriptionTransactionRepository>();
builder.Services.AddScoped<IMemberSubscriptionFreezeRepository, MemberSubscriptionFreezeRepository>();
builder.Services.AddScoped<IMemberSubscriptionTransferRepository, MemberSubscriptionTransferRepository>();
builder.Services.AddScoped<IMemberVisitRepository, MemberVisitRepository>();
builder.Services.AddScoped<IMemberSessionRatingRepository, MemberSessionRatingRepository>();
builder.Services.AddScoped<IMemberQrLogRepository, MemberQrLogRepository>();
builder.Services.AddScoped<IMemberDeviceInformationRepository, MemberDeviceInformationRepository>();
builder.Services.AddScoped<IMemberTransactionExternalReferenceRepository, MemberTransactionExternalReferenceRepository>();
builder.Services.AddScoped<ISubscriptionPlanSettingRepository, SubscriptionPlanSettingRepository>();
builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<ISubscriptionPlanDiscountRepository, SubscriptionPlanDiscountRepository>();
builder.Services.AddScoped<ISubscriptionPlanDiscountCombinationRepository, SubscriptionPlanDiscountCombinationRepository>();
builder.Services.AddScoped<ISubscriptionPlanAddonRepository, SubscriptionPlanAddonRepository>();
builder.Services.AddScoped<ISubscriptionPlanAssignedClubRepository, SubscriptionPlanAssignedClubRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IAccessRuleRepository, AccessRuleRepository>();
builder.Services.AddScoped<IAccessRuleItemRepository, AccessRuleItemRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IInquiryRepository, InquiryRepository>();
builder.Services.AddScoped<IInquiryReplyRepository, InquiryReplyRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionMultiLanguageRepository, QuestionMultiLanguageRepository>();
builder.Services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
builder.Services.AddScoped<IQuestionCommentRepository, QuestionCommentRepository>();
builder.Services.AddScoped<ISystemConfigRepository, SystemConfigRepository>();

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IClubOpeningHourService, ClubOpeningHourService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IModuleOperationService, ModuleOperationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleModuleService, RoleModuleService>();
builder.Services.AddScoped<IRoleModuleOperationService, RoleModuleOperationService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IPaymentIntervalService, PaymentIntervalService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IVatService, VatService>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
builder.Services.AddScoped<ISmsNotificationService, SmsNotificationService>();
builder.Services.AddScoped<IMailManagerService, MailManagerService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IIconService, IconService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductAvailableApplicationService, ProductAvailableApplicationService>();
builder.Services.AddScoped<IProductAvailableClubService, ProductAvailableClubService>();
builder.Services.AddScoped<IProductPriceAuditService, ProductPriceAuditService>();
builder.Services.AddScoped<IMobileUsageLogService, MobileUsageLogService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();
builder.Services.AddScoped<IInquiryReplyService, InquiryReplyService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberOtpService, MemberOtpService>();
builder.Services.AddScoped<IMemberSubscriptionService, MemberSubscriptionService>();
builder.Services.AddScoped<IMemberSubscriptionTransactionService, MemberSubscriptionTransactionService>();
builder.Services.AddScoped<IMemberSubscriptionFreezeService, MemberSubscriptionFreezeService>();
builder.Services.AddScoped<IMemberSubscriptionTransferService, MemberSubscriptionTransferService>();
builder.Services.AddScoped<IMemberVisitService, MemberVisitService>();
builder.Services.AddScoped<IMemberSessionRatingService, MembseSessionRatingService>();
builder.Services.AddScoped<IMemberQrLogService, MemberQrLogService>();
builder.Services.AddScoped<IMemberDeviceInformationService, MemberDeviceInformationService>();
builder.Services.AddScoped<IMemberTransactionExternalReferenceService, MemberTransactionExternalReferenceService>();
builder.Services.AddScoped<ISubscriptionPlanSettingService, SubscriptionPlanSettingService>();
builder.Services.AddScoped<ISubscriptionPlanService, SubscriptionPlanService>();
builder.Services.AddScoped<ISubscriptionPlanDiscountService, SubscriptionPlanDiscountService>();
builder.Services.AddScoped<ISubscriptionPlanDiscountCombinationService, SubscriptionPlanDiscountCombinationService>();
builder.Services.AddScoped<ISubscriptionPlanAddonService, SubscriptionPlanAddonService>();
builder.Services.AddScoped<ISubscriptionPlanAssignedClubService, SubscriptionPlanAssignedClubService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IAccessRuleService, AccessRuleService>();
builder.Services.AddScoped<IAccessRuleItemService, AccessRuleItemService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionMultiLanguageService, QuestionMultiLanguageService>();
builder.Services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
builder.Services.AddScoped<IQuestionCommentService, QuestionCommentService>();
builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();
builder.Services.AddScoped<IXeroService, XeroService>();
builder.Services.AddHostedService<MemberSubscriptionHostedService>();
builder.Services.AddScoped<IZendeskService, ZendeskService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    if (token != null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)

            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            AccessDto access = new AccessDto();
            access.UserId = jwtToken.Claims.Any(x => x.Type == "UserId") ? int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value) : 0;
            access.EmployeeId = jwtToken.Claims.Any(x => x.Type == "EmployeeId") ? int.Parse(jwtToken.Claims.First(x => x.Type == "EmployeeId").Value) : 0;
            access.ApplicationId = jwtToken.Claims.Any(x => x.Type == "ApplicationId") ? int.Parse(jwtToken.Claims.First(x => x.Type == "ApplicationId").Value) : 0;
            access.MemberId = jwtToken.Claims.Any(x => x.Type == "MemberId") ? int.Parse(jwtToken.Claims.First(x => x.Type == "MemberId").Value) : 0;
            access.RoleId = jwtToken.Claims.Any(x => x.Type == "RoleId") ? int.Parse(jwtToken.Claims.First(x => x.Type == "RoleId").Value) : 0;
            access.CompanyId = jwtToken.Claims.Any(x => x.Type == "CompanyId") ? int.Parse(jwtToken.Claims.First(x => x.Type == "CompanyId").Value) : 0;
            access.DepartmentId = jwtToken.Claims.Any(x => x.Type == "DepartmentId") ? int.Parse(jwtToken.Claims.First(x => x.Type == "DepartmentId").Value) : 0;
            if (jwtToken.Claims.Any(x => x.Type == "ClubIds"))
            { 
                string clubIds = jwtToken.Claims.First(x => x.Type == "ClubIds").Value;
                if(!string.IsNullOrEmpty(clubIds))
                foreach (string clubId in clubIds.Split(','))
                {
                    access.ClubIdList.Add(int.Parse(clubId));
                }
                
            }
            context.Items["Access"] = access;
        }
        catch (Exception)
        {
        }
    }
    await next(context);
});

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();