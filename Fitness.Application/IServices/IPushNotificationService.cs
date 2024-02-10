using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface IPushNotificationService
{
    Task<string> SendPushNotification(PushNotification notification);
}