using Fitness.Core.Entities;

namespace Fitness.Core.IRepositories;
public interface ISmsNotificationRepository
{
    Task<string> SendSmsNotification(SmsNotification notification);
}