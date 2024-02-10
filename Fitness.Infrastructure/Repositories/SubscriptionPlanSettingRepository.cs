using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;

namespace Fitness.Infrastructure.Repositories;
public class SubscriptionPlanSettingRepository : GenericRepository<SubscriptionPlanSetting>, ISubscriptionPlanSettingRepository
{
    public SubscriptionPlanSettingRepository(FitnessContext context) : base(context)
    {
    }
}