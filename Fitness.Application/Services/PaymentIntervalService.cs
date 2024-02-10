using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class PaymentIntervalService : IPaymentIntervalService
{
    private readonly IPaymentIntervalRepository _paymentIntervalRepository;
    public PaymentIntervalService(IPaymentIntervalRepository paymentIntervalRepository)
    {
        _paymentIntervalRepository = paymentIntervalRepository;
    }

    public async Task<IReadOnlyList<PaymentInterval>> GetAllAsync()
    {
        return await _paymentIntervalRepository.GetAllAsync();
    }

    Task<PaymentInterval?> IGenericService<PaymentInterval>.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task IGenericService<PaymentInterval>.AddAsync(PaymentInterval entity)
    {
        await _paymentIntervalRepository.AddAsync(entity);
    }

    async Task IGenericService<PaymentInterval>.UpdateAsync(PaymentInterval entity)
    {
        await _paymentIntervalRepository.UpdateAsync(entity);
    }
}
