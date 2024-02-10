using Fitness.Application.IServices;
using Fitness.Core.Constants;
using Fitness.Core.Entities;

namespace Fitness.Workers;
public class MemberSubscriptionHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private Timer? _timer = null;
    private readonly IServiceScopeFactory _factory;

    public MemberSubscriptionHostedService(IServiceScopeFactory factory)
    {
        _factory = factory;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);
        try
        {
            await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
            DateTime utcNow = DateTime.UtcNow;
            if (utcNow.Hour > 3)
            {
                IMemberSubscriptionService memberSubscriptionService = asyncScope.ServiceProvider.GetRequiredService<IMemberSubscriptionService>();

                // Check For Freezed Expired
                List<MemberSubscription> freezedExpiredList =  await memberSubscriptionService.GetAllFreezedExpiredAsync();
                foreach (MemberSubscription memberSubscription in freezedExpiredList)
                {
                    memberSubscription.Status = MemberSubscriptionStatus.Current;
                    memberSubscription.ModifiedOn = utcNow;
                    memberSubscription.ModifiedBy = 0;
                    await memberSubscriptionService.UpdateAsync(memberSubscription);
                }

                // Check For Current Expired
                List<MemberSubscription> currentExpiredList = await memberSubscriptionService.GetAllCurrentExpiredAsync();
                foreach (MemberSubscription memberSubscription in currentExpiredList)
                {
                    memberSubscription.Status = MemberSubscriptionStatus.Ended;
                    memberSubscription.ModifiedOn = utcNow;
                    memberSubscription.ModifiedBy = 0;
                    await memberSubscriptionService.UpdateAsync(memberSubscription);
                }

                // Check For NotStarted Active and make them Current
                List<MemberSubscription> notStartedList = await memberSubscriptionService.GetAllNotStartedActiveAsync();
                foreach (MemberSubscription memberSubscription in notStartedList)
                {
                    memberSubscription.Status = MemberSubscriptionStatus.Current;
                    memberSubscription.ModifiedOn = utcNow;
                    memberSubscription.ModifiedBy = 0;
                    await memberSubscriptionService.UpdateAsync(memberSubscription);
                }

                // Check To Freeze
                List<MemberSubscription> freezeToBeList = await memberSubscriptionService.GetAllForFreezeAsync();
                foreach (MemberSubscription memberSubscription in freezeToBeList)
                {
                    memberSubscription.Status = MemberSubscriptionStatus.Freezed;
                    memberSubscription.ModifiedOn = utcNow;
                    memberSubscription.ModifiedBy = 0;
                    await memberSubscriptionService.UpdateAsync(memberSubscription);
                }
            }
        }
        catch (Exception ex)
        {
            String error = ex.ToString();
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
