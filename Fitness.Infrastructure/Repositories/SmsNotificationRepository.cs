using Fitness.Core.Constants;
using Fitness.Core.Entities;
using Fitness.Core.IRepositories;
using Fitness.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fitness.Infrastructure.Repositories;
public class SmsNotificationRepository : ISmsNotificationRepository
{
    private readonly FitnessContext _context;
    private readonly IConfiguration _config;
    public SmsNotificationRepository(FitnessContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string> SendSmsNotification(SmsNotification notification)
    {
        string url = _config.GetSection(AppSettingKey.smsGatewayApiUrl).Value ?? "";
        string apiKey = _config.GetSection(AppSettingKey.smsGatewayApiKey).Value ?? "";
        string userName = _config.GetSection(AppSettingKey.smsGatewayUserName).Value ?? "";

        var baseAddress = new Uri(url);
        var httpClient = new HttpClient { BaseAddress = baseAddress };

        var requestData = new
        {
            userName = userName,
            numbers = notification.Numbers,
            userSender = notification.UserSender,
            apiKey = apiKey,
            msg = notification.Msg

        };

        string jsonContent = JsonConvert.SerializeObject(requestData);

        using (var content = new StringContent(jsonContent, System.Text.Encoding.Default, "application/json"))
        {
            using (var response = await httpClient.PostAsync("/gw/sendsms.php", content))
            {
                string responseHeaders = response.Headers.ToString();
                string responseData = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Status " + (int)response.StatusCode);
                    Console.WriteLine("Headers " + responseHeaders);
                    Console.WriteLine("Data " + responseData);
                    JObject data = JObject.Parse(responseData);
                    return data["message"]!.Value<string>()!;
                }
                else
                {
                    throw new Exception(responseData);
                }
            }
        }
    }
}