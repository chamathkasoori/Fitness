using AutoMapper;
using Fitness.Core.Entities;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers;
public class PaymentIntervalsController : CommonController<PaymentIntervalsController>
{
    private readonly IMapper _mapper;
    private readonly IPaymentIntervalService _paymentIntervalService;
    public PaymentIntervalsController(IPaymentIntervalService paymentIntervalService, IMapper mapper)
    {
        _paymentIntervalService = paymentIntervalService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IReadOnlyList<PaymentIntervalDto>> GetAllAsync()
    {
        var items = await _paymentIntervalService.GetAllAsync();
        return _mapper.Map<IReadOnlyList<PaymentIntervalDto>>(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(PaymentIntervalPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<PaymentInterval>(model);
        item.CreatedBy = access!.UserId;

        await _paymentIntervalService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, PaymentIntervalPostDto model)
    {
        var access = HttpContext.Items["Access"] as AccessDto;
        var item = _mapper.Map<PaymentInterval>(model);
        item.Id = id;
        item.ModifiedBy = access!.UserId;
        item.ModifiedOn = DateTime.UtcNow;

        await _paymentIntervalService.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _paymentIntervalService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("PaymentInterval Not Found");
        }

        var access = HttpContext.Items["Access"] as AccessDto;
        item.DeletedBy = access!.UserId;
        item.DeletedOn = DateTime.UtcNow;
        item.IsDelete = true;

        await _paymentIntervalService.UpdateAsync(item);
        return NoContent();
    }
}
