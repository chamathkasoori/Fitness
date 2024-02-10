using Fitness.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Data;
public partial class FitnessContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public FitnessContext(DbContextOptions<FitnessContext> options) : base(options)
    {
    }

    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
    public virtual DbSet<Module> Modules { get; set; }
    public virtual DbSet<ModuleOperation> ModuleOperations { get; set; }
    public virtual DbSet<Operation> Operations { get; set; }
    public virtual DbSet<RoleModule> RoleModules { get; set; }
    public virtual DbSet<RoleModuleOperation> RoleModuleOperations { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Club> Clubs { get; set; }
    public virtual DbSet<ClubOpeningHour> ClubOpeningHours { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Warehouse> Warehouses { get; set; }
    public virtual DbSet<Icon> Icons { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<EmployeeAssignedClub> EmployeeAssignedClubs { get; set; }
    public virtual DbSet<EmployeeAvailableClub> EmployeeAvailableClubs { get; set; }


    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<Region> Regions { get; set; }
    public virtual DbSet<Application> Applications { get; set; }
    public virtual DbSet<Vat> Vat { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductPriceAudit> ProductPriceAudits { get; set; }
    public virtual DbSet<ProductImage> ProductImages { get; set; }
    public virtual DbSet<ProductAvailableClub> ProductAvailableClubs { get; set; }
    public virtual DbSet<ProductAvailableApplication> ProductAvailableApplications { get; set; }
    public virtual DbSet<Stock> Stocks { get; set; }
    public virtual DbSet<StockMovement> StockMovements { get; set; }

    public virtual DbSet<PaymentInterval> PaymentIntervals { get; set; }
    public virtual DbSet<PaymentPlan> PaymentPlans { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<SubscriptionPlanSetting> SubscriptionPlanSettings { get; set; }
    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
    public virtual DbSet<SubscriptionPlanPaymentMethod> SubscriptionPlanPaymentMethods { get; set; }
    public virtual DbSet<SubscriptionPlanAssignedClub> SubscriptionPlanAssignedClubs { get; set; }
    public virtual DbSet<SubscriptionPlanAvailableClub> SubscriptionPlanAvailableClubs { get; set; }
    public virtual DbSet<SubscriptionPlanTag> SubscriptionPlanTags { get; set; }
    public virtual DbSet<SubscriptionPlanRole> SubscriptionPlanRoles { get; set; }
    public virtual DbSet<SubscriptionPlanApplication> SubscriptionPlanApplications { get; set; }
    public virtual DbSet<SubscriptionPlanDiscount> SubscriptionPlanDiscounts { get; set; }
    public virtual DbSet<SubscriptionPlanDiscountClub> SubscriptionPlanDiscountClubs { get; set; }
    public virtual DbSet<SubscriptionPlanDiscountRole> SubscriptionPlanDiscountRoles { get; set; }
    public virtual DbSet<SubscriptionPlanDiscountApplication> SubscriptionPlanDiscountApplications { get; set; }
    public virtual DbSet<SubscriptionPlanDiscountSubscriptionPlan> SubscriptionPlanDiscountSubscriptionPlans { get; set; }
    public virtual DbSet<SubscriptionPlanDiscountCombination> SubscriptionPlanDiscountCombinations { get; set; }
    public virtual DbSet<SubscriptionPlanAddon> SubscriptionPlanAddons { get; set; }
    public virtual DbSet<SubscriptionPlanAddonClub> SubscriptionPlanAddonClubs { get; set; }
    public virtual DbSet<SubscriptionPlanSubscriptionPlanAddon> SubscriptionPlanSubscriptionPlanAddons { get; set; }

    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<MemberSubscription> MemberSubscriptions { get; set; }
    public virtual DbSet<MemberSubscriptionDiscount> MemberSubscriptionDiscounts { get; set; }
    public virtual DbSet<MemberSubscriptionAddon> MemberSubscriptionAddons { get; set; }
    public virtual DbSet<MemberSubscriptionFreeze> MemberSubscriptionFreezes { get; set; }
    public virtual DbSet<MemberSubscriptionTransaction> MemberSubscriptionTransactions { get; set; }
    public virtual DbSet<MemberSubscriptionTransfer> MemberSubscriptionTransfers { get; set; }
    public virtual DbSet<MemberSessionRating> MemberSessionRatings { get; set; }
    public virtual DbSet<MemberVisit> MemberVisits { get; set; }
    public virtual DbSet<MemberQrLog> MemberQrLogs { get; set; }
    public virtual DbSet<MemberOtp> MemberOtps { get; set; }
    public virtual DbSet<MemberDeviceInformation> MemberDeviceInformations { get; set; }
    public virtual DbSet<MemberTransactionExternalReference> MemberTransactionExternalReferences { get; set; }
    public virtual DbSet<MobileUsageLog> MobileUsageLogs { get; set; }
    public virtual DbSet<AccessRule> AccessRules { get; set; }
    public virtual DbSet<AccessRuleItem> AccessRuleItems { get; set; }
    public virtual DbSet<AccessRuleItemTiming> AccessRuleItemTimings { get; set; }
    public virtual DbSet<AccessRuleItemAssignedClub> AccessRuleItemAssignedClubs { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Inquiry> Inquiries { get; set; }
    public virtual DbSet<InquiryReply> InquiryReplies { get; set; }

    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<QuestionMultiLanguage> QuestionMultiLanguages { get; set; }
    public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public virtual DbSet<QuestionComment> QuestionComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var decimalProps = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

        foreach (var property in decimalProps)
        {
            property.SetPrecision(18);
            property.SetScale(2);
        }

        modelBuilder.Entity<SystemConfig>().OwnsOne<Config>(s => s.Configs, navigationBuilder =>
        {
            navigationBuilder.ToJson();
        });

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientNoAction;

        modelBuilder.Entity<IdentityUserLogin<int>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<int>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<int>>().HasNoKey();
        modelBuilder.Entity<Operation>(o => o.HasIndex(i => i.Name).IsUnique());
        modelBuilder.Entity<Club>().Property(e => e.Latitude).HasPrecision(18, 8);
        modelBuilder.Entity<Club>().Property(e => e.Longitude).HasPrecision(18, 8);
        modelBuilder.Entity<SubscriptionPlanDiscount>().Property(e => e.MembershipFeeDiscountValue).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Module>(entity => { entity.HasMany(e => e.Modules).WithOne(e => e.ParentModule).HasForeignKey(e => e.ParentModuleId); });
        modelBuilder.Entity<RoleModuleOperation>(roleModuleOperation => { roleModuleOperation.HasKey(aec => new { aec.RoleModuleId, aec.OperationId }); });

        OnModelCreatingPartial(modelBuilder);

        SeedDB.Seed(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}