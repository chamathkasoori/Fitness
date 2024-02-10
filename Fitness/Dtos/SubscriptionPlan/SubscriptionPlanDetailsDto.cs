namespace Fitness.Dtos;
public class SubscriptionPlanDetailsDto: SubscriptionPlanDto
{
    public virtual ICollection<SubscriptionPlanPaymentMethodDto> SubscriptionPlanPaymentMethods { get; set; } = new List<SubscriptionPlanPaymentMethodDto>();
    
    public virtual ICollection<SubscriptionPlanClubDto> SubscriptionPlanAssignedClubs { get; set; } = new List<SubscriptionPlanClubDto>();
    
    public virtual ICollection<SubscriptionPlanClubDto> SubscriptionPlanAvailableClubs { get; set; } = new List<SubscriptionPlanClubDto>();
    
    public virtual ICollection<SubscriptionPlanTagDto> SubscriptionPlanTags { get; set; } = new List<SubscriptionPlanTagDto>();

    public virtual ICollection<SubscriptionPlanApplicationDto> SubscriptionPlanApplications { get; set; } = new List<SubscriptionPlanApplicationDto>();

    public virtual ICollection<SubscriptionPlanRoleDto> SubscriptionPlanRoles { get; set; } = new List<SubscriptionPlanRoleDto>();
    
    public virtual ICollection<SubscriptionPlanSubscriptionPlanSettingDto> SubscriptionPlanSubscriptionPlanSettings { get; set; } = new List<SubscriptionPlanSubscriptionPlanSettingDto>();

    public virtual ICollection<SubscriptionPlanSubscriptionPlanAddonDto> SubscriptionPlanSubscriptionPlanAddons { get; set; } = new List<SubscriptionPlanSubscriptionPlanAddonDto>();

}