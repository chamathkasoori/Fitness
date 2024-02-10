using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers.MobileUsageLog;
public class MobileUsageLogController : CommonController<MobileUsageLogController>
{
    private readonly IMapper _mapper;
    private readonly IMobileUsageLogService _mobileUsageLogService;
    public MobileUsageLogController(IMapper mapper, IMobileUsageLogService mobileUsageLogService)
    {
        _mapper = mapper;
        _mobileUsageLogService = mobileUsageLogService;
    }

    [HttpGet("{memberId:int}")]
    public async Task<IReadOnlyList<MobileUsageLogDto>> GetByMember(int memberId)
    {
        var items = await _mobileUsageLogService.GetByMemberAsync(memberId);
        return _mapper.Map<IReadOnlyList<MobileUsageLogDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(MobileUsageLogPostDto request)
    {
        try
        {
            var mobileUsageLog = _mapper.Map<Fitness.Core.Entities.MobileUsageLog>(request);
            mobileUsageLog.DeviceOs = mobileUsageLog.DeviceOs.ToLower().Contains("ios")
               ? "IOS"
               : "ANDROID";
            await _mobileUsageLogService.AddAsync(mobileUsageLog);
            return CreatedAtAction("GetByMember", new { memberId = request.MemberId }, request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}