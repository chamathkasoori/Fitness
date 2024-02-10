using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class SubscriptionPlanSettingsController : CommonController<SubscriptionPlanSettingsController>
{
    private readonly IMapper mapper;
    private readonly ISubscriptionPlanSettingService subscriptionPlanSettingService;
    public SubscriptionPlanSettingsController(IMapper mapper, ISubscriptionPlanSettingService subscriptionPlanSettingService)
    {
        this.mapper = mapper;
        this.subscriptionPlanSettingService = subscriptionPlanSettingService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<SubscriptionPlanSettingDto>> GetAllAsync()
    {
        var items = await subscriptionPlanSettingService.GetAllAsync();
        return mapper.Map<IReadOnlyList<SubscriptionPlanSettingDto>>(items);
    }
}
