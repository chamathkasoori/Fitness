using Fitness.Core.Enums;
namespace Fitness.Core.Constants;
public static class AppSettingsMessages
{
    public const string otpNotFound = "Messages:OtpNotFound";
	public const string failedToSendSMS = "Messages:FailedToSendSMS";
	public const string otpExpired = "Messages:OtpExpired";
	public const string invalidOTP = "Messages:InvalidOTP";
	public const string otpSentSuccessfully = "Messages:OtpSentSuccessfully";

	public static string getMessageByLanguage(String messageKey, string lang)
    {
        if (lang=="Ar")
        {
			return messageKey +":Ar";
		}
        else
        {
			return messageKey + ":En";
		}
    }
}
