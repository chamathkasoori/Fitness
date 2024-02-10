using Fitness.Core.Entities.Base;

namespace Fitness.Core.Entities;
public class PushNotification : BaseEntity
{
    public List<string>? MemberIds { get; set; }
    public string TitleEn { get; set; } = null!;
    public string TitleAr { get; set; } = null!;
    public string BodyEn { get; set; } = null!;
    public string BodyAr { get; set; } = null!;
    public string? ImageUrl { get; set; }
}
