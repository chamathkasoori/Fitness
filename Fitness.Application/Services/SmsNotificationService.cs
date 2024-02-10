using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class SmsNotificationService : ISmsNotificationService
{
    private readonly ISmsNotificationRepository _smsNotificationRepository;
    public SmsNotificationService(ISmsNotificationRepository smsNotificationRepository)
    {
        _smsNotificationRepository = smsNotificationRepository;
    }

    public async Task<string> SendSmsNotification(SmsNotification notification)
    {
        return await _smsNotificationRepository.SendSmsNotification(notification);
    }
}