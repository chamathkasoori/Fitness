using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class PaymentIntervalRepository: GenericRepository<PaymentInterval>, IPaymentIntervalRepository
{
    public PaymentIntervalRepository(FitnessContext context) : base(context)
    { 
    }
}
