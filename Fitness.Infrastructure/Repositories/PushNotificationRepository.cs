using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneSignal.RestAPIv3.Client;
using OneSignal.RestAPIv3.Client.Resources;
using OneSignal.RestAPIv3.Client.Resources.Notifications;

namespace Fitness.Infrastructure.Repositories;
public class PushNotificationRepository : IPushNotificationRepository
{
    private readonly FitnessContext _context;
    private readonly IConfiguration _config;
    public PushNotificationRepository(FitnessContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string> SendPushNotification(PushNotification notification)
    {
        string appId = _config.GetSection(AppSettingKey.oneSignalAppId).Value ?? "";
        string restKey = _config.GetSection(AppSettingKey.oneSignalRestKey).Value ?? "";

        Guid appGuid;
        var isValid = Guid.TryParse(appId, out appGuid);

        if (isValid)
        {

            var client = new OneSignalClient(restKey);
            var opt = new NotificationCreateOptions()
            {
                AppId = appGuid,
                IncludeExternalUserIds = notification.MemberIds
            };
            opt.Headings.Add(LanguageCodes.English, notification.TitleEn);
            opt.Headings.Add(LanguageCodes.Arabic, notification.TitleAr);
            opt.Contents.Add(LanguageCodes.English, notification.BodyEn);
            opt.Contents.Add(LanguageCodes.Arabic, notification.BodyAr);

            if (!notification.ImageUrl.IsNullOrEmpty())
            {
                opt.BigPictureForAndroid = notification.ImageUrl;
            }
            NotificationCreateResult result = await client.Notifications.CreateAsync(opt);
            return result.Id;

        }
        else
        {
            throw new Exception("GUID Error: Invalid Push notification app Id");
        }
    }
}