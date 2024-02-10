using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class PushNotificationController : CommonController<PushNotificationController>
{
    private readonly IMapper _mapper;
    private readonly IPushNotificationService _pushNotificationService;
    public PushNotificationController(IPushNotificationService pushNotificationService, IMapper mapper)
    {
        _mapper = mapper;
        _pushNotificationService = pushNotificationService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(PushNotificationPostDto model)
    {
        var item = _mapper.Map<Fitness.Core.Entities.PushNotification>(model);
        var result = await _pushNotificationService.SendPushNotification(item);
        return Ok(result);
    }
}