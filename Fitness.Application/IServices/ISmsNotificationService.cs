using Fitness.Core.Entities;

namespace Fitness.Application.IServices;
public interface ISmsNotificationService
{
    Task<string> SendSmsNotification(SmsNotification notification);
}