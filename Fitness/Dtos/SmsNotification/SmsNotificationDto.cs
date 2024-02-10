using Fitness.Dtos.Base;

namespace Fitness.Dtos;
public class SmsNotificationDto : BaseDto
{
    public string Numbers { get; set; } = null!;
    public string UserSender { get; set; } = null!;
    public string Msg { get; set; } = null!;
}