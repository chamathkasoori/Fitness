using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;
using Fitness.Helpers;

namespace Fitness.Controllers;
public class MemberTransactionsController : CommonController<MemberTransactionsController>
{
    private readonly IMapper mapper;
    private readonly IMemberSubscriptionService memberSubscriptionService;
    private readonly IMemberSubscriptionTransactionService memberSubscriptionTransactionService;
    private readonly IMemberTransactionExternalReferenceService memberTransactionExternalReferenceService;
    public MemberTransactionsController(IMapper mapper, IMemberSubscriptionService memberSubscriptionService, IMemberTransactionExternalReferenceService memberTransactionExternalReferenceService, IMemberSubscriptionTransactionService memberSubscriptionTransactionService)
    {
        this.mapper = mapper;
        this.memberSubscriptionService = memberSubscriptionService;
        this.memberTransactionExternalReferenceService = memberTransactionExternalReferenceService;
        this.memberSubscriptionTransactionService = memberSubscriptionTransactionService;
    }

    [HttpGet("member/{memberId:int}")]
    public async Task<IActionResult> GetAllByMember(int memberId)
    {
        var items = await memberSubscriptionTransactionService.GetAllByMemberAsync(memberId);
        var result = CustomMappers.MapTransactions(items.First());
        return Ok(result);
    }

    [HttpPost("ExternalReference")]
    public async Task<IActionResult> CreateExternalReference([FromBody] MemberTransactionExternalReferenceRequest model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        MemberTransactionExternalReference item = new MemberTransactionExternalReference();
        item.MemberId = model.MemberId;
        item.SubscriptionPlanId = model.SubscriptionPlanId;
        item.TransactionId = Guid.NewGuid();
        item.CreatedBy = access!.UserId;

        await memberTransactionExternalReferenceService.AddAsync(item);
        return Ok(item.TransactionId);
    }

    [HttpPost("CreateManual")]
    public async Task<IActionResult> CreateManualTransaction([FromBody] MemberSubscriptionTransactionPostDto model)
    {
        if (model.Amount == 0)
        {
            return BadRequest("Amount Cannot Be Zero");
        }

        var memberSubscription = await memberSubscriptionService.GetByIdAsync(model.MemberSubscriptionId);
        if (memberSubscription == null)
        {
            return BadRequest("MemberSubscription Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        var item = mapper.Map<MemberSubscriptionTransaction>(model);
        item.CreatedBy = access!.UserId;
        await memberSubscriptionTransactionService.AddAsync(item);

        return Ok(mapper.Map<MemberSubscriptionTransactionDto>(item));
    }
}
