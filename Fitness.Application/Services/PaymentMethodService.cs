using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }

    async Task<IReadOnlyList<PaymentMethod>> IGenericService<PaymentMethod>.GetAllAsync()
    {
        return await _paymentMethodRepository.GetAllAsync();
    }

    Task<PaymentMethod?> IGenericService<PaymentMethod>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<PaymentMethod>.AddAsync(PaymentMethod entity)
    {
        throw new NotImplementedException();
    }

    Task IGenericService<PaymentMethod>.UpdateAsync(PaymentMethod entity)
    {
        throw new NotImplementedException();
    }
}
