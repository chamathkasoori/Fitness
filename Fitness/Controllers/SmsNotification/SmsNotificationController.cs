using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class SmsNotificationController : CommonController<SmsNotificationController>
{
    private readonly ISmsNotificationService _smsNotificationService;
    private readonly IMapper _mapper;

    public SmsNotificationController(ISmsNotificationService smsNotificationService, IMapper mapper)
    {
        _smsNotificationService = smsNotificationService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(SmsNotificationPostDto model)
    {
        var item = _mapper.Map<Fitness.Core.Entities.SmsNotification>(model);
        var result = await _smsNotificationService.SendSmsNotification(item);
        return Ok(result);
    }
}