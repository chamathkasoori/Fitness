namespace Fitness.Dtos;
public class SmsNotificationPostDto
{
    public string Numbers { get; set; } = null!;
    public string UserSender { get; set; } = null!;
    public string Msg { get; set; } = null!;
}