using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class InquiriesController : CommonController<InquiriesController>
{
    private readonly IMapper _mapper;
    private readonly IInquiryService _inquiryService;
    private readonly IInquiryReplyService _inquiryReplyService;
    private readonly IMemberService memberService;
    public InquiriesController(IMapper mapper, IInquiryService inquiryService, IInquiryReplyService inquiryReplyService, IMemberService memberService)
    {
        _mapper = mapper;
        _inquiryService = inquiryService;
        _inquiryReplyService = inquiryReplyService;
        this.memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string type = "")
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        if (access!.EmployeeId > 0)
        {
            // This will filter by Logged In Users Club Ids
            var items = await _inquiryService.GetAllByClubsAsync(access!.ClubIdList, type);
            return Ok(_mapper.Map<List<InquiryDetailsDto>>(items));
        }
        else
        {
            // This will Filter by Logged In MemberId-ForMobile
            var inquiries = await _inquiryService.GetAllByMemberAsync(access!.MemberId);
            return Ok(_mapper.Map<List<InquiryDetailsDto>>(inquiries));
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _inquiryService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Inquiry Not Found");
        }

        var mapped = _mapper.Map<InquiryDetailsDto>(item);
        if (item.InquiryReply != null)
        {
            mapped.InquiryReply = _mapper.Map<InquiryReplyDto>(item.InquiryReply);
        }
        return Ok(mapped);
    }

    [HttpGet("{id:int}/Reply")]
    public async Task<IActionResult> GetInquiryReplyByInquiryId(int id)
    {
        var inquiryReplies = await _inquiryReplyService.GetByInquiryIdAsync(id);
        return Ok(_mapper.Map<InquiryReplyDto>(inquiryReplies));
    }

    [HttpPost]
    public async Task<IActionResult> AddInquiry(InquiryPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<Inquiry>(model);
        item.MemberId = access!.MemberId;
        item.TicketId = $"{item.CreatedOn:MMddHHmmss}";
        item.Status = InquiryStatus.Reviewing;
        item.CreatedBy = access!.UserId;

        var member = await memberService.GetByIdAsync(item.MemberId);
        if (member == null)
        {
            throw new Exception("Member Not Found");
        }

        await _inquiryService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, _mapper.Map<InquiryDto>(item));
    }

    [HttpPut("{id:int}/Reply")]
    public async Task<IActionResult> AddReply(int id, InquiryReplyPostDto model)
    {
        var inquiry = await _inquiryService.GetByIdAsync(id);
        if (inquiry == null)
        {
            return BadRequest("Inquiry Not Found");
        }

        var item = await _inquiryReplyService.GetByInquiryIdAsync(inquiry.Id);
        if (item != null)
        {
            return BadRequest("Already Replied To The Inquiry");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        inquiry.Status = InquiryStatus.Replied;
        inquiry.ModifiedBy = access!.UserId;
        inquiry.ModifiedOn = DateTime.UtcNow;
        await _inquiryService.UpdateAsync(inquiry);

        item = _mapper.Map<InquiryReply>(model);
        item.InquiryId = id;
        item.ReplyFromId = access!.UserId;
        item.CreatedBy = access!.UserId;
        await _inquiryReplyService.AddAsync(item);

        return Ok(item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateFeedback(int id, InquiryPutDto model)
    {
        var item = await _inquiryService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Inquiry Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        item.IsHappy = model.IsHappy;
        item.MemberComment = model.MemberComment;
        if (model.IsHappy)
        {
            item.Status = InquiryStatus.Closed;
        }
        await _inquiryService.UpdateAsync(item);
        return Ok(item);
    }


    [HttpPut("{id:int}/Status")]
    public async Task<IActionResult> ChangeInquiryStatus(int id, InquiryStatus status)
    {
        var item = await _inquiryService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("Inquiry Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;
        item.Status = status;
        await _inquiryService.UpdateAsync(item);
        return NoContent();
    }
}