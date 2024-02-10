using AutoMapper;
using Fitness.Application.IServices;
using Fitness.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers.PaymentMethod;
public class PaymentMethodsController : CommonController<PaymentMethodsController>
{
    private readonly IMapper mapper;
    private readonly IPaymentMethodService paymentMethodService;
    public PaymentMethodsController( IMapper mapper, IPaymentMethodService paymentMethodService)
    {
        this.mapper = mapper;
        this.paymentMethodService = paymentMethodService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<PaymentMethodDto>> GetAllAsync()
    {
        var items = await paymentMethodService.GetAllAsync();
        return mapper.Map<IReadOnlyList<PaymentMethodDto>>(items);
    }
}
