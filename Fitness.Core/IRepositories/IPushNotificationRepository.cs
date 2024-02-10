using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;

public interface IPushNotificationRepository
{
    Task<string> SendPushNotification(PushNotification notification);

}