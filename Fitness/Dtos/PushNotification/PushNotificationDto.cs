using Fitness.Dtos.Base;

namespace Fitness.Dtos;
public class PushNotificationDto : BaseDto
{
    public List<string>? MemberIds { get; set; }
    public string TitleEn { get; set; } = null!;
    public string TitleAr { get; set; } = null!;
    public string BodyEn { get; set; } = null!;
    public string BodyAr { get; set; } = null!;
    public string? ImageUrl { get; set; }
}