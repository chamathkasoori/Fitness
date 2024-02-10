using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class PushNotificationService : IPushNotificationService
{
    private readonly IPushNotificationRepository _pushNotificationRepository;

    public PushNotificationService(IPushNotificationRepository pushNotificationRepository)
    {
        _pushNotificationRepository = pushNotificationRepository;
    }

    public async Task<string> SendPushNotification(PushNotification notification)
    {
        return await _pushNotificationRepository.SendPushNotification(notification);
    }
}