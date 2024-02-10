using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Dtos.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class InvoicesController : CommonController<InvoicesController>
{
    private readonly IMapper mapper;
    private readonly IInvoiceService invoiceService;
    private readonly IMemberSubscriptionService memberSubscriptionService;
    
    public InvoicesController(IMapper mapper, IInvoiceService invoiceService, IMemberSubscriptionService memberSubscriptionService)
    {
        this.invoiceService = invoiceService;
        this.memberSubscriptionService = memberSubscriptionService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await invoiceService.GetAllAsync();
        var result = mapper.Map<IReadOnlyList<InvoiceDto>>(invoices);
        return Ok(result);
    }

    [HttpPut("{id:int}/Mail")]
    public async Task<IActionResult> SendMail(int id)
    {
        try
        {
            var invoice = await invoiceService.GetByIdAsync(id);
            if (invoice == null)
                return NotFound("Invoice not found");

            var memberSubscription = await memberSubscriptionService.GetByIdAsync(invoice.MemberSubscriptionId);
            if (memberSubscription == null)
                return NotFound("Member subscription not found");

            memberSubscription = new MemberSubscription
            {
                Member = invoice.MemberSubscription.Member,
                SubscriptionPlan = invoice.MemberSubscription.SubscriptionPlan,
                IsDelete = memberSubscription.IsDelete
            };
            
            if (memberSubscription.IsDelete)
            {
                await invoiceService.SendMail(id, MailTypes.CancelInvoice, invoice, memberSubscription);
                return Ok(id);
            }

            await invoiceService.SendMail(id, MailTypes.Invoice, invoice, memberSubscription);
            return Ok(id);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}