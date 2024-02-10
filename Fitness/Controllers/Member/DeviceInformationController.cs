using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Fitness.Infrastructure.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class DeviceInformationController : CommonController<DeviceInformationController>
{
    private readonly IMapper _mapper;
    private readonly IMemberDeviceInformationService _memberDeviceInformationService;
    public DeviceInformationController(IMapper mapper, IMemberDeviceInformationService deviceInformationService)
    {
        _mapper = mapper;
        _memberDeviceInformationService = deviceInformationService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<MemberDeviceInformationDetailDto>> GetAllByMember()
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var items = await _memberDeviceInformationService.GetAllByMemberAsync(access!.MemberId);
        return _mapper.Map<IReadOnlyList<MemberDeviceInformationDetailDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(MemberDeviceInformationPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var exist = await _memberDeviceInformationService.GetByMemberAndDeviceModelAsync(access!.MemberId, model.DeviceModel);
        if (exist != null && exist.Id > 0)
        {
            return BadRequest("Device Information Already Exist");
        }

        var item = _mapper.Map<MemberDeviceInformation>(model);
        item.MemberId = access!.MemberId;
        item.CreatedBy = access!.UserId;
        if (model.DeviceLanguage != null)
        {
            model.DeviceLanguage = model.DeviceLanguage.ToLower().Contains("ar") ? Languages.AR.ToString() : Languages.EN.ToString();
        }
        if (model.DeviceOs != null)
        {
            model.DeviceOs = model.DeviceOs.ToLower().Contains("ios") ? DeviceTypes.IOS.ToString() : DeviceTypes.ANDROID.ToString();
        }
        await _memberDeviceInformationService.AddAsync(item);
        return CreatedAtAction("GetAllByMember", new { id = item.Id }, _mapper.Map<MemberDeviceInformationDto>(item));
    }

    [HttpPut]
    public async Task<IActionResult> Update(MemberDeviceInformationPutDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = await _memberDeviceInformationService.GetByMemberAndDeviceModelAsync(access!.MemberId, model.DeviceModel);
        if (item == null || item.Id == 0)
        {
            return BadRequest("Device Information Not Found");
        }

        item.ModifiedBy = access!.MemberId;
        item.ModifiedOn = DateTime.UtcNow;
        if (model.DeviceLanguage != null)
        {
            item.DeviceLanguage = model.DeviceLanguage.ToLower().Contains("ar") ? Languages.AR.ToString() : Languages.EN.ToString();
        }

        await _memberDeviceInformationService.UpdateAsync(item);
        return NoContent();
    }
}